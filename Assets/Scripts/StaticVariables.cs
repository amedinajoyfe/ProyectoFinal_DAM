using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StaticVariables : MonoBehaviour
{
    public static StaticVariables Instance { get; private set; }

    public List<GameObject> Players;
    public GameObject MissionPrefab;
    public GameObject CardPrefab;
    public GameObject VictoryFeedback;
    public bool Animate = true;

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
        { "Gold to market", new KeyValuePair<string, Items>("Market", Items.GoldIngot) },
        { "Food to dumpster", new KeyValuePair<string, Items>("Dumpster", Items.Food) },
        { "Apple to shopping mall", new KeyValuePair<string, Items>("ShoppingMall", Items.Apple) },
        { "Soap to city", new KeyValuePair<string, Items>("City", Items.Soap) },
        { "Apple to lake", new KeyValuePair<string, Items>("Lake", Items.Apple) },
        { "Chip to dumpster", new KeyValuePair<string, Items>("Dumpster", Items.CasinoChip) },
        { "Wood to vineyard", new KeyValuePair<string, Items>("Vineyard", Items.WoodPlank) },
        { "Bolt to casino", new KeyValuePair<string, Items>("Casino", Items.Bolt) },
        { "Bolt to shopping mall", new KeyValuePair<string, Items>("ShoppingMall", Items.Bolt) },
        { "Apple to restaurant", new KeyValuePair<string, Items>("Restaurant", Items.Apple) },
        { "Fish to restaurant", new KeyValuePair<string, Items>("Restaurant", Items.Fish) },
        { "Ball to market", new KeyValuePair<string, Items>("Market", Items.Ball) },
        { "Wood to lake", new KeyValuePair<string, Items>("Lake", Items.WoodPlank) },
        { "Book to city", new KeyValuePair<string, Items>("City", Items.Book) },
        { "Wood to mountains", new KeyValuePair<string, Items>("Mountains", Items.WoodPlank) },
        { "Food to mountains", new KeyValuePair<string, Items>("Mountains", Items.Food) },
        { "Chip to forest", new KeyValuePair<string, Items>("Forest", Items.CasinoChip) },
        { "Book to forest", new KeyValuePair<string, Items>("Forest", Items.Book) },
        { "Gold to shopping mall", new KeyValuePair<string, Items>("ShoppingMall", Items.GoldIngot) }
    };

    public Dictionary<string, Action> Cards = new Dictionary<string, Action>();

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

    public void EndGame(string Winner)
    {
        Debug.Log("El ganador es: " + Winner);
        DisableButtons(false);
        StartCoroutine(EndGameProcess());
    }

    private IEnumerator EndGameProcess()
    {
        VictoryFeedback.SetActive(true);
        VictoryFeedback.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = "La victoria es para el jugador " + (CurrentPlayer + 1);
        VictoryFeedback.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1f);
        VictoryFeedback.GetComponent<Animator>().enabled = false;
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void DisableOutlines()
    {
        var ListOutlines = GameObject.FindGameObjectsWithTag("Outlines");
        foreach (GameObject outline in ListOutlines)
        {
            outline.SetActive(false);
        }
    }

    public void DisableButtons(bool Reenable = true, float time = 1.85f)
    {
        var Buttons = FindObjectsOfType<Button>();

        foreach(Button Btn in Buttons)
        {
            Btn.interactable = false;
        }

        if(Reenable)
            StartCoroutine(ReenableButtons(time));
    }

    private IEnumerator ReenableButtons(float time)
    {
        var Buttons = FindObjectsOfType<Button>();

        yield return new WaitForSeconds(time);

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

        Cards.Add("ExtraPoint", () => ButtonsActions.Instance.DebugAddPoints());
        Cards.Add("ExtraTurn", () => ButtonsActions.Instance.DebugAddTurn());
        Cards.Add("RemoveItems", () => ButtonsActions.Instance.DebugRemoveAllItems());
        Cards.Add("RemoveQuests", () => ButtonsActions.Instance.DebugRemoveAllMissions());
    }
}
