using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rib_Bar_Controller : MonoBehaviour
{
    RIB_Belt_Controller RibController;
    [SerializeField] GameObject ribBelt;

    [SerializeField] float heightThreshold = 1f;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        RibController = ribBelt.GetComponent<RIB_Belt_Controller>();
    }

    // Update is called once per frame

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        if(RibBeltInformation.fullyCalibrated){
            
            rb.transform.position = new Vector2(rb.transform.position.x, RibController.maxRange - heightThreshold);
        }
        
        
    }
    void Update()
    {
        
    }
}
