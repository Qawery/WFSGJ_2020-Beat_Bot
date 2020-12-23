using UnityEngine;
using UnityEngine.Assertions;


namespace WFS
{
	public interface IWorld
	{
		void Initialize();
		GameObject InstantiatePrefab(GameObject prefab, Transform parent = null);
		GameObject InstantiatePrefab(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null);
		ComponentType InstantiatePrefab<ComponentType>(ComponentType prefab, Transform parent = null)
			where ComponentType : Component;
		ComponentType InstantiatePrefab<ComponentType>(ComponentType prefab, Vector3 position, Quaternion rotation,
			Transform parent = null) where ComponentType : Component;
		void Destroy(GameObject gameObject);
	}

	public class World : MonoBehaviour, IWorld
	{
		[SerializeField] private bool runOnStart = false;
		private Zenject.RunnableContext context = null;
		
		public void Initialize()
		{
			context.Run();
		}
	
		public GameObject InstantiatePrefab(GameObject prefab, Transform parent = null)
		{
			return InstantiatePrefab(prefab, Vector3.zero, Quaternion.identity, parent);
		}

		public GameObject InstantiatePrefab(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
		{
			var spawnedObject = Object.Instantiate(prefab, position, rotation, parent);
			context.Container.InjectGameObject(spawnedObject);
			return spawnedObject;
		}

		public ComponentType InstantiatePrefab<ComponentType>(ComponentType prefab, Transform parent = null) where ComponentType : Component
		{
			return InstantiatePrefab(prefab.gameObject, parent).GetComponent<ComponentType>();
		}
		
		public ComponentType InstantiatePrefab<ComponentType>(ComponentType prefab, Vector3 position, Quaternion rotation, Transform parent = null) where ComponentType : Component
		{
			return InstantiatePrefab(prefab.gameObject, position, rotation, parent).GetComponent<ComponentType>();
		}

		public void Destroy(GameObject gameObject)
		{
			Object.Destroy(gameObject);
		}

		public Class InstantiateClass<Class>() where Class: new()
		{
			var instance = new Class();
			context.Container.Inject(instance);
			return instance;
		}
		
		private void Awake()
		{
			context = FindObjectOfType<Zenject.RunnableContext>();
			Assert.IsNotNull(context);
			if (runOnStart)
			{
				Initialize();
			}
		}
	}
}
