using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour
{

    public Text timer;
    public GameObject gameController;

    [Space]
    public Slider hp; 
    public Slider energy;
    public GameObject player;

    [Space]
    public Slider sleep;
    public Slider comfort;
    public GameObject kid; 

    void Update()
    {
        timer.text = gameController.GetComponent<GameLevelController>().showTime();

        hp.value = player.GetComponent<PlayerStats>().getHealth() / player.GetComponent<PlayerStats>().maxHealth;
        energy.value = player.GetComponent<PlayerStats>().getEnergy() / player.GetComponent<PlayerStats>().maxEnergy;

        sleep.value = kid.GetComponent<KidController>().getSleep() / kid.GetComponent<KidController>().maxSleep;
        comfort.value = kid.GetComponent<KidController>().getComfort() / kid.GetComponent<KidController>().maxComfort;
    }
}
