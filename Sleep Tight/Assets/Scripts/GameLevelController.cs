using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelController : MonoBehaviour
{

    public GameObject player;
    public GameObject[] TallGuySpawnpoints;
    public int TallGuysNumber = 1;
    int minTallGuysNumber = 1, maxTallGuysNumber = 5;
    public Vector2 levelDuration;
    float timeToEnd, safeTime;
    float[] respTime = new float[5];
    int respOrder = 0;

    [Space]
    [Header("Debug only")]
    public bool isItShowTime = false;

    void Start()
    {
        // Make sure values stays in range
        if (TallGuysNumber < minTallGuysNumber)
            TallGuysNumber = minTallGuysNumber;
        else if (TallGuysNumber > maxTallGuysNumber)
            TallGuysNumber = maxTallGuysNumber;


        timeToEnd = levelDuration.x * 60f + levelDuration.y;
        safeTime = timeToEnd - 60f;
        for(int i = 0; i < TallGuysNumber; i++)
        {
            //float nextRespTime = Random.Range((safeTime / TallGuysNumber) * (i), (safeTime / TallGuysNumber) * (i + 1)) + 30f;
            float nextRespTime = Random.Range(0, (safeTime / (TallGuysNumber * 2))) + (safeTime / TallGuysNumber) * i + 30f;
            respTime[TallGuysNumber - i - 1] = nextRespTime;
            Debug.Log(nextRespTime);
        }

    }

    void Update()
    {
        timeToEnd -= Time.deltaTime;

        if(respOrder < respTime.Length)
            if(timeToEnd < respTime[respOrder])
            {
                respTallGuy();
                respOrder++;
            }



        if (checkForEndGame())
            endGame();

        if (isItShowTime)
            Debug.Log(showTime());

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

    bool checkForEndGame()
    {
        bool condition = false;

        if(player.GetComponent<PlayerStats>().getHealth() < 0f
            || timeToEnd < 1f)
            condition = true;

        return condition;
    }

    void respTallGuy()
    {
        int n = TallGuySpawnpoints.Length;
        int randomPoint = Random.Range(0, n);
        TallGuySpawnpoints[randomPoint].GetComponent<TallGuyResp>().resp();
    }

    void endGame()
    {
        Debug.Log("oh snap");
    }

}
