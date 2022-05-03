using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    int score;
    Collider2D currentCollider;
    private void Start() {
        currentCollider = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            Physics2D.IgnoreCollision(other.collider, currentCollider);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
        score++;
        Debug.Log(score);
    }

}