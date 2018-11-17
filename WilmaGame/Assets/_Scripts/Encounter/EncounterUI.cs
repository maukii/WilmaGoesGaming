using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterUI : MonoBehaviour
{
    AnimationScript AS;

    public Text Action, Investigate, Run;
    private string actionTemp, investigateTemp, runTemp;

    public int index;

    bool canInteract;
    float timer = 0.15f;
    float tempTime;

    void Start()
    {
        AS = GetComponent<AnimationScript>();

        tempTime = timer;

        actionTemp = Action.text;
        investigateTemp = Investigate.text;
        runTemp = Run.text;

        ChangeNode(); // do this so ">" shows before getting 1st input
    }

    void Update()
    {
        if (AS.ready) // dont accept input before animation is ready
        {
            if (!canInteract)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    canInteract = true;
                    timer = tempTime;
                }
            }

            if (Input.GetAxisRaw("Horizontal") < 0 && canInteract)
            {
                index--;
                canInteract = false;
                ChangeNode();
            }
            else if (Input.GetAxisRaw("Horizontal") > 0 && canInteract)
            {
                index++;
                canInteract = false;
                ChangeNode();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Choose();
            }

        }
    }

    void ChangeNode()
    {

        if (index > 2)
        {
            index = 0;
        }
        else if (index < 0)
        {
            index = 2;
        }

        switch (index)
        {
            case 0:
                Action.text = "> " + actionTemp;
                Investigate.text = investigateTemp;
                Run.text = runTemp;
                break;

            case 1:
                Action.text = actionTemp;
                Investigate.text = "> " + investigateTemp;
                Run.text = runTemp;
                break;

            case 2:
                Action.text = actionTemp;
                Investigate.text = investigateTemp;
                Run.text = "> " + runTemp;
                break;

            default:
                print("changing node broke");
                break;
        }
    }

    void Choose()
    {
        switch (index)
        {
            case 0:

                break;

            case 1:

                break;

            case 2:

                break;

            default:
                print("choosing index broke");
                break;
        }
    }

}
