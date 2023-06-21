using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BootGame : MonoBehaviour
{
    public AudioSource GameSong;
    public event Action BootFinished;

    [SerializeField] private Slider SliderVolume;
    [SerializeField] private GameObject ChipParent;
    [SerializeField] private List<GameObject> Portraits;

    [Header("Chips")]
    [SerializeField] private List<Texture2D> Chips;

    void Start()
    {
        SpawnChips(StaticVariablesPlayerSelect.Players); //This will count number of players
        StartMusic();
        BootFinished();
        Destroy(gameObject);
    }

    private void SpawnChips(int NumPlayers)
    {
        PlayerScript PlayerData;
        for(int i = 0; i < NumPlayers; i++)
        {
            Portraits[i].SetActive(true);
            GameObject player = new("Player_" + (i+1));

            player.AddComponent<Image>();
            player.AddComponent<PlayerScript>();
            PlayerData = player.GetComponent<PlayerScript>();
            player.GetComponent<Image>().sprite = Sprite.Create(Chips[i], new Rect(0.0f, 0.0f, Chips[i].width, Chips[i].height), new Vector2(0.5f, 0.5f));
            PlayerData.Name = "Player" + (i + 1);
            PlayerData.PlayerNumber = (i + 1);

            switch (i + 1)
            {
                case 1:
                    PlayerData.CoordinateX = 55;
                    PlayerData.CoordinateY = -55;
                    break;
                case 2:
                    PlayerData.CoordinateX = 55;
                    PlayerData.CoordinateY = 55;
                    break;
                case 3:
                    PlayerData.CoordinateX = -55;
                    PlayerData.CoordinateY = -55;
                    break;
                case 4:
                    PlayerData.CoordinateX = -55;
                    PlayerData.CoordinateY = 55;
                    break;
                default:
                    PlayerData.CoordinateX = 0;
                    PlayerData.CoordinateY = 0;
                    break;
            }

            switch (i)
            {
                case 0:
                    PlayerData.Colour = "Rojo";
                    break;
                case 1:
                    PlayerData.Colour = "Azul";
                    break;
                case 2:
                    PlayerData.Colour = "Verde";
                    break;
                case 3:
                    PlayerData.Colour = "Amarillo";
                    break;
                case 4:
                    PlayerData.Colour = "Marrón";
                    break;
                case 5:
                    PlayerData.Colour = "Morado";
                    break;
            }
            player.transform.SetParent(ChipParent.transform);

            RectTransform rectTransform;
            rectTransform = player.GetComponent<RectTransform>();

            Vector2 StartingPos = new Vector2(PlayerData.CoordinateX, PlayerData.CoordinateY);
            rectTransform.localPosition = StartingPos;
            rectTransform.sizeDelta = new Vector2(45, 45);

            StaticVariables.Instance.Players.Add(player);
        }
    }

    private void StartMusic()
    {
        GameSong.volume = 0.4f; //This will have to change with playerprefs
        SliderVolume.value = GameSong.volume;
        GameSong.Play();
    }
}
