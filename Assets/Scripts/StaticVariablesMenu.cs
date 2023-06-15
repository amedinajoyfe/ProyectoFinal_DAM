using System.Collections.Generic;
using UnityEngine;

public class StaticVariablesMenu : MonoBehaviour
{
    public static StaticVariablesMenu Instance { get; private set; }

    public Dictionary<string, int[]> ScreenResolution = new Dictionary<string, int[]> {
        { "1920 X 1080", new int[]{1920,1080} },
        { "1440 X 900", new int[]{1440,900} },
        { "1366 X 768", new int[]{1366,768} },
        { "1280 X 720", new int[]{1280,720} },
        { "720 X 480", new int[]{720,480} }
    };

    public Dictionary<string, FullScreenMode> ScreenMode = new Dictionary<string, FullScreenMode> {
        { "VENTANA SIN BORDES", FullScreenMode.FullScreenWindow },
        { "MODO VENTANA", FullScreenMode.Windowed },
        { "PANTALLA COMPLETA", FullScreenMode.ExclusiveFullScreen }
    };

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
