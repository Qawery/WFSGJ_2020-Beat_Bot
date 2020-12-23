using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;


namespace WFS
{
    public class ReturnToMainMenuButton : MonoBehaviour
    {
        private Button button = null;


        private void Awake()
        {
            button = GetComponent<Button>();
            Assert.IsNotNull(button);
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}