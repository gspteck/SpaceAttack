using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;

    void Start() {
        
    }

    void Update() {
        transform.position += direction * speed * Time.deltaTime;

        if (transform.position.y > 10 || transform.position.y < -10) {
            if (this.destroyed != null) {
                this.destroyed.Invoke();
            }
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (this.destroyed != null) {
            this.destroyed.Invoke();
        }
        Destroy(this.gameObject);
    }
}
