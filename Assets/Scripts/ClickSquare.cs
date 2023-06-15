using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSquare : MonoBehaviour
{
    public void ClickOutline(GameObject objeto)
    {
        PlayerScript PlayerToMove = StaticVariables.Instance.Players[StaticVariables.Instance.CurrentPlayer].GetComponent<PlayerScript>(); ;
        PlayerToMove.Move(objeto.transform.position, objeto.transform.parent.name);
        StaticVariables.Instance.DisableOutlines();
        PlayerToMove.IsMoving = false;
        //TurnManager.Instance.PassTurn(PlayerToMove);
    }
}
