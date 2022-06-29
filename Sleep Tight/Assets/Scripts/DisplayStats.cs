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

    [Space]
    public RawImage[] playerImg;
    int playerImgId = 8;

    void Update()
    {
        timer.text = gameController.GetComponent<GameLevelController>().showTime();

        hp.value = player.GetComponent<PlayerStats>().getHealth() / player.GetComponent<PlayerStats>().maxHealth;
        energy.value = player.GetComponent<PlayerStats>().getEnergy() / player.GetComponent<PlayerStats>().maxEnergy;

        sleep.value = kid.GetComponent<KidController>().getSleep() / kid.GetComponent<KidController>().maxSleep;
        comfort.value = kid.GetComponent<KidController>().getComfort() / kid.GetComponent<KidController>().maxComfort;



        if(sleep.value > 0.6f)
        {
            if (comfort.value > 0.6f)
            {
                showPlayerImg(8);
            }
            else if (comfort.value > 0.3f)
            {
                showPlayerImg(7);
            }
            else
            {
                showPlayerImg(6);
            }
        }
        else if(sleep.value > 0.3f)
        {
            if (comfort.value > 0.6f)
            {
                showPlayerImg(5);
            }
            else if (comfort.value > 0.3f)
            {
                showPlayerImg(4);
            }
            else
            {
                showPlayerImg(3);
            }
        }
        else
        {
            if (comfort.value > 0.6f)
            {
                showPlayerImg(2);
            }
            else if (comfort.value > 0.3f)
            {
                showPlayerImg(1);
            }
            else
            {
                showPlayerImg(0);
            }
        }
    }

    void showPlayerImg(int id)
    {
        for(int i = 0; i < 9; i++)
            if(i == id)
                playerImg[i].enabled = true;
            else
                playerImg[i].enabled = false;
    }
}
