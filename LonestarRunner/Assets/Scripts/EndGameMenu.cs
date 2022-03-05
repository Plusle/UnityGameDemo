using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour {
    public void RestartButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TitleButton() {
        SceneManager.LoadScene(0);
    }
}
