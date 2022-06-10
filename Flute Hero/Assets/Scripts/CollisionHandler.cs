using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    int score;

    //[SerializeField] ParticleSystem pickupParticle;

    Collider2D currentCollider;
    
    private void Start() {
        currentCollider = GetComponent<Collider2D>();
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            Physics2D.IgnoreCollision(other.collider, currentCollider);
        }
    }
    //IF collision breaks check the tags
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag != "Hoop"){
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            
        }
        else{
            
            GameObject parent = other.gameObject;
            Debug.Log(parent.transform.GetChild(0).name);

            Color parentColor = parent.GetComponent<SpriteRenderer>().color;
            ParticleSystem hoopParticles = parent.transform.GetChild(0).GetComponent<ParticleSystem>();
            ParticleSystem.MainModule settings = hoopParticles.main;
            settings.startColor = parentColor;
            hoopParticles.Play();
            parent.GetComponent<SpriteRenderer>().enabled = false;
            
        }
        
        
        //Debug.Log(score);
    }
    
    
}
