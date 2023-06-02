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

    private int PosMode = 0;
    private int PosRes = 0;

    public List<GameObject> EdificiosList;
    public Dictionary<string, List<GameObject>> Squares;

    private void Start()
    {
        Squares = new Dictionary<string, List<GameObject>> {
        { "CentralPlaza", new List<GameObject>{EdificiosList[1], EdificiosList[2], EdificiosList[3], EdificiosList[4], EdificiosList[7] } },
        { "City", new List<GameObject>{EdificiosList[0], EdificiosList[5]} },
        { "Supermarket", new List<GameObject>{EdificiosList[0], EdificiosList[11], EdificiosList[6]} },
        { "Market", new List<GameObject>{EdificiosList[0]} },
        { "Lake", new List<GameObject>{EdificiosList[0], EdificiosList[10], EdificiosList[9]} },
        { "Casino", new List<GameObject>{EdificiosList[1]} },
        { "Dumpster", new List<GameObject>{EdificiosList[2]} },
        { "Vineyard", new List<GameObject>{EdificiosList[0], EdificiosList[8]} },
        { "Mountains", new List<GameObject>{EdificiosList[7]} },
        { "Restaurant", new List<GameObject>{EdificiosList[4]} },
        { "Forest", new List<GameObject>{EdificiosList[4]} },
        { "ShoppingMall", new List<GameObject>{EdificiosList[2]} }
    };
}

    public void ChangeWindowModeLeft()
    {
        PosMode = PosMode == 0 ? 2 : PosMode -= 1; //If pos equal to 0 then go to 2, else subtract 1
        FeedbackScreenMode.text = StaticVariables.ScreenMode.ElementAt(PosMode).Key;
#if !UNITY_EDITOR
        Screen.fullScreenMode = ScreenMode.ElementAt(PosMode).Value;
#endif
    }

    public void ChangeWindowModeRight()
    {
        PosMode = PosMode == 2 ? 0 : PosMode += 1; //If pos equal to 2 then go to 0, else add 1
        FeedbackScreenMode.text = StaticVariables.ScreenMode.ElementAt(PosMode).Key;
#if !UNITY_EDITOR
        Screen.fullScreenMode = ScreenMode.ElementAt(PosMode).Value;
#endif
    }

    public void ChangeScreenResolutionLeft()
    {
        PosRes = PosRes == 0 ? 4 : PosRes -= 1;
        FeedbackScreenResolution.text = StaticVariables.ScreenResolution.ElementAt(PosRes).Key;
#if !UNITY_EDITOR
        Screen.SetResolution(ScreenResolution.ElementAt(PosRes).Value[0], ScreenResolution.ElementAt(PosRes).Value[1], Screen.fullScreenMode);
#endif
    }

    public void ChangeScreenResolutionRight()
    {
        PosRes = PosRes == 4 ? 0 : PosRes += 1;
        FeedbackScreenResolution.text = StaticVariables.ScreenResolution.ElementAt(PosRes).Key;
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

    public void MoveButton()
    {
        GameObject PlayerToMove = StaticVariables.Players[StaticVariables.CurrentPlayer];
        if (!PlayerToMove.GetComponent<PlayerScript>().IsMoving)
        {
            foreach (GameObject Square in Squares[PlayerToMove.GetComponent<PlayerScript>().CurrPosition])
            {
                Square.SetActive(true);
            }
            PlayerToMove.GetComponent<PlayerScript>().IsMoving = !PlayerToMove.GetComponent<PlayerScript>().IsMoving;
        }
        else
        {
            StaticVariables.DisableOutlines();
            PlayerToMove.GetComponent<PlayerScript>().IsMoving = !PlayerToMove.GetComponent<PlayerScript>().IsMoving;
        }
    }
}
