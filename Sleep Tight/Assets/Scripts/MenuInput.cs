using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInput : MonoBehaviour
{

    public Animator menuCamController;
    public LayerMask menuLayer;
    Camera cam;

    [Space]
    public GameObject stage1PhotoFrame;
    public GameObject stage2PhotoFrame;
    public GameObject stage3PhotoFrame;

    void Start()
    {
        Time.timeScale = 1f;
        cam = Camera.main;

        GameSave stage1 = SaveSystem.LoadData("Stage1");
        if(stage1 != null)
            if(stage1.lvlScore > 0)
            {
                Debug.Log("Stage 1 score: " + stage1.lvlScore);
                if(stage1.lvlScore > 90)
                    stage1PhotoFrame.transform.Find("Stars").Find("3star").gameObject.SetActive(true);
                else if(stage1.lvlScore > 75)
                    stage1PhotoFrame.transform.Find("Stars").Find("2star").gameObject.SetActive(true);
                else if(stage1.lvlScore > 50)
                    stage1PhotoFrame.transform.Find("Stars").Find("1star").gameObject.SetActive(true);
                stage2PhotoFrame.SetActive(true);

                GameSave stage2 = SaveSystem.LoadData("Stage2");
                if(stage2 != null)
                    if(stage2.lvlScore > 0)
                    {
                        Debug.Log("Stage 2 score: " + stage2.lvlScore);
                        if(stage2.lvlScore > 90)
                            stage2PhotoFrame.transform.Find("Stars").Find("3star").gameObject.SetActive(true);
                        else if(stage2.lvlScore > 75)
                            stage2PhotoFrame.transform.Find("Stars").Find("2star").gameObject.SetActive(true);
                        else if(stage2.lvlScore > 50)
                            stage2PhotoFrame.transform.Find("Stars").Find("1star").gameObject.SetActive(true);
                        stage3PhotoFrame.SetActive(true);

                        GameSave stage3 = SaveSystem.LoadData("Stage3");
                        if(stage3 != null)
                            if(stage3.lvlScore > 0)
                            {
                                Debug.Log("Stage 3 score: " + stage3.lvlScore);
                                if(stage3.lvlScore > 90)
                                    stage3PhotoFrame.transform.Find("Stars").Find("3star").gameObject.SetActive(true);
                                else if(stage3.lvlScore > 75)
                                    stage3PhotoFrame.transform.Find("Stars").Find("2star").gameObject.SetActive(true);
                                else if(stage3.lvlScore > 50)
                                    stage3PhotoFrame.transform.Find("Stars").Find("1star").gameObject.SetActive(true);
                            }
                    }
            }
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
                case "Stage2":
                    startStage(2);
                    break;
                case "Stage3":
                    startStage(3);
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

    private void showExit()
    {
        menuCamController.SetTrigger("Exit");
        this.Invoke(() => { Application.Quit(); }, 1f);
    }

    private void startStage(int lvlNumber)
    {
        if(lvlNumber == 0)
            SceneManager.LoadScene("TestWorld");
        else
            SceneManager.LoadScene("Stage" + lvlNumber);
    }

}
