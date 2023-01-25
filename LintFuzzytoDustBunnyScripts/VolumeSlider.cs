using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//created by Jocelyn Brown 5/18/2022
//resource on code: "How to Make a Volume Slider in 4 Minutes - Easy Unity Tutorial" by Hooson: https://www.youtube.com/watch?v=yWCHaTwVblk&t=3s
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("gameVolume"))
        {
            PlayerPrefs.SetFloat("gameVolume", 0.5f);//load at 50% if there is not save data
            Load();
        }
        else
        {
            Load();
        }
    }

    public void AlterVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();

    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("gameVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("gameVolume", volumeSlider.value);
    }
}
