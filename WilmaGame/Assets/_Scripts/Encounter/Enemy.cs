using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Dialogue dialogue;
    public EncounterUI ui;

    void Start()
    {
        ui = FindObjectOfType<EncounterUI>();
    }

    public void TriggerConversation()
    {
        ui.ShowInfo(dialogue);
    }
}
