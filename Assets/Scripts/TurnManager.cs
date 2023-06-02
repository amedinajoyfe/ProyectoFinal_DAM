using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public BootGame ScriptBoot;
    void Awake()
    {
        ScriptBoot.BootFinished += () => { ChooseTurn(); };
    }

    private void ChooseTurn()
    {
        StaticVariables.CurrentPlayer = Random.Range(0, StaticVariables.Players.Count);
    }
}
