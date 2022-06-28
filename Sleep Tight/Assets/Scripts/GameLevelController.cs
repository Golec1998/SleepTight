using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelController : MonoBehaviour
{

    public GameObject player;

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

    void Start()
    {
        controllVariables();        //Here we make sure that some variables are in range and set start values of the others

        setupTallGuysSpawns();
        

    }

    void Update()
    {
        timeToEnd -= Time.deltaTime;

        spawnControll();

        if (checkForEndGame())
            endGame();

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
            Debug.Log(nextTallGuysSpawnTimes);
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

    bool checkForEndGame()
    {
        bool condition = false;

        if(player.GetComponent<PlayerStats>().getHealth() < 0f)
        {
            endGameMessage = "dead";
        }
        if(timeToEnd < 1f)
        {
            endGameMessage = "win";
        }

        if(endGameMessage != "-")
            condition = true;

        return condition;
    }

    void endGame()
    {
        PauseMenu.canPause = false;

        Debug.Log("oh snap");
    }

}
