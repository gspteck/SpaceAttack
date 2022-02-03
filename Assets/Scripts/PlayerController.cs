using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {    
    public ProjectileController projectilePrefab;
    private InvaderController invaderController;
    public LeftButtonController leftButtonController;
    public RightButtonController rightButtonController;

    public Text lifeText;
    public Text scoreText;
    public Button moveLeftButton;
    public Button moveRightButton;
    public Button shootButton;

    public float movementSpeed = 3f;

    public int life = 100;
    public int damage = 75;

    public int difficulty = 0;
    public int score = 0;
    private int highscore = 0;

    private bool alreadyShot = false;
    private bool shouldShoot = false;

    void Start() {
        life = PlayerPrefs.GetInt("life");
        difficulty = PlayerPrefs.GetInt("difficulty");
        score = PlayerPrefs.GetInt("score");
        highscore = PlayerPrefs.GetInt("highscore");

        if (Application.platform == RuntimePlatform.Android) {
            moveLeftButton.gameObject.SetActive(true);
            moveRightButton.gameObject.SetActive(true);
            shootButton.gameObject.SetActive(true);
            shootButton.onClick.AddListener(SetShootBool);
        } else {
            moveLeftButton.gameObject.SetActive(false);
            moveRightButton.gameObject.SetActive(false);
            shootButton.gameObject.SetActive(false);
        }

        
    }

    void Update() {
        if (Application.platform == RuntimePlatform.Android) {
            if (leftButtonController.buttonPressed && transform.position.x > -7.5f) { MoveLeft(); }
            if (rightButtonController.buttonPressed && transform.position.x < 7.5f) { MoveRight(); }
            if (shouldShoot) { Shoot(); }
        } else {
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > -7.5f) { MoveLeft(); }
            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position.x < 7.5f) { MoveRight(); }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) { Shoot(); }
        }
    }

    void MoveLeft() { transform.position = new Vector3(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y, 0f); }
    void MoveRight() { transform.position = new Vector3(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y, 0f); }
    void Shoot() {
        if (!alreadyShot) {
            ProjectileController spawnedProjectile = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
            spawnedProjectile.destroyed += ProjectileDestroyed;
            alreadyShot = true;
        }        
    }

    void SetShootBool() {
        if (!shouldShoot) {
            shouldShoot = true;
        } else if (shouldShoot) {
            shouldShoot = false;
        }
        
    }

    void ProjectileDestroyed() { alreadyShot = false; }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile")) {
            life -= 1 * difficulty;
            lifeText.text = "HP: " + life.ToString();
            PlayerPrefs.SetInt("life", life);
        } else if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            life -= 5 * difficulty;
            lifeText.text = "HP: " + life.ToString();
            PlayerPrefs.SetInt("life", life);
        } 

        if (life <= 0) {
            PlayerPrefs.SetInt("life", 100);
            PlayerPrefs.SetInt("difficulty", 0);
            PlayerPrefs.SetInt("score", 0);
            if (score > highscore) {
                PlayerPrefs.SetInt("highscore", score);
                PlayerPrefs.SetInt("highscore_changed", 1);
            } else {
                PlayerPrefs.SetInt("highscore_changed", 0);
            }
            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
        }
    }

    private void OnApplicationQuit() {
        PlayerPrefs.SetInt("life", 100);
        PlayerPrefs.SetInt("difficulty", 0);
        PlayerPrefs.SetInt("score", 0);
        if (score > highscore) {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.SetInt("highscore_changed", 1);
        } else {
            PlayerPrefs.SetInt("highscore_changed", 0);
        }
    }
}
