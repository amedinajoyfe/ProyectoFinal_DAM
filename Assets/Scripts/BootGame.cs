using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BootGame : MonoBehaviour
{
    public AudioSource GameSong;
    [SerializeField] private Slider SliderVolume;
    void Awake()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.MaximizedWindow);
        GameSong.volume = 0.4f; //This will have to change with playerprefs
        SliderVolume.value = GameSong.volume;
        GameSong.Play();
    }
}
