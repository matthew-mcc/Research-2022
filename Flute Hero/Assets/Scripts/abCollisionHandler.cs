using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class abCollisionHandler : MonoBehaviour
{

    int score;
    float totalScore = 0f;

    [SerializeField] float score_modifier = 2f;
    [SerializeField] TextMeshProUGUI scoreText;

    //[SerializeField] ParticleSystem pickupParticle;

    Collider2D currentCollider;
    [SerializeField] GameObject bar;
    [SerializeField] Color abAboveBarColor;
    [SerializeField] Color abAtBarColor;
    Scene currentScene;
    
    private void Start() {
        currentCollider = GetComponent<Collider2D>();
        currentScene = SceneManager.GetActiveScene();
        scoreText.color = abAtBarColor;
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            Physics2D.IgnoreCollision(other.collider, currentCollider);
        }
    }
    //IF collision breaks check the tags
    private void OnTriggerEnter2D(Collider2D other) {
        // if(other.gameObject.tag != "Hoop" || other.gameObject.tag != "Ab_Bar"){
        //     Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            
        // }
        
        
        if(other.gameObject.tag == "Hoop"){

    
            GameObject parent = other.gameObject;
            //Debug.Log(parent.transform.GetChild(0).name);

            Color parentColor = parent.GetComponent<SpriteRenderer>().color;
            ParticleSystem hoopParticles = parent.transform.GetChild(0).GetComponent<ParticleSystem>();
            ParticleSystem.MainModule settings = hoopParticles.main;
            settings.startColor = parentColor;
            hoopParticles.Play();
            parent.GetComponent<SpriteRenderer>().enabled = false;
            
        }

        
        
        
        //Debug.Log(score);
    }

    //This is all for the bar stuff
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Ab_Bar"){

            GameObject parent = currentCollider.gameObject;
            Color parentColor = parent.GetComponent<SpriteRenderer>().color;
            ParticleSystem abParticles = parent.transform.GetChild(0).GetComponent<ParticleSystem>();
            ParticleSystem.MainModule settings = abParticles.main;
            settings.startColor = abAtBarColor;
            
            abParticles.Play();

            scoreText.color = abAtBarColor;
            totalScore += (1f/score_modifier);


        }

    }

    private void Update() {
        
        
        if(currentScene.name == "Bar_Easy" || currentScene.name == "Bar_Medium" || currentScene.name == "Bar_Hard"){
            scoreText.text = totalScore.ToString();
            
            GameObject ab_parent = currentCollider.gameObject;
            ParticleSystem abParticles = ab_parent.transform.GetChild(0).GetComponent<ParticleSystem>();
            ParticleSystem.MainModule settings = abParticles.main;
            if(ab_parent.transform.position.y > (bar.transform.position.y + bar.transform.localScale.y)){
                settings.startColor = abAboveBarColor;
                abParticles.Play();
                //scoreText.color = abAboveBarColor;
                totalScore += (2f/score_modifier);
            }
        }
        
        
        
    }
    
    
}
