using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void PlayButton() {
        SceneManager.LoadScene("LevelScene");
    }

    public void ExitButton() {
        Application.Quit();
    }
}
