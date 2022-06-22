using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ab_Bar_Controller : MonoBehaviour
{

    AB_Belt_Controller AbController;
    [SerializeField] GameObject abBelt;

    [SerializeField] float heightThreshold = 1f;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        AbController = abBelt.GetComponent<AB_Belt_Controller>();
    }

    // Update is called once per frame

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        if(ABBeltInformation.fullyCalibrated){
            
            rb.transform.position = new Vector2(rb.transform.position.x, AbController.maxRange - heightThreshold);
        }
        
        
    }
    void Update()
    {
        
    }
}
