using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSave
{
    
    public float lvlScore;

    public GameSave(float points)
    {
        lvlScore = points;
    }

}
