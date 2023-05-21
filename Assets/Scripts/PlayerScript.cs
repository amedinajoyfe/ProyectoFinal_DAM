using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private string _currPosition;
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
    public void Move(Vector2 position)
    {
        Debug.Log(position);
        this.gameObject.transform.position = position;
    }
}
