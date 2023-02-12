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

    private bool CR_Running = false;

    AB_Belt_Controller AbController;
    [SerializeField] GameObject abBelt;

    RIB_Belt_Controller RibController;
    [SerializeField] GameObject ribBelt;


    void Start()
    {
        AbController = abBelt.GetComponent<AB_Belt_Controller>();
        RibController = ribBelt.GetComponent<RIB_Belt_Controller>();
        //StartCoroutine(bcRoutine());
        infoText.text = "Press Space to Start!";
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space) && CR_Running == false){
            StartCoroutine(bcRoutine());
        }
        else if(Input.GetKeyDown(KeyCode.Space) && CR_Running == true){
            Debug.Log("Sorry, Basecamp already started");
        }

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
        
        CR_Running = true;
        infoText.text = "NORMAL breathing";
        measureNormal = true;
        //Start text timer
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "get ready for deep breathing";
        yield return new WaitForSeconds(inBetweenTime);
        measureNormal = false;
    

        infoText.text = "DEEP breathing";
        measureDeep = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "get ready for normal breathing";
        yield return new WaitForSeconds(3);
        measureDeep = false;

        infoText.text = "NORMAL breathing";
        measureNormal = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "get ready for abdominal breathing";
        yield return new WaitForSeconds(3);
        measureNormal = false;

        infoText.text = "ABDOMINAL breathing";
        measureABOnly = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "get ready for normal breathing";
        yield return new WaitForSeconds(inBetweenTime);
        measureABOnly = false;

        infoText.text = "NORMAL breathing";
        measureNormal = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "get ready for ribcage breathing";
        yield return new WaitForSeconds(inBetweenTime);
        measureNormal = false;

        infoText.text = "RIBCAGE breathing";
        measureRibOnly = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "get ready for normal breathing";
        yield return new WaitForSeconds(inBetweenTime);
        measureRibOnly = false;

        infoText.text = "NORMAL breathing";
        measureNormal = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "get ready for abdominal and ribcage breathing";
        yield return new WaitForSeconds(inBetweenTime);
        measureNormal = false;

        infoText.text = "ABDOMINAL & RIBCAGE breathing";
        measureBoth = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "get ready for normal breathing";
        yield return new WaitForSeconds(inBetweenTime);
        measureBoth = false;

        infoText.text = "NORMAL breathing";
        measureNormal = true;
        yield return new WaitForSeconds(sequenceTime);
        infoText.text = "get ready to hold a deep breath";
        yield return new WaitForSeconds(inBetweenTime);
        measureNormal = false;

        infoText.text = "Take deep breath and HOLD, repeat";
        measureBigBreath = true;
        yield return new WaitForSeconds(20);
        infoText.text = "get ready to hold a deep breath";
        yield return new WaitForSeconds(inBetweenTime);
        measureBigBreath = false;

        infoText.text = "Take deep breath and HOLD";
        measureBigBreath = true;
        yield return new WaitForSeconds(20);
        measureBigBreath = false;

        infoText.text = "All done - Great job!";
        Debug.Log("Finished BaseCamp Sequence");


    }
}
