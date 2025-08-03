using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuFunctionality : MonoBehaviour{

    public void OnEnable() {
        if (GetComponent<UIDocument>() != null) {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            Button menuButton = root.Q<Button>("MenuButton");
            menuButton.clicked += goToMainMenu;
        }
    }

    public void goToGame() {
        SceneManager.LoadScene("MainGame");
    }

    public void goToCredits() {
        SceneManager.LoadScene("Credits");
    }

    public void goToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
