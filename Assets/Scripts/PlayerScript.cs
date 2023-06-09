using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    List<Image> ItemImages = new List<Image>();
    List<Image> NumberImages = new List<Image>();
    public List<string> Missions = new List<string>();

    Image NumberSign;
    GameObject UiPicture;
    Image PointCounter;

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
            if(NumberSign != null)
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

    public void AddMission(string mission)
    {
        Missions.Add(mission);

        foreach(string miss in Missions)
        {
            Debug.Log(miss);
        }
    }

    public List<StaticVariables.Items> GetObjects()
    {
        return _objects;
    }

    public void AddObject(StaticVariables.Items _obj)
    {
        if(_objects.Count < 3)
        {
            _objects.Add(_obj);
            ItemImages[_objects.Count - 1].sprite = Resources.Load<Sprite>(_obj.ToString());
            TurnManager.Instance.PassTurn(this);
        }
        else
        {
            Debug.Log("Has alcanzado el máximo de objetos");
        }
    }

    private void Start()
    {
        NumberSign = GameObject.Find("ActionsLeft").GetComponent<Image>();
        NumberSign.sprite = Resources.Load<Sprite>("Sign" + StaticVariables.Instance.Players[StaticVariables.Instance.CurrentPlayer].GetComponent<PlayerScript>().TurnsLeft.ToString());
        UiPicture = GameObject.Find(Name);
        PointCounter = UiPicture.transform.GetChild(3).GetChild(1).GetComponent<Image>();

        foreach (Transform child in UiPicture.transform)
        {
            ItemImages.Add(child.gameObject.GetComponent<Image>());
        }
    }

    public void AddPoints(int points)
    {
        this.Points += points;
        PointCounter.sprite = Resources.Load<Sprite>("SignScore" + Points);
    }

    public void Move(Vector2 position, string square)
    {
        this.gameObject.transform.position = position;
        _currPosition = square;
    }
}
