using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllInvaderController : MonoBehaviour {
    public GameObject player;
    public InvaderController[] allInvaders;
    public ProjectileController projectilePrefab;
    public PlayerController playerController;

    public Text scoreText;

    public int totalRows = 3;
    public int totalColumns = 7;
    public AnimationCurve movementSpeed;

    public int amountKilled {get; private set;}
    public int totalInvaders => totalRows * totalColumns;
    public int amountAlive => totalInvaders - amountKilled;
    public float percentKilled => (float)amountKilled / (float)totalInvaders;

    private Vector3 direction = Vector3.right;

    void Start() {
        InvokeRepeating(nameof(Shoot), 3, 3);
    }

    void Update() {
        transform.position += direction * movementSpeed.Evaluate(percentKilled) * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        Vector3 topBorder = new Vector3(0f, 1.5f, 0f);
        Vector3 bottomBorder = new Vector3(0f, 0f, 0f);
        foreach (Transform invader in this.transform) {
            if (!invader.gameObject.activeInHierarchy) {
                continue;
            } else if (direction == Vector3.right && invader.position.x > rightEdge.x - 1) {
                ChangeDirection();
            } else if (direction == Vector3.left && invader.position.x < leftEdge.x + 1) {
                ChangeDirection();
            }

            void ChangeDirection() {
                direction *= -1;
                Vector3 position = transform.position;
                position = new Vector3(position.x, position.y - 0.25f, position.z);
                transform.position = position;
            }
        }

        if (transform.childCount == 0) {
            int difficulty = PlayerPrefs.GetInt("difficulty");
            difficulty += 1;
            PlayerPrefs.SetInt("difficulty", difficulty);
            Awake();
        }
        if (transform.position.y < player.transform.position.y - 3) {
            int difficulty = PlayerPrefs.GetInt("difficulty");
            difficulty += 1;
            PlayerPrefs.SetInt("difficulty", difficulty);
            
            int playerLife = playerController.life - (transform.childCount * 1);
            playerController.lifeText.text = "HP: " + playerLife.ToString();
            PlayerPrefs.SetInt("life", playerLife);
            Awake();
        }
    }

    void Awake() {
        for (int row = 0; row < totalRows; row++) {
            float gridWidth = 1.5f * (totalColumns - 1);
            float gridHeight = 1.25f * (totalRows - 1);
            Vector2 centering = new Vector3(-gridWidth / 2, -gridHeight / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 1.25f), 0);
            for (int column = 0; column < totalColumns; column++) {
                InvaderController invader = Instantiate(allInvaders[row], this.transform);
                invader.invaderDestroyed += Destroyed;
                Vector3 position = rowPosition;
                position.x += column * 1.5f;
                invader.transform.localPosition = position;
            }
        }
        transform.position = new Vector3(0f, 1.5f, 0f);
    }

    void Shoot() {
        foreach (Transform invader in this.transform) {
            if (!invader.gameObject.activeInHierarchy) {
                continue;
            } else if (Random.value < (1 / (float)amountAlive)) {
                Instantiate(projectilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    private void Destroyed() {
        amountKilled += 1;
        int score = PlayerPrefs.GetInt("score");
        score += 5;
        PlayerPrefs.SetInt("score", score);
        scoreText.text = score.ToString();
    }
}
