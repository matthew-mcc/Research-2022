using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float moveSpeed = 2f;
    public Rigidbody2D rb;
    public Transform bg1;
    public Transform bg2;

    private float size;
    
    //public Ridgidbody2D rb_cam;
    void Start()
    {

        size = bg1.GetComponent<BoxCollider2D>().size.x;

        rb = GetComponent<Rigidbody2D>();
        //rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= bg2.position.x){
            bg1.position = new Vector3(bg2.position.x+size, bg1.position.y, bg1.position.z);
            SwitchBg();
        }
    }

    private void SwitchBg(){
        Transform temp = bg1;
        bg1 = bg2;
        bg2 = temp;
    }
}
