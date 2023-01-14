using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ribCollisionHandler : MonoBehaviour
{

    int score;
    float totalScore = 0f;
    int hoopScore = 0;

    [SerializeField] float score_modifier = 2f;
    [SerializeField] TextMeshProUGUI scoreText;

    //[SerializeField] ParticleSystem pickupParticle;

    Collider2D currentCollider;
    [SerializeField] GameObject bar;
    [SerializeField] Color ribAboveBarColor;
    [SerializeField] Color ribAtBarColor;
    Scene currentScene;

    [SerializeField] Slider scoreSlider;
    
    private void Start() {
        currentCollider = GetComponent<Collider2D>();
        currentScene = SceneManager.GetActiveScene();
        //scoreText.color = ribAtBarColor;
        
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

    
            // GameObject parent = other.gameObject;
            // //Debug.Log(parent.transform.GetChild(0).name);

            // Color parentColor = parent.GetComponent<SpriteRenderer>().color;
            // ParticleSystem hoopParticles = parent.transform.GetChild(0).GetComponent<ParticleSystem>();
            // ParticleSystem.MainModule settings = hoopParticles.main;
            // settings.startColor = parentColor;
            // hoopParticles.Play();
            // parent.GetComponent<SpriteRenderer>().enabled = false;
            hoopScore+=1;
            scoreText.text = "SCORE: " + hoopScore.ToString();
            GameObject hoop = other.gameObject;
            GameObject parent = currentCollider.gameObject;

            ParticleSystem ribParticles = parent.transform.GetChild(0).GetComponent<ParticleSystem>();
            ribParticles.Play();
            hoop.GetComponent<SpriteRenderer>().enabled = false;
            
        }

        
        
        
        //Debug.Log(score);
    }

    //This is all for the bar stuff
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Rib_Bar"){

            GameObject parent = currentCollider.gameObject;
            ParticleSystem ribParticles = parent.transform.GetChild(0).GetComponent<ParticleSystem>();
            ParticleSystem.EmissionModule emissionSettings = ribParticles.emission;
            emissionSettings.rateOverTime = 50;
            
            ribParticles.Play();

            //scoreText.color = ribAtBarColor;
            totalScore += (1f/score_modifier);


        }

    }

    private void Update() {
        
        scoreSlider.value = totalScore/1000;

        scoreText.fontSize = 75;
        if(currentScene.name == "Bar_Easy" || currentScene.name == "Bar_Medium" || currentScene.name == "Bar_Hard"){
            scoreText.text = totalScore.ToString("00000");
            
            GameObject rib_parent = currentCollider.gameObject;
            ParticleSystem ribParticles = rib_parent.transform.GetChild(0).GetComponent<ParticleSystem>();
            ParticleSystem.EmissionModule emissionSettings = ribParticles.emission;
            if(rib_parent.transform.position.y > (bar.transform.position.y + bar.transform.localScale.y)){
                emissionSettings.rateOverTime = 100;
                ribParticles.Play();
                //scoreText.color = ribAboveBarColor;
                scoreText.fontSize = 100;
                totalScore += (2f/score_modifier);
            }
        }
        
        
        
    }

    
    
    
}
