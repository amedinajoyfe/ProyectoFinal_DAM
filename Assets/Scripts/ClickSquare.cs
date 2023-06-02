using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSquare : MonoBehaviour
{
    public void ClickOutline(GameObject objeto)
    {
        GameObject PlayerToMove = StaticVariables.Players[StaticVariables.CurrentPlayer];
        PlayerToMove.GetComponent<PlayerScript>().Move(objeto.transform.position, objeto.transform.parent.name);
        StaticVariables.DisableOutlines();
        PlayerToMove.GetComponent<PlayerScript>().IsMoving = false;
        StaticVariables.CurrentPlayer = StaticVariables.CurrentPlayer == StaticVariables.Players.Count - 1 ? 0 : StaticVariables.CurrentPlayer += 1;
    }
}
