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
    public RawImage[] kidImg;

    void Update()
    {
        timer.text = gameController.GetComponent<GameLevelController>().showTime();

        hp.value = player.GetComponent<PlayerStats>().getHealth() / player.GetComponent<PlayerStats>().maxHealth;
        energy.value = player.GetComponent<PlayerStats>().getEnergy() / player.GetComponent<PlayerStats>().maxEnergy;

        sleep.value = kid.GetComponent<KidController>().getSleep() / kid.GetComponent<KidController>().maxSleep;
        comfort.value = kid.GetComponent<KidController>().getComfort() / kid.GetComponent<KidController>().maxComfort;

        choosePlayerImg();
        //chooseKidImg();
        
    }

    void choosePlayerImg()
    {
        if(hp.value > 0.6f)
        {
            if (energy.value > 0.6f)
            {
                showPlayerImg(8);
            }
            else if (energy.value > 0.3f)
            {
                showPlayerImg(7);
            }
            else
            {
                showPlayerImg(6);
            }
        }
        else if(hp.value > 0.3f)
        {
            if (energy.value > 0.6f)
            {
                showPlayerImg(5);
            }
            else if (energy.value > 0.3f)
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
            if (energy.value > 0.6f)
            {
                showPlayerImg(2);
            }
            else if (energy.value > 0.3f)
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

    void chooseKidImg()
    {
        if(comfort.value > 0.6f)
        {
            if (sleep.value > 0.6f)
            {
                showKidImg(8);
            }
            else if (sleep.value > 0.3f)
            {
                showKidImg(7);
            }
            else
            {
                showKidImg(6);
            }
        }
        else if(comfort.value > 0.3f)
        {
            if (sleep.value > 0.6f)
            {
                showKidImg(5);
            }
            else if (sleep.value > 0.3f)
            {
                showKidImg(4);
            }
            else
            {
                showKidImg(3);
            }
        }
        else
        {
            if (sleep.value > 0.6f)
            {
                showKidImg(2);
            }
            else if (sleep.value > 0.3f)
            {
                showKidImg(1);
            }
            else
            {
                showKidImg(0);
            }
        }
    }

    void showKidImg(int id)
    {
        for(int i = 0; i < 9; i++)
            if(i == id)
                kidImg[i].enabled = true;
            else
                kidImg[i].enabled = false;
    }

}
