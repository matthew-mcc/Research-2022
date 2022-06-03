using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;
    [SerializeField] float playerPosX = -6.5f;
    [SerializeField] public float moveSpeed = -2f;

    SpriteRenderer sr;

    //coloring
    [SerializeField] private Color dangerColor;
    [SerializeField] private Color safeColor;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, 0);
        sr = GetComponent<SpriteRenderer>();
        sr.color = dangerColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.transform.position.x < -10){
            Destroy(rb.gameObject);
    }
        if (rb.transform.position.x < playerPosX){
            sr.color = safeColor;
        }
    
    }
}
