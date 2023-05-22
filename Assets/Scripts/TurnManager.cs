using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    public BootGame ScriptBoot;
    void Start()
    {
        ScriptBoot.BootFinished += (sender, args) => { Placeholder(); };
    }

    private void Placeholder()
    {

    }
}
