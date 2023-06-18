using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    List<Image> ItemImages = new List<Image>();
    List<Image> NumberImages = new List<Image>();
    public List<string> Missions = new List<string>();
    public List<GameObject> MissionPrefabs = new List<GameObject>();
    public List<string> Cards = new List<string>();
    public List<GameObject> CardPrefabs = new List<GameObject>();

    Animator BoneUI;
    Image NumberSign;
    GameObject UiPicture;
    Image PointCounter;

    public int CoordinateX;
    public int CoordinateY;

    public int Points { get; set; }
    public string Colour { get; set; }

    public int PlayerNumber { get; set; }

    private int _turnsLeft;
    public int TurnsLeft
    {
        get
        {
            return _turnsLeft;
        }
        set
        {
            _turnsLeft = value;
            if (NumberSign != null)
            {
                NumberSign.sprite = Resources.Load<Sprite>("Sign" + TurnsLeft.ToString());
            }
        }
    }
    public string Name { get; set; }

    private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        set
        {
            _isMoving = value;
        }
    }

    private string _currPosition = "CentralPlaza";
    public string CurrPosition
    {
        get
        {
            return _currPosition;
        }
        set
        {
            _currPosition = value;
        }
    }

    public List<StaticVariables.Items> _objects = new List<StaticVariables.Items>();

    public int AddMission(string mission)
    {
        if (Missions.Count > 2)
        {
            Debug.Log("You already have missions");
            return -2;
        }

        if (Missions.Contains(mission))
        {
            return -1;
        }

        GameObject NewMission = Instantiate(StaticVariables.Instance.MissionPrefab);
        NewMission.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(StaticVariables.Instance.Missions[mission].Value.ToString());
        NewMission.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(StaticVariables.Instance.Missions[mission].Key);
        NewMission.transform.GetChild(2).GetComponent<TMP_Text>().text = mission;
        NewMission.transform.SetParent(UiPicture.transform.GetChild(4));

        RectTransform rectTransform;
        Vector2 StartingPos;
        rectTransform = NewMission.GetComponent<RectTransform>();


        if (Missions.Count == 0)
        {
            StartingPos = new Vector2(-370, 0);
        }
        else if (Missions.Count == 1)
        {
            StartingPos = new Vector2(0, 0);
        }
        else
        {
            StartingPos = new Vector2(370, 0);
        }

        rectTransform.localPosition = StartingPos;
        rectTransform.localScale = new Vector2(1.1f, 1.1f);
        Missions.Add(mission);
        MissionPrefabs.Add(NewMission);

        return 0;
    }

    public int AddCard(string card)
    {
        if (Cards.Count > 2)
        {
            Debug.Log("You already have cards");
            return -2;
        }

        if (Cards.Contains(card))
        {
            return -1;
        }

        GameObject NewCard = Instantiate(StaticVariables.Instance.CardPrefab);

        Debug.Log(card);

        NewCard.GetComponent<Image>().sprite = Resources.Load<Sprite>(card);
        NewCard.transform.SetParent(UiPicture.transform.GetChild(5));

        RectTransform rectTransform;
        Vector2 StartingPos;
        rectTransform = NewCard.GetComponent<RectTransform>();


        if (Cards.Count == 0)
        {
            StartingPos = new Vector2(-370, 0);
        }
        else if (Cards.Count == 1)
        {
            StartingPos = new Vector2(0, 0);
        }
        else
        {
            StartingPos = new Vector2(370, 0);
        }

        rectTransform.localPosition = StartingPos;
        rectTransform.localScale = new Vector2(1f, 1f);
        Cards.Add(card);
        CardPrefabs.Add(NewCard);

        return 0;
    }

    public List<StaticVariables.Items> GetObjects()
    {
        return _objects;
    }

    public bool AddObject(StaticVariables.Items _obj)
    {
        if (_objects.Count < 3)
        {
            _objects.Add(_obj);
            ItemImages[_objects.Count - 1].sprite = Resources.Load<Sprite>(_obj.ToString());
            TurnManager.Instance.PassTurn(this);
            return true;
        }
        else
        {
            Debug.Log("Has alcanzado el máximo de objetos");
            return false;
        }
    }

    private void RemoveObject(StaticVariables.Items _obj)
    {
        if (_objects.IndexOf(_obj) == 0)
        {
            if (_objects.Count > 2)
            {
                ItemImages[_objects.IndexOf(_obj)].sprite = ItemImages[_objects.IndexOf(_obj) + 1].sprite;
                ItemImages[_objects.IndexOf(_obj) + 1].sprite = ItemImages[_objects.IndexOf(_obj) + 2].sprite;
                ItemImages[_objects.IndexOf(_obj) + 2].sprite = Resources.Load<Sprite>("None");
            }
            else if (_objects.Count > 1)
            {
                ItemImages[_objects.IndexOf(_obj)].sprite = ItemImages[_objects.IndexOf(_obj) + 1].sprite;
                ItemImages[_objects.IndexOf(_obj) + 1].sprite = Resources.Load<Sprite>("None");
            }
            else
            {
                ItemImages[_objects.IndexOf(_obj)].sprite = Resources.Load<Sprite>("None");
            }
        }
        else if (_objects.IndexOf(_obj) == 1)
        {
            if (_objects.Count > 2)
            {
                ItemImages[_objects.IndexOf(_obj)].sprite = ItemImages[_objects.IndexOf(_obj) + 1].sprite;
                ItemImages[_objects.IndexOf(_obj) + 1].sprite = Resources.Load<Sprite>("None");
            }
            else
            {
                ItemImages[_objects.IndexOf(_obj)].sprite = Resources.Load<Sprite>("None");
            }
        }
        else
        {
            ItemImages[_objects.IndexOf(_obj)].sprite = Resources.Load<Sprite>("None");
        }

        _objects.Remove(_obj);
    }

    private IEnumerator PlayBoneAnimation()
    {
        BoneUI.enabled = true;
        yield return new WaitForSeconds(1f);
        BoneUI.enabled = false;
    }

    public void CompleteMission(int missionIndex)
    {
        AddPoints(1);
        if (Points == 5)
        {
            StaticVariables.Instance.EndGame(this.Name);
            return;
        }

        if (missionIndex == 0)
        {
            if (MissionPrefabs.Count > 1)
                MissionPrefabs.ElementAt(missionIndex + 1).GetComponent<RectTransform>().localPosition = new Vector2(-370, 0);
            if (MissionPrefabs.Count > 2)
                MissionPrefabs.ElementAt(missionIndex + 2).GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        }
        else if (missionIndex == 1)
        {
            if (MissionPrefabs.Count > 2)
                MissionPrefabs.ElementAt(missionIndex + 1).GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        }
        StartCoroutine(CompleteMissionAnim(missionIndex));

        RemoveObject(StaticVariables.Instance.Missions[Missions.ElementAt(missionIndex)].Value);

        Missions.RemoveAt(missionIndex);

        TurnManager.Instance.PassTurn(this);
    }

    public IEnumerator CompleteMissionAnim(int missionIndex)
    {
        if (StaticVariables.Instance.Animate)
        {
            MissionPrefabs.ElementAt(missionIndex).transform.SetParent(GameObject.Find("Canvas").transform);
            MissionPrefabs.ElementAt(missionIndex).SetActive(true);
            MissionPrefabs.ElementAt(missionIndex).GetComponent<Animator>().enabled = true;

            StaticVariables.Instance.DisableButtons(true, 2.5f);

            yield return new WaitForSeconds(2.5f);
        }

        Destroy(MissionPrefabs.ElementAt(missionIndex));
        MissionPrefabs.RemoveAt(missionIndex);
    }

    public void UseCard(int cardIndex)
    {
        if (Points == 5)
        {
            StaticVariables.Instance.EndGame(this.Name);
            return;
        }

        if (cardIndex == 0)
        {
            if (CardPrefabs.Count > 1)
                CardPrefabs.ElementAt(cardIndex + 1).GetComponent<RectTransform>().localPosition = new Vector2(-370, 0);
            if (CardPrefabs.Count > 2)
                CardPrefabs.ElementAt(cardIndex + 2).GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        }
        else if (cardIndex == 1)
        {
            if (CardPrefabs.Count > 2)
                CardPrefabs.ElementAt(cardIndex + 1).GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        }

        StartCoroutine(UseCardAnim(cardIndex));

        Cards.RemoveAt(cardIndex);
    }

    public IEnumerator UseCardAnim(int cardIndex)
    {
        if (StaticVariables.Instance.Animate)
        {
            CardPrefabs.ElementAt(cardIndex).transform.SetParent(GameObject.Find("Canvas").transform);
            CardPrefabs.ElementAt(cardIndex).SetActive(true);
            CardPrefabs.ElementAt(cardIndex).GetComponent<Animator>().enabled = true;

            StaticVariables.Instance.DisableButtons(true, 2.5f);

            yield return new WaitForSeconds(2.5f);
        }

        Destroy(CardPrefabs.ElementAt(cardIndex));
        CardPrefabs.RemoveAt(cardIndex);
    }

    public void MissionBoard()
    {
        UiPicture.transform.GetChild(4).gameObject.SetActive(!UiPicture.transform.GetChild(4).gameObject.activeSelf);
        UiPicture.transform.GetChild(5).gameObject.SetActive(false);
    }

    public void CardsBoard()
    {
        UiPicture.transform.GetChild(5).gameObject.SetActive(!UiPicture.transform.GetChild(5).gameObject.activeSelf);
        UiPicture.transform.GetChild(4).gameObject.SetActive(false);
    }

    private void Start()
    {
        NumberSign = GameObject.Find("ActionsLeft").GetComponent<Image>();
        NumberSign.sprite = Resources.Load<Sprite>("Sign" + StaticVariables.Instance.Players[StaticVariables.Instance.CurrentPlayer].GetComponent<PlayerScript>().TurnsLeft.ToString());
        UiPicture = GameObject.Find(Name);
        PointCounter = UiPicture.transform.GetChild(3).GetChild(1).GetComponent<Image>();

        foreach (Transform child in UiPicture.transform)
        {
            if(child.gameObject.name.Contains("Item"))
            {
                ItemImages.Add(child.gameObject.GetComponent<Image>());
            }
            if(child.gameObject.name == "Background")
            {
                BoneUI = child.gameObject.GetComponentsInChildren<Animator>()[0];
            }
        }
    }

    public void AddPoints(int points)
    {
        this.Points += points;
        PointCounter.sprite = Resources.Load<Sprite>("SignScore" + Points);
        StartCoroutine(PlayBoneAnimation());

        if(this.Points == 5)
            StaticVariables.Instance.EndGame(this.Name);
    }

    public void Move(Vector2 position, string square)
    {
        Vector2 newPos = new Vector2(position.x + (this.CoordinateX / 1.6f), position.y + (this.CoordinateY / 1.6f)); 
        this.gameObject.transform.position = newPos;
        _currPosition = square;
    }

    #region DebugActions
    public void AddPointsOnDemand()
    {
        AddPoints(1);
    }
    public void RemovePointsOnDemand()
    {
        AddPoints(-1);
    }
    public void PassTurnOnCommand()
    {
        TurnManager.Instance.PassTurn(this);
    }
    public void AddTurnOnCommand()
    {
        TurnManager.Instance.PassTurn(this, -1);
    }
    public void RemoveAllObjects()
    {
        var Total = _objects.Count;
        if(Total > 0)
            AddPoints(1);

        for (int i = 0; i < Total; i++)
        {
            Debug.Log("Borrando " + i + " objetos");
            RemoveObject(_objects[0]);
        }
    }
    public void RemoveAllMissions()
    {
        var Total = Missions.Count;
        if (Total > 0)
            AddPoints(1);

        for (int i = 0; i < Total; i++)
        {
            Missions.RemoveAt(0);
            Destroy(MissionPrefabs.ElementAt(0));
            MissionPrefabs.RemoveAt(0);
        }
    }
    #endregion
}
