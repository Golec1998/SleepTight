using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSave
{
    
    public int lvlScore;

    public GameSave(int points)
    {
        lvlScore = points;
    }

}
