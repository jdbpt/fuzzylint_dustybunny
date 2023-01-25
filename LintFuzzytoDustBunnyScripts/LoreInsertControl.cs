using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//created by Jocely Brown 5/19/2022 based off script I created for S-SI Spr 2022
public class LoreInsertControl : MonoBehaviour
{
    public GameObject leveltext_0;
    public GameObject leveltext_1;
    public GameObject leveltext_2;
    public GameObject leveltext_3;

    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex == 0)
        {
            leveltext_0.SetActive(true);
        }
        else if (currentScene.buildIndex == 1)
        {
            leveltext_1.SetActive(true);
        }
        else if (currentScene.buildIndex == 2)
        {
            leveltext_2.SetActive(true);
        }
        else if (currentScene.buildIndex == 3)
        {
            leveltext_3.SetActive(true);
        }


    }

    public void LoadTrial1()
    {
        SceneManager.LoadScene(1);
    }
}
