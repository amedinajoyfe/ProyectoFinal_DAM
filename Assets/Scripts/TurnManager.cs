using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public BootGame ScriptBoot;
    public GameObject ChangeTurnSign;

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
            Turns = 2;
        }
        else if (RandomChance <= 25)
        {
            Turns = 3;
        }
        else if (RandomChance <= 50)
        {
            Turns = 4;
        }
        else
        {
            Turns = 5;
        }


        return Turns;
    }

    public void PassTurn(PlayerScript PlayerToChange)
    {
        int CurrPlayer = StaticVariables.Instance.CurrentPlayer;
        var PlayerList = StaticVariables.Instance.Players;

        if (PlayerToChange.TurnsLeft == 1)
        {
            PlayerList[CurrPlayer].GetComponent<PlayerScript>().AddMission(StaticVariables.Instance.Missions.ElementAt(Random.Range(0, 5)).Key);
            StaticVariables.Instance.CurrentPlayer = CurrPlayer == PlayerList.Count - 1 ? 0 : CurrPlayer += 1;
            PlayerList[StaticVariables.Instance.CurrentPlayer].GetComponent<PlayerScript>().TurnsLeft = RollActions();
            ChangeTurnSign.transform.GetChild(0).GetComponent<TMP_Text>().text = "Turno del jugador " + PlayerList[StaticVariables.Instance.CurrentPlayer].GetComponent<PlayerScript>().Colour;
            StartCoroutine(AnimateTurnPass());
            StaticVariables.Instance.DisableButtons();
        }
        else
        {
            PlayerToChange.TurnsLeft -= 1;
        }
    }

    private IEnumerator AnimateTurnPass()
    {
        ChangeTurnSign.GetComponent<Animator>().SetBool("StartedAnimation", true);
        yield return new WaitForSeconds(2.06f);
        ChangeTurnSign.GetComponent<Animator>().SetBool("StartedAnimation", false);
    }
}
