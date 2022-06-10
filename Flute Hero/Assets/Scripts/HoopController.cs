using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoopController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject hoop;
    [SerializeField] private float timeBetweenHoopSets;
    [SerializeField] private float timeBetweenHoopPairs;

    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    

    [SerializeField] float startHoopThreshold = 0f;
    [SerializeField] float endHoopThreshold = 0f;

    [SerializeField] private float startingXPos = 15f;

    private SpriteRenderer sr;

    //For now data will look like --> (string, float) --> (start, hoopYpos) or (end, hoopYPos)
    List<(string, float, string, float)> positions = new List<(string, float, string, float)>();

    float hoopSize = 2f;
    void Start()
    {
       
        hoopSize = hoop.transform.localScale.y;
        sr = hoop.GetComponent<SpriteRenderer>();

        // ADDING MOCK DATA
        //-2.5 start, 2.5 end x2

        positions.Add(("Start", -2.5f, "End", 2.5f));
        positions.Add(("Start", -2.5f, "End", 2.5f));
        positions.Add(("Start", -3f, "End", 3f));
        positions.Add(("Start", -3.5f, "End", 3.5f));
        positions.Add(("Done", 1000f, "Done", 1000f));

        StartCoroutine(spawnHoops());
    }

   

    IEnumerator spawnHoops(){
        foreach(var item in positions){
            string firstIdentifier = item.Item1;
            float firstPos = item.Item2;

            string secondIdentifier = item.Item3;
            float secondPos = item.Item4;

            if(firstIdentifier == "Done"){
                yield return new WaitForSeconds(10);
                SceneManager.LoadScene("Menu");
            }
            else if(firstIdentifier == "Start"){
                spawnHoop(firstPos, firstIdentifier);
                yield return new WaitForSeconds(timeBetweenHoopPairs);
                spawnHoop(secondPos, secondIdentifier);
                yield return new WaitForSeconds(timeBetweenHoopSets);
            }


        }
    }
    // IEnumerator test(){

    //     Debug.Log("First!");
    //     yield return new WaitForSeconds(2);
    //     Debug.Log("Second!");
    //     yield return new WaitForSeconds(2);
    //     Debug.Log("Third");
    // }

    void spawnHoop(float yPos, string identifier){
        if(identifier == "Start"){
            Vector2 spawnPos = new Vector2(startingXPos, (yPos - (hoopSize/2)) + startHoopThreshold);
            sr.color = startColor;
            Instantiate(hoop, spawnPos, Quaternion.identity);
        }
        if(identifier == "End"){
            Vector2 spawnPos = new Vector2(startingXPos, (yPos - (hoopSize/2)) + endHoopThreshold);
            sr.color = endColor;
            Instantiate(hoop, spawnPos, Quaternion.identity);
        }
    } 
}
