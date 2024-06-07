using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logger : MonoBehaviour
{
    // variables for 01_FittsPoke and 02_FittsRay
    private float buttonScale;
    private float buttonDistance;
    private int targetButton;
    private int clickedButton;
    private Vector3 centerLocation;
    private Vector3 fingerLocation;
    private DateTime clickedTime;

    // variables for 03_TracingTask
    private SnapZoneCollisionDetector snapZoneCollisionDetector;
    private string shapeName;
    private DateTime currentTime;
    private Vector3 ballLocation;

    // variables for 04_TypingTask
    private TypingSentenceManager typingSentenceManager;
    private string sentenceID;
    private string targetSentence;
    private string enteredSentence;
    private float typingTime;

    // global variables
    private bool startRecord;
    private string currentEntry;
    private List<string> allEntries;
    private string logPath;
    private string iterationNumStr;
    private string sceneNumStr;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        startRecord = false;

        allEntries = new List<string>();

        iterationNumStr = RandomSceneManager.currentIndex.ToString();
        sceneNumStr = SceneManager.GetActiveScene().buildIndex.ToString();
        sceneName = SceneManager.GetActiveScene().name;
        string fname = iterationNumStr + '_' + sceneNumStr + '_' + sceneName + '_' + System.DateTime.Now.ToString("dd-MMM HH-mm-ss") + ".csv";
        logPath = Path.Combine(Application.persistentDataPath, fname);

        // if (sceneNumStr == "1")/
        allEntries.Add("sceneName,sceneNum,iterationNum,buttonScale,buttonDistance,targetButton,clickedButton,centerLocationX,centerLocationY,centerLocationZ,fingerLocationX,fingerLocationY,fingerLocationZ,clickedTime");
        // }
        if (sceneNumStr == "2")
        {
            allEntries.Add("sceneName,sceneNum,iterationNum,buttonScale,buttonDistance,targetButton,clickedButton,centerLocationX,centerLocationY,centerLocationZ,fingerLocationX,fingerLocationY,fingerLocationZ,clickedTime");
        }
        else if (sceneNumStr == "3")
        {
            allEntries.Add("sceneName,sceneNum,iterationNum,shapeName,currentTime,ballLocationX,ballLocationY,ballLocationZ");
        }
        else if (sceneNumStr == "4")
        {
            allEntries.Add("sceneName,sceneNum,iterationNum,sentenceID,targetSentence,enteredSentence,typingTime");
        }

        StartRecodLog();
    }

    void FixedUpdate()
    {
        if (startRecord)
        {
            buttonScale = PointTask.scales[PointTask.randomList[PointTask.currentIteration]];
            buttonDistance = PointTask.distances[PointTask.randomList[PointTask.currentIteration]];
            targetButton = PointTask.currentIndex;
            clickedButton = PointTask.buttonNumber;
            centerLocation = PointTask.centerLocation;
            fingerLocation = PointTask.fingerLocation;

            currentEntry = new string(
                sceneName + "," +
                sceneNumStr + "," +
                iterationNumStr + "," +
                buttonScale.ToString() + "," +
                buttonDistance.ToString() + "," +
                targetButton.ToString() + "," +
                clickedButton.ToString() + "," +
                Vector3ToString(centerLocation) + "," +
                Vector3ToString(fingerLocation) + "," +
                GetTimeStamp());

            allEntries.Add(currentEntry);

            currentEntry = new string("");
        }

        else if (startRecord && sceneNumStr == "2")
        {
            buttonScale = PointTask.scales[PointTask.randomList[PointTask.currentIteration]];
            buttonDistance = PointTask.distances[PointTask.randomList[PointTask.currentIteration]];
            targetButton = PointTask.currentIndex;
            clickedButton = PointTask.buttonNumber;
            centerLocation = PointTask.centerLocation;
            fingerLocation = PointTask.fingerLocation;

            currentEntry = new string(
                sceneName + "," +
                sceneNumStr + "," +
                iterationNumStr + "," +
                buttonScale.ToString() + "," +
                buttonDistance.ToString() + "," +
                targetButton.ToString() + "," +
                clickedButton.ToString() + "," +
                Vector3ToString(centerLocation) + "," +
                Vector3ToString(fingerLocation) + "," +
                GetTimeStamp());

            allEntries.Add(currentEntry);

            currentEntry = new string("");
        }

        else if (startRecord && sceneNumStr == "3")
        {
            shapeName = snapZoneCollisionDetector.GetShapeName();
            ballLocation = snapZoneCollisionDetector.GetBallLocation();

            currentEntry = new string(
                sceneName + "," +
                sceneNumStr + "," +
                iterationNumStr + "," +
                shapeName + "," +
                GetTimeStamp() + "," +
                Vector3ToString(ballLocation));

            allEntries.Add(currentEntry);

            currentEntry = new string("");
        }

        else if (startRecord && sceneNumStr == "4")
        {
            sentenceID = typingSentenceManager.index.ToString();
            targetSentence = typingSentenceManager.sentences[typingSentenceManager.index - 1];
            enteredSentence = typingSentenceManager.enteredSentence.text.ToString();
            typingTime = typingSentenceManager.typingTime;

            currentEntry = new string(
                sceneName + "," +
                sceneNumStr + "," +
                iterationNumStr + "," +
                sentenceID + "," +
                targetSentence + "," +
                enteredSentence + "," +
                typingTime.ToString());

            allEntries.Add(currentEntry);

            currentEntry = new string("");
        }
    }

    string GetTimeStamp()
    {
        DateTime currentTime = DateTime.Now;
        return currentTime.ToString("HH:mm:ss.fff");
    }

    string Vector3ToString(Vector3 position)
    {
        return position.x.ToString() + "," +
            position.y.ToString() + "," +
            position.z.ToString();
    }

    public void StartRecodLog()
    {
        startRecord = true;
    }

    public void StopRecodLog()
    {
        startRecord = false;
        WriteToCSV();
    }

    void WriteToCSV()
    {
        File.AppendAllLines(logPath, allEntries);
        allEntries.Clear();
    }
}
