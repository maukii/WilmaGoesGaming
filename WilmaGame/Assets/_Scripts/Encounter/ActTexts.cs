using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActTexts : MonoBehaviour
{

    public Dialogue leftInfo, rightInfo;
    public EncounterUI ui;

    void Start()
    {
        ui = FindObjectOfType<EncounterUI>();
    }

    public void LeftChoise()
    {
        ui.ShowInfo(leftInfo);
    }

    public void RightChoise()
    {
        ui.ShowInfo(rightInfo);
    }
}
