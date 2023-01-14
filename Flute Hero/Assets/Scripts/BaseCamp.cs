using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BaseCamp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI infoText;
    private float sequenceTime = 27f;
    private float inBetweenTime = 3f;
    

    private bool measureNormal = false;
    private bool measureDeep = false;
    private bool measureABOnly = false;
    private bool measureRibOnly = false;
    private bool measureBoth = false;
    private bool measureBigBreath = false;

    AB_Belt_Controller AbController;
    [SerializeField] GameObject abBelt;

    RIB_Belt_Controller RibController;
    [SerializeField] GameObject ribBelt;


    void Start()
    {
        AbController = abBelt.GetComponent<AB_Belt_Controller>();
        RibController = ribBelt.GetComponent<RIB_Belt_Controller>();
        StartCoroutine(bcRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(measureNormal){
            string logString = "Normal Breathing (AB): " + AbController.newCurrentVoltage.ToString() + " Normal Breathing (Rib): " + RibController.newCurrentVoltage.ToString();
            Debug.Log(logString);
        }
        if(measureDeep){
            string logString = "Deep Breathing (AB): " + AbController.newCurrentVoltage.ToString() + " Deep Breathing (Rib): " + RibController.newCurrentVoltage.ToString();
            Debug.Log(logString);
        }
        if(measureABOnly){
            string logString = "Holding Abdomen Only (AB): " + AbController.newCurrentVoltage.ToString() + " Holding Abdomen Only (Rib): " + RibController.newCurrentVoltage.ToString();
            Debug.Log(logString);
        }
        if(measureRibOnly){
            string logString = "Holding Ribcage Only (AB): " + AbController.newCurrentVoltage.ToString() + " Holding Ribcage Only (Rib): " + RibController.newCurrentVoltage.ToString();
            Debug.Log(logString);
        }
        if(measureBoth){
            string logString = "Deep Abdomen and Ribcage (AB): " + AbController.newCurrentVoltage.ToString() + " Deep Abdomen and Ribcage (Rib): " + RibController.newCurrentVoltage.ToString();
            Debug.Log(logString);
        }
        if(measureBigBreath){
            string logString = "Big Breath (AB): " + AbController.newCurrentVoltage.ToString() + " Big Breath (Rib): " + RibController.newCurrentVoltage.ToString();
            Debug.Log(logString);
        }
        
    }

    IEnumerator bcRoutine(){

        infoText.text = "Take Normal Breaths!";
        measureNormal = true;
        //Start text timer
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "Get Ready For Deep Breaths!";
        yield return new WaitForSeconds(inBetweenTime);
        measureNormal = false;
    

        infoText.text = "Take Deep Breaths!";
        measureDeep = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "Get Ready For Normal Breaths!";
        yield return new WaitForSeconds(3);
        measureDeep = false;

        infoText.text = "Take Normal Breaths!";
        measureNormal = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "Get Ready For Deep Breaths!";
        yield return new WaitForSeconds(3);
        measureNormal = false;

        infoText.text = "Try breathing through your Abdomen!";
        measureABOnly = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "Get Ready For Normal Breathing!";
        yield return new WaitForSeconds(inBetweenTime);
        measureABOnly = false;

        infoText.text = "Take Normal Breaths!";
        measureNormal = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "Get Ready For Deep Breathing!";
        yield return new WaitForSeconds(inBetweenTime);
        measureNormal = false;

        infoText.text = "Try breathing through your Ribs!";
        measureRibOnly = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "Get Ready For Normal Breathing!";
        yield return new WaitForSeconds(inBetweenTime);
        measureRibOnly = false;

        infoText.text = "Take Normal Breaths!";
        measureNormal = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "Get Ready For Deep Breathing!";
        yield return new WaitForSeconds(inBetweenTime);
        measureNormal = false;

        infoText.text = "Deep Breaths through your Ribs and Abdomen!";
        measureBoth = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "Get Ready For Normal Breathing!";
        yield return new WaitForSeconds(inBetweenTime);
        measureBoth = false;

        infoText.text = "Take Normal Breaths!";
        measureNormal = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "Get Ready For a Big Breath!";
        yield return new WaitForSeconds(inBetweenTime);
        measureNormal = false;

        infoText.text = "Take a Big Breath!";
        measureBigBreath = true;
        yield return new WaitForSeconds(20);
        infoText.text = "Get ready for the next!";
        yield return new WaitForSeconds(inBetweenTime);
        measureBigBreath = false;

        infoText.text = "Take a Big Breath!";
        measureBigBreath = true;
        yield return new WaitForSeconds(20);
        measureBigBreath = false;

        infoText.text = "All done - Great job!";
        Debug.Log("Finished BaseCamp Sequence");


    }
}
