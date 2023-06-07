using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public BootGame ScriptBoot;
    public  Animator ChangeTurnSign;

    private bool ChangingTurn;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        ChangeTurnSign = GameObject.Find("TextTurnIndicator").GetComponent<Animator>();
        ScriptBoot.BootFinished += () => { ChooseTurn(); };
    }

    private void ChooseTurn()
    {
        StaticVariables.Instance.CurrentPlayer = Random.Range(0, StaticVariables.Instance.Players.Count);

        StaticVariables.Instance.Players[StaticVariables.Instance.CurrentPlayer].GetComponent<PlayerScript>().TurnsLeft = RollActions();
    }

    private int RollActions()
    {
        int RandomChance = Random.Range(1, 60);
        int Turns;

        if (RandomChance <= 5)
        {
            Turns = 1;
        }
        else if (RandomChance <= 25)
        {
            Turns = 2;
        }
        else if (RandomChance <= 50)
        {
            Turns = 3;
        }
        else
        {
            Turns = 4;
        }


        return Turns;
    }

    public void PassTurn(PlayerScript PlayerToChange)
    {
        if (PlayerToChange.TurnsLeft == 1)
        {
            StaticVariables.Instance.CurrentPlayer = StaticVariables.Instance.CurrentPlayer == StaticVariables.Instance.Players.Count - 1 ? 0 : StaticVariables.Instance.CurrentPlayer += 1;
            StaticVariables.Instance.Players[StaticVariables.Instance.CurrentPlayer].GetComponent<PlayerScript>().TurnsLeft = RollActions();
        }
        else
        {
            PlayerToChange.TurnsLeft -= 1;
        }
    }
}
