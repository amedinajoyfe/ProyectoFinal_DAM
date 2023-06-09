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
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject MenuExit;
    [SerializeField] private GameObject MenuOptions;

    [SerializeField] private Animator ObjectObtained;

    public AudioSource GameSong;

    private int PosMode = 0;
    private int PosRes = 0;

    public List<GameObject> EdificiosList;
    public Dictionary<string, List<GameObject>> Squares;
    public Dictionary<string, StaticVariables.Items> PossibleObjects;

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

        PossibleObjects = new Dictionary<string, StaticVariables.Items> {
        { "City", StaticVariables.Items.Ball },
        { "Supermarket", StaticVariables.Items.Soap },
        { "Market", StaticVariables.Items.Clothes },
        { "Lake", StaticVariables.Items.Fish },
        { "Casino", StaticVariables.Items.CasinoChip },
        { "Dumpster", StaticVariables.Items.Bolt },
        { "Vineyard", StaticVariables.Items.Apple },
        { "Mountains", StaticVariables.Items.GoldIngot },
        { "Restaurant", StaticVariables.Items.Food },
        { "Forest", StaticVariables.Items.WoodPlank },
        { "ShoppingMall", StaticVariables.Items.Book }
        };
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Menu.activeSelf)
            {
                Menu.SetActive(false);
                return;
            }
            else if(MenuExit.activeSelf)
            {
                MenuExit.SetActive(false);
            }
            else if(MenuOptions.activeSelf)
            {
                MenuOptions.SetActive(false);
            }
            else
            {
                Menu.SetActive(true);
                return;
            }
            Menu.SetActive(true);
        }
    }

    public void ChangeWindowModeLeft()
    {
        PosMode = PosMode == 0 ? 2 : PosMode -= 1; //If pos equal to 0 then go to 2, else subtract 1
        FeedbackScreenMode.text = StaticVariables.Instance.ScreenMode.ElementAt(PosMode).Key;
#if !UNITY_EDITOR
        Screen.fullScreenMode = ScreenMode.ElementAt(PosMode).Value;
#endif
    }

    public void ChangeWindowModeRight()
    {
        PosMode = PosMode == 2 ? 0 : PosMode += 1; //If pos equal to 2 then go to 0, else add 1
        FeedbackScreenMode.text = StaticVariables.Instance.ScreenMode.ElementAt(PosMode).Key;
#if !UNITY_EDITOR
        Screen.fullScreenMode = ScreenMode.ElementAt(PosMode).Value;
#endif
    }

    public void ChangeScreenResolutionLeft()
    {
        PosRes = PosRes == 0 ? 4 : PosRes -= 1;
        FeedbackScreenResolution.text = StaticVariables.Instance.ScreenResolution.ElementAt(PosRes).Key;
#if !UNITY_EDITOR
        Screen.SetResolution(ScreenResolution.ElementAt(PosRes).Value[0], ScreenResolution.ElementAt(PosRes).Value[1], Screen.fullScreenMode);
#endif
    }

    public void ChangeScreenResolutionRight()
    {
        PosRes = PosRes == 4 ? 0 : PosRes += 1;
        FeedbackScreenResolution.text = StaticVariables.Instance.ScreenResolution.ElementAt(PosRes).Key;
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

    public void CompleteMission()
    {
        PlayerScript PlayerWithQuest = StaticVariables.Instance.Players[StaticVariables.Instance.CurrentPlayer].GetComponent<PlayerScript>();
        if (PlayerWithQuest.Missions.Count > 0)
        {
            if (PlayerWithQuest.Missions.Any(Mission => StaticVariables.Instance.Missions[Mission].Key == PlayerWithQuest.CurrPosition) && PlayerWithQuest.Missions.Any(Mission => PlayerWithQuest.GetObjects().Contains(StaticVariables.Instance.Missions[Mission].Value)))
            {
                Debug.Log("You can complete a quest");
            }
            else if(PlayerWithQuest.Missions.Any(Mission => StaticVariables.Instance.Missions[Mission].Key == PlayerWithQuest.CurrPosition))
            {
                Debug.Log("You lack the necessary items");
            }
            else
            {
                Debug.Log("No quests here");
            }
        }
        else
        {
            Debug.Log("You dont have missions");
        }
    }

    public void MoveButton()
    {
        PlayerScript PlayerToMove = StaticVariables.Instance.Players[StaticVariables.Instance.CurrentPlayer].GetComponent<PlayerScript>();

        if (!PlayerToMove.IsMoving)
        {
            foreach (GameObject Square in Squares[PlayerToMove.CurrPosition])
            {
                Square.SetActive(true);
            }
            PlayerToMove.IsMoving = !PlayerToMove.IsMoving;
        }
        else
        {
            StaticVariables.Instance.DisableOutlines();
            PlayerToMove.IsMoving = !PlayerToMove.IsMoving;
        }
    }
    
    public void TakeObject()
    {
        PlayerScript PlayerToAdd = StaticVariables.Instance.Players[StaticVariables.Instance.CurrentPlayer].GetComponent<PlayerScript>();
        if(PlayerToAdd.IsMoving)
        {
            StaticVariables.Instance.DisableOutlines();
            PlayerToAdd.IsMoving = false;
        }
        
        if (PlayerToAdd.CurrPosition != "CentralPlaza")
        {
            PlayerToAdd.AddObject(PossibleObjects[PlayerToAdd.CurrPosition]);
            PlayerToAdd.GetObjects();
            ObjectObtained.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(PossibleObjects[PlayerToAdd.CurrPosition].ToString());
            StartCoroutine(AnimateObjectObtained());
            StaticVariables.Instance.DisableButtons();
        }
        else
        {
            Debug.Log("La plaza no da objetos!!!");
        }
    }

    private IEnumerator AnimateObjectObtained()
    {
        ObjectObtained.SetBool("Appearing", true);
        yield return new WaitForSeconds(1.8f);
        ObjectObtained.SetBool("Appearing", false);
    }
}
