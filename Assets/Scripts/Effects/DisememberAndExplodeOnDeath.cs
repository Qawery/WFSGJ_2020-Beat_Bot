using UnityEngine;
using UnityEngine.Assertions;


namespace WFS
{
    public class DisememberAndExplodeOnDeath : MonoBehaviour
    {
        private const float MIN_EXPLOSION_FORCE = 50.0f;
        private const float MAX_EXPLOSION_FORCE = 75.0f;
        private const float MIN_ROTATION_VELOCITY = 20.0f;
        private const float MAX_ROTATION_VELOCITY = 50.0f;
        private const float DESTRUCTION_TIME = 5.0f;


        private void Start()
        {
            var unit = GetComponent<UnitComponent>();
            Assert.IsNotNull(unit);
            unit.Unit.OnDeath += OnDeath;
        }

        private void OnDeath(Unit deadUnit)
        {
            foreach (var bodyPart in GetComponentsInChildren<BodyPart>())
            {
                bodyPart.transform.parent = null;
                bodyPart.Rigidbody.useGravity = true;
                bodyPart.Rigidbody.isKinematic = false;
                Destroy(bodyPart.gameObject, DESTRUCTION_TIME);

                //Randomise direction
                var randomisedVector = transform.up * ((MAX_EXPLOSION_FORCE - MIN_EXPLOSION_FORCE) * Random.Range(0.0f, 1.0f) + MIN_EXPLOSION_FORCE);
                bodyPart.Rigidbody.AddForce(randomisedVector);

                //Randomise rotation
                randomisedVector = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
                randomisedVector *= ((MAX_ROTATION_VELOCITY - MIN_ROTATION_VELOCITY) * Random.Range(0.0f, 1.0f)) + MIN_ROTATION_VELOCITY;
                bodyPart.Rigidbody.angularVelocity = randomisedVector;
            }
        }
    }
}