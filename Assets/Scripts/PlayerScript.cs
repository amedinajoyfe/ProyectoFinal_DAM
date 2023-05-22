using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
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
    public void Move(Vector2 position, string square)
    {
        this.gameObject.transform.position = position;
        _currPosition = square;
    }
}
