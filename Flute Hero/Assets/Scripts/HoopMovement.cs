using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;
    [SerializeField] float playerPosX = -6.5f;
    [SerializeField] public float moveSpeed = -2f;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.transform.position.x < -10){
            Destroy(rb.gameObject);
        }

        if (-rb.transform.position.x < playerPosX - 0.5){
            //Animation to warn of missing one..
        }
    }
}
