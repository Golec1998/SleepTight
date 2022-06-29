using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInput : MonoBehaviour
{

    public Animator menuCamController;
    public LayerMask menuLayer;
    Camera cam;

    void Start()
    {
        Time.timeScale = 1f;
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetButtonDown("Attack"))
            performClick();
    }

    private void performClick()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, menuLayer))
        {
            switch(hitInfo.collider.tag)
            {
                case "PlayButton":
                    showPlay();
                    break;
                case "MonstersButton":
                    showMonsters();
                    break;
                case "OptionsButton":
                    showOptions();
                    break;
                case "ExitButton":
                    showExit();
                    break;
                case "Stage1":
                    startStage(1);
                    break;
                case "Back":
                    menuCamController.SetTrigger("BackToMain");
                    break;
            }
            Debug.Log(hitInfo.collider.tag);
        }
    }

    private void showPlay() {menuCamController.SetTrigger("Play");}

    private void showMonsters() {menuCamController.SetTrigger("Monsters");}

    private void showOptions() {menuCamController.SetTrigger("Options");}

    private void showExit() {menuCamController.SetTrigger("Exit");}

    private void startStage(int lvlNumber)
    {
        if(lvlNumber == 1)
            SceneManager.LoadScene("TestWorld");
    }

}
