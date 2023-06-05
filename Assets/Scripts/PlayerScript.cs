using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    List<Image> ItemImages = new List<Image>();

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

    public string[] GetObjects()
    {
        List<string> ReturnObjects = new List<string>();
        foreach (StaticVariables.Items item in _objects)
        {
            Debug.Log(item.ToString());
            ReturnObjects.Add(item.ToString());
        }
        return ReturnObjects.ToArray();
    }

    public void AddObject(StaticVariables.Items _obj)
    {
        if(_objects.Count < 3)
        {
            _objects.Add(_obj);
            ItemImages[_objects.Count - 1].sprite = Resources.Load<Sprite>(_obj.ToString());
        }
        else
        {
            Debug.Log("Has alcanzado el máximo de objetos");
        }
    }

    private void Start()
    {
        GameObject UiPicture = GameObject.Find(Name);
        Debug.Log(Name + ", Encontrado: " + GameObject.Find(Name));
        foreach (Transform child in UiPicture.transform)
        {
            ItemImages.Add(child.gameObject.GetComponent<Image>());
        }
    }

    public void Move(Vector2 position, string square)
    {
        this.gameObject.transform.position = position;
        _currPosition = square;
    }
}
