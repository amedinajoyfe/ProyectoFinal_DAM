using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticVariables : MonoBehaviour
{
    public static StaticVariables Instance { get; private set; }

    public List<GameObject> Players;

    public enum Items
    {
        Apple,
        Ball,
        Bolt,
        Book,
        CasinoChip,
        Clothes,
        Fish,
        Food,
        GoldIngot,
        Soap,
        WoodPlank
    }

    public Dictionary<string, KeyValuePair<string, Items>> Missions = new Dictionary<string, KeyValuePair<string, Items>> {
        { "Food to city", new KeyValuePair<string, Items>("City", Items.Food) },
        { "Fish to market", new KeyValuePair<string, Items>("Market", Items.Fish) },
        { "Clothes to supermarket", new KeyValuePair<string, Items>("Supermarket", Items.Clothes) },
        { "Gold to restaurant", new KeyValuePair<string, Items>("Restaurant", Items.GoldIngot) },
        { "Food to dumpster", new KeyValuePair<string, Items>("Dumpster", Items.Food) },
        { "Apple to shopping mall", new KeyValuePair<string, Items>("ShoppingMall", Items.Apple) },
        { "Soap to city", new KeyValuePair<string, Items>("City", Items.Soap) },
        { "Apple to lake", new KeyValuePair<string, Items>("Lake", Items.Apple) },
    };

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

    public void DisableOutlines()
    {
        var ListOutlines = GameObject.FindGameObjectsWithTag("Outlines");
        foreach (GameObject outline in ListOutlines)
        {
            outline.SetActive(false);
        }
    }

    public void DisableButtons()
    {
        var Buttons = FindObjectsOfType<Button>();

        foreach(Button Btn in Buttons)
        {
            Btn.interactable = false;
        }

        StartCoroutine(ReenableButtons());
    }

    private IEnumerator ReenableButtons()
    {
        var Buttons = FindObjectsOfType<Button>();

        yield return new WaitForSeconds(1.85f);

        foreach (Button Btn in Buttons)
        {
            Btn.interactable = true;
        }

    }

    private int _currentPlayer = 0;
    public int CurrentPlayer
    {
        get
        {
            return _currentPlayer;
        }
        set
        {
            _currentPlayer = value;
        }
    }

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
