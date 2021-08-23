using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderController : MonoBehaviour {
    public GameObject player;

    public int life = 100;
    public int damage = 10;

    public System.Action invaderDestroyed;

    void Start() {
        
    }

    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) {
            life -= player.GetComponent<PlayerController>().damage;
            if (life < 0) {
                invaderDestroyed.Invoke();
                Destroy(this.gameObject);
            }
        } else if (collider.gameObject.layer == LayerMask.NameToLayer("Obstacle") || collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            Destroy(this.gameObject);
        }
    }
}
