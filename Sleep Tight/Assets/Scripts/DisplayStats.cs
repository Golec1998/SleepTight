using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour
{

    public Text timer;
    public GameObject gameController;

    void Update()
    {
        timer.text = gameController.GetComponent<GameLevelController>().showTime();
    }
}
