using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    
    private float moveSpeed = -2f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        
        rb.velocity = new Vector2(moveSpeed, 0);
    }

    private void Update() {
        if(rb.transform.position.x < -10){
            Destroy(rb.gameObject);
        }
    }

    // Update is called once per frame

}
