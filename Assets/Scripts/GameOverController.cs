using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class GameOverController : MonoBehaviour {
    private string gameUnityID = "4276795";
    private string gameAppodealID = "b896e0dabc496e4e137317ce16b623a1614b0e39d3a6d1a1";

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
        UnityAdsInit();
        ApppodealInit();
        if (highscore_changed == 1) {
            highscoreText.text = "New Highscore! \n" + highscore.ToString();
            ShowNonSkippable();
        } else {
            highscoreText.text = "Highscore: " + highscore.ToString();
            ShowInterstitial();
        }
    }

    void Update() {
        
    }

    void UnityAdsInit() {
        Advertisement.Initialize(gameUnityID, false);
    }

    void ApppodealInit() {
        Appodeal.disableLocationPermissionCheck();
        Appodeal.initialize(
            gameAppodealID,
            Appodeal.INTERSTITIAL | Appodeal.NON_SKIPPABLE_VIDEO,
            true
        );
    }

    void ShowNonSkippable() {
        if (Appodeal.isLoaded(Appodeal.NON_SKIPPABLE_VIDEO)) {
            Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
        } else {
            ShowInterstitial();
        }
    }

    void ShowInterstitial() {
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL)) {
            Appodeal.show(Appodeal.INTERSTITIAL);
        } else {
            Advertisement.Show("Interstitial_Android");
        }
    }

    void Restart() {
        SceneManager.LoadScene("MainGameScene", LoadSceneMode.Single);
    }

    void MainMenu() {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
}
