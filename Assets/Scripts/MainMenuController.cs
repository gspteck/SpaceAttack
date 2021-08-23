using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    public Button playButton;
    public Button leaderboardButton;

    void Start() {
        playButton.onClick.AddListener(Play);
        leaderboardButton.onClick.AddListener(OpenLeaderboard);
    }

    void Update() {
        
    }

    void Play() {
        SceneManager.LoadScene("MainGameScene", LoadSceneMode.Single);
    }

    void OpenLeaderboard() {
        
    }
}
