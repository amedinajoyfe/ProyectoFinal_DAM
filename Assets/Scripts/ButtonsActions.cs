using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class ButtonsActions : MonoBehaviour
{
    //Los comentarios los pongo en inglés porque me apetece igual que los nombres
    [SerializeField] private TMP_Text FeedbackScreenMode;
    [SerializeField] private TMP_Text FeedbackScreenResolution;
    [SerializeField] private GameObject ChatCanvas;
    public AudioSource GameSong;

    private Dictionary<string, FullScreenMode> ScreenMode = new Dictionary<string, FullScreenMode> { 
        { "VENTANA SIN BORDES", FullScreenMode.FullScreenWindow }, 
        { "MODO VENTANA", FullScreenMode.Windowed },
        { "PANTALLA COMPLETA", FullScreenMode.ExclusiveFullScreen }
    };
    private int PosMode = 0;

    private Dictionary<string, int[]> ScreenResolution = new Dictionary<string, int[]> {
        { "1920 X 1080", new int[]{1920,1080} },
        { "1440 X 900", new int[]{1440,900} },
        { "1366 X 768", new int[]{1366,768} },
        { "1280 X 720", new int[]{1280,720} },
        { "720 X 480", new int[]{720,480} }
    };
    private int PosRes = 0;


    public void ChangeWindowModeLeft()
    {
        PosMode = PosMode == 0 ? 2 : PosMode -= 1; //If pos equal to 0 then go to 2, else subtract 1
        FeedbackScreenMode.text = ScreenMode.ElementAt(PosMode).Key;
#if !UNITY_EDITOR
        Screen.fullScreenMode = ScreenMode.ElementAt(PosMode).Value;
#endif
    }

    public void ChangeWindowModeRight()
    {
        PosMode = PosMode == 2 ? 0 : PosMode += 1; //If pos equal to 2 then go to 0, else add 1
        FeedbackScreenMode.text = ScreenMode.ElementAt(PosMode).Key;
#if !UNITY_EDITOR
        Screen.fullScreenMode = ScreenMode.ElementAt(PosMode).Value;
#endif
    }

    public void ChangeScreenResolutionLeft()
    {
        PosRes = PosRes == 0 ? 4 : PosRes -= 1;
        FeedbackScreenResolution.text = ScreenResolution.ElementAt(PosRes).Key;
#if !UNITY_EDITOR
        Screen.SetResolution(ScreenResolution.ElementAt(PosRes).Value[0], ScreenResolution.ElementAt(PosRes).Value[1], Screen.fullScreenMode);
#endif
    }

    public void ChangeScreenResolutionRight()
    {
        PosRes = PosRes == 4 ? 0 : PosRes += 1;
        FeedbackScreenResolution.text = ScreenResolution.ElementAt(PosRes).Key;
#if !UNITY_EDITOR
        Screen.SetResolution(ScreenResolution.ElementAt(PosRes).Value[0], ScreenResolution.ElementAt(PosRes).Value[1], Screen.fullScreenMode);
#endif
    }

    public void ChangeVolume(Slider slider)
    {
        GameSong.volume = slider.value;
    }

    public void ToggleCanvas()
    {
        if(!ChatCanvas.activeSelf)
        {
            ChatCanvas.SetActive(true);
        }
        else
        {
            ChatCanvas.SetActive(false);
        }
    }
}
