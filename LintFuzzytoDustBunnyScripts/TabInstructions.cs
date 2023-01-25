using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabInstructions : MonoBehaviour
{
    public GameObject Instructions;
    private bool instructionsShowing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Instructions"))
        {
            if (!instructionsShowing)
            {
                Instructions.SetActive(true);
                instructionsShowing = true;
            }
            else
            {
                Instructions.SetActive(false);
                instructionsShowing = false;
            }
        }
    }
}
