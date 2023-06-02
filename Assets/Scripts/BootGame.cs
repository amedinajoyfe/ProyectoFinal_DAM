using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BootGame : MonoBehaviour
{
    public AudioSource GameSong;
    public event Action BootFinished;

    [SerializeField] private Slider SliderVolume;
    [SerializeField] private Canvas Map;

    [Header("Chips")]
    [SerializeField] private List<Texture2D> Chips;

    void Awake()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.MaximizedWindow);
        SpawnChips(4); //This will count number of players
        StartMusic();
        BootFinished();
        Destroy(gameObject);
    }

    private void SpawnChips(int NumPlayers)
    {
        for(int i = 0; i < NumPlayers; i++)
        {
            GameObject player = new GameObject("Player_" + (i+1));
            player.AddComponent<Image>();
            player.AddComponent<PlayerScript>();
            player.GetComponent<Image>().sprite = Sprite.Create(Chips[i], new Rect(0.0f, 0.0f, Chips[i].width, Chips[i].height), new Vector2(0.5f, 0.5f));
            player.transform.SetParent(Map.GetComponent<Canvas>().transform);

            RectTransform rectTransform;
            rectTransform = player.GetComponent<RectTransform>();
            Vector2 StartingPos = i % 2 == 0 ? new Vector2(-55, i>1 ? 55 : -55) : new Vector2(55, i > 1 ? 55 : -55); //Estoy aprendiendo a usar estos por eso hay varios
            rectTransform.localPosition = StartingPos;
            rectTransform.sizeDelta = new Vector2(45, 45);

            StaticVariables.Players.Add(player);
        }
    }

    private void StartMusic()
    {
        GameSong.volume = 0.4f; //This will have to change with playerprefs
        SliderVolume.value = GameSong.volume;
        GameSong.Play();
    }
}
