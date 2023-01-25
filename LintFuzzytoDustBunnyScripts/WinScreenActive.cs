using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//created by Jocelyn Brown, 5/19/2022

public class WinScreenActive : MonoBehaviour
{
    public Sprite finalWinSprite;
    public GameObject NextLevelButton;
    public GameObject ThanksButton;//for final level to pull up the thank you screen
    private void Awake()//at script awake, pause the game
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 3)
        {
            gameObject.GetComponent<Image>().sprite = finalWinSprite;
            NextLevelButton.SetActive(false);
            ThanksButton.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public void LoadNextLevel()
    {
        
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}
