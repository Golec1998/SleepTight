using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameLevelController : MonoBehaviour
{

    public GameObject player;
    public GameObject kid;

    [Space]
    public GameObject[] TallGuySpawnpoints;
    public int TallGuysNumber = 1;
    int minTallGuysNumber = 0, maxTallGuysNumber = 5;
    
    public GameObject[] PoltergeistSpawnpoints;
    public int PoltergeistNumber = 0;
    int minPoltergeistNumber = 0, maxPoltergeistNumber = 5;
    
    public GameObject[] StinkerSpawnpoints;
    public int StinkerNumber = 0;
    int minStinkerNumber = 0, maxStinkerNumber = 5;

    [Space]
    public Vector2 levelDuration;
    float timeToEnd, safeTime;

    //Spawn controll
    float[] TallGuysSpawnTimes = new float[5];
    int TallGuysSpawnOrder = 0;
    
    float[] PoltergeistSpawnTimes = new float[5];
    int PoltergeistSpawnOrder = 0;
    
    float[] StinkerSpawnTimes = new float[5];
    int StinkerSpawnOrder = 0;

    string endGameMessage = "-";
    public GameObject pauseMenuUI;
    public GameObject gameUI;
    public GameObject endGameUI;
    public GameObject camera;

    [Space]
    public GameObject endStats;
    public GameObject endFailed;

    [Space]
    public Text endGameResultText;
    public Text sleepPointsText;
    public Text comfortPointsText;
    public Text wokenUpText;
    public Text pointsText;
    public Text failedText;

    ulong maxSleepResult = 0;
    ulong sleepResult = 0;
    int finalSleepScore = 100;
    ulong maxComfortResult = 0;
    ulong comfortResult = 0;
    int finalComfortScore = 100;
    int wokenUp = 0;

    bool gameEnded = false;
    bool scoreOpen = true;

    void Start()
    {
        controllVariables();        //Here we make sure that some variables are in range and set start values of the others

        setupTallGuysSpawns();
        setupPoltergeistSpawns();
        setupStinkerSpawns();

    }

    void Update()
    {
        timeToEnd -= Time.deltaTime;

        spawnControll();

        if (gameEnded)
            endGame();
        else
        {
            countPoints();
            checkForEndGame();
        }

    }

    void controllVariables()
    {
        if (TallGuysNumber < minTallGuysNumber)
            TallGuysNumber = minTallGuysNumber;
        else if (TallGuysNumber > maxTallGuysNumber)
            TallGuysNumber = maxTallGuysNumber;


        timeToEnd = levelDuration.x * 60f + levelDuration.y;
        safeTime = timeToEnd - 60f;
    }

    public string showTime()
    {
        string time;
        if ((int)(timeToEnd % 60f) > 9)
            time = (int)(timeToEnd / 60f) + ":" + (int)(timeToEnd % 60f);
        else
            time = (int)(timeToEnd / 60f) + ":0" + (int)(timeToEnd % 60f);
        return time;
    }

    void setupTallGuysSpawns()
    {
        for(int i = 0; i < TallGuysNumber; i++)
        {
            float nextTallGuysSpawnTimes = Random.Range(0, (safeTime / (TallGuysNumber * 2))) + (safeTime / TallGuysNumber) * i + 30f;
            TallGuysSpawnTimes[TallGuysNumber - i - 1] = nextTallGuysSpawnTimes;
            //Debug.Log(nextTallGuysSpawnTimes);
        }
    }

    void setupPoltergeistSpawns()
    {
        //TODO
    }

    void setupStinkerSpawns()
    {
        //TODO
    }

    void spawnControll()
    {
        //TallGuy spawn controll
        if(TallGuysSpawnOrder < TallGuysSpawnTimes.Length)
            if(timeToEnd < TallGuysSpawnTimes[TallGuysSpawnOrder])
            {
                spawnTallGuy();
                TallGuysSpawnOrder++;
            }
            //TODO better mathematical model for choosing next spawnpoint

        //Poltergeist spawn controll
        //TODO

        //Stinker spawn controll
        //TODO

    }

    void spawnTallGuy()
    {
        int n = TallGuySpawnpoints.Length;
        int randomPoint = Random.Range(0, n);
        TallGuySpawnpoints[randomPoint].GetComponent<TallGuySpawn>().spawn();
    }

    void spawnPoltergeist()
    {
        //TODO
    }

    void spawnStinker()
    {
        //TODO
    }

    void checkForEndGame()
    {
        if(timeToEnd < 1f) endGameMessage = "win";
        else if(player.GetComponent<PlayerStats>().getHealth() < 0f) endGameMessage = "dead";
        else if(wokenUp >= 3) endGameMessage = "woken";

        if(endGameMessage != "-")
            gameEnded = true;
    }

    void countPoints()
    {
        maxSleepResult += 100;
        sleepResult += (ulong)kid.GetComponent<KidController>().getSleep();
        maxComfortResult += 100;
        comfortResult += (ulong)kid.GetComponent<KidController>().getComfort();

        if(kid.GetComponent<KidController>().getWokenUpCount() > wokenUp)
        {
            sleepResult /= 2;
            comfortResult /= 2;
            wokenUp++;
        }

        finalSleepScore = (int)(((float)sleepResult / (float)maxSleepResult) * 100f);
        finalComfortScore = (int)(((float)comfortResult / (float)maxComfortResult) * 100f);

        //Debug.Log("Sleep: " + finalSleepScore + "\nComfort: " + finalComfortScore);
    }

    [System.Obsolete]
    void endGame()
    {
        if(scoreOpen)
        {
            PauseMenu.canPause = false;
            PauseMenu.gameIsPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenuUI.SetActive(false);
            gameUI.SetActive(false);
            endGameUI.SetActive(true);
            Time.timeScale = 0f;
            camera.GetComponent<CinemachineFreeLook>().enabled = false;

            if (endGameMessage == "win")
            {
                sleepPointsText.text = finalSleepScore + "/100";
                comfortPointsText.text = finalComfortScore + "/100";
                wokenUpText.text = wokenUp + "/3";
                pointsText.text = (((finalSleepScore + finalComfortScore) / 2) / (wokenUp + 1)) + "/100";
            }
            else
            {
                endGameResultText.text = "Game Over";
                endStats.SetActive(false);
                endFailed.SetActive(true);

                if (endGameMessage == "dead")
                    failedText.text = "You've been killed";
                else if(endGameMessage == "woken")
                    failedText.text = "The kid woke up too many times";
            }

            scoreOpen = false;
        }
    }

    

}
