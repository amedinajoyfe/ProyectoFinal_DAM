using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsActionsPlayerSelect : MonoBehaviour
{
    public GameObject GreenChip;
    public GameObject YellowChip;
    public Button AddPlayerButton;
    public Button RemovePlayerButton;

    private void Start()
    {
        StaticVariablesPlayerSelect.Players = 2;
    }

    public void AddPlayer()
    {
        if (StaticVariablesPlayerSelect.Players == 2)
        {
            GreenChip.SetActive(true);
            StaticVariablesPlayerSelect.Players += 1;
            RemovePlayerButton.interactable = true;
        }
        else if(StaticVariablesPlayerSelect.Players == 3)
        {
            YellowChip.SetActive(true);
            StaticVariablesPlayerSelect.Players += 1;
            AddPlayerButton.interactable = false;
        }
        else
        {
            Debug.Log("Ha ocurrido un error");
        }
    }

    public void RemovePlayer()
    {
        if (StaticVariablesPlayerSelect.Players == 4)
        {
            YellowChip.SetActive(false);
            StaticVariablesPlayerSelect.Players -= 1;
            AddPlayerButton.interactable = true;
        }
        else if (StaticVariablesPlayerSelect.Players == 3)
        {
            GreenChip.SetActive(false);
            StaticVariablesPlayerSelect.Players -= 1;
            RemovePlayerButton.interactable = false;
        }
        else
        {
            Debug.Log("Ha ocurrido un error");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
