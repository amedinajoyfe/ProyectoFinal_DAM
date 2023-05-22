using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSquare : MonoBehaviour
{
    public void ClickOutline(GameObject objeto)
    {
        GameObject.Find("Player_1").GetComponent<PlayerScript>().Move(objeto.transform.position, objeto.transform.parent.name);
        StaticVariables.DisableOutlines();
        GameObject.Find("Player_1").GetComponent<PlayerScript>().IsMoving = false;
    }
}
