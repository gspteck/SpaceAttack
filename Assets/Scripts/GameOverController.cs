using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {
    public Button restartButton;
    public Button mainmenuButton;
    public Text highscoreText;

    private int highscore;
    private int highscore_changed;

    void Start() {
        restartButton.onClick.AddListener(Restart);
        mainmenuButton.onClick.AddListener(MainMenu);
        highscore = PlayerPrefs.GetInt("highscore");
        highscore_changed = PlayerPrefs.GetInt("highscore_changed");
        if (highscore_changed == 1) {
            highscoreText.text = "New Highscore! \n" + highscore.ToString();
        } else {
            highscoreText.text = "Highscore: " + highscore.ToString();
        }
    }

    void Update() {
        
    }

    void Restart() {
        SceneManager.LoadScene("MainGameScene", LoadSceneMode.Single);
    }

    void MainMenu() {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
}
