using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterUI : MonoBehaviour
{
    public int winningIndex = 0;

    AnimationScript AS;

    public int levelToLoadIndex = 2;
    public GameObject encounter;

    public Text Action, Investigate, Run;
    public Text Info;
    public Text ActLeft, ActRight;

    private string actionTemp, investigateTemp, runTemp;
    private string leftTemp, rightTemp;
    public float typingSpeed;
    public int index;
    public bool choosingAction;

    bool canInteract;
    float timer = 0.15f;
    float tempTime;

    public bool infoReady = false;
    public bool chosen = false;

    public Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        AS = GetComponent<AnimationScript>();

        ActLeft.gameObject.SetActive(false);
        ActRight.gameObject.SetActive(false);

        Info.text = "";
        tempTime = timer;
        actionTemp = Action.text;
        investigateTemp = Investigate.text;
        runTemp = Run.text;

        leftTemp = ActLeft.text;
        rightTemp = ActRight.text;

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

            if(!chosen && !choosingAction)
            {
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
            }
            else if(choosingAction)
            {
                if (Input.GetAxisRaw("Horizontal") != 0 && canInteract)
                {
                    index++;
                    canInteract = false;
                    ChangeAction();
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && !chosen && !infoReady)
            {
                canInteract = false;
                chosen = true;
                StopAllCoroutines();
                Choose();
            }
            else if(Input.GetKeyDown(KeyCode.Space) && infoReady && canInteract)
            {
                NextSentence();
            }
            else if(Input.GetKeyDown(KeyCode.Space) && choosingAction && canInteract)
            {
                ChooseActionChoise();
            }

        }
    }

    private void ChooseActionChoise()
    {
        if(index == 0)
        {
            encounter.GetComponent<ActTexts>().LeftChoise();
        }
        else
        {
            encounter.GetComponent<ActTexts>().RightChoise();
        }

        if(index == winningIndex)
        {
            if(GameManager.instance != null)
                GameManager.instance.firstEnemyWon = true;
        }

        ActLeft.gameObject.SetActive(false);
        ActRight.gameObject.SetActive(false);

    }

    void ChangeAction()
    {
        canInteract = false;

        if (index > 1)
        {
            index = 0;
        }
        else if (index < 0)
        {
            index = 1;
        }

        switch(index)
        {
            case 0:
                ActLeft.text = "> " + leftTemp;
                ActRight.text = rightTemp;
                break;

            case 1:
                ActLeft.text = leftTemp;
                ActRight.text = "> " + rightTemp;
                break;
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
            case 0: // action
                ChooseAction();
                break;

            case 1: // investigate
                ChooseInvestigate();
                break;

            case 2: // RUN
                ChooseRun();
                break;

            default:
                print("choosing index broke");
                break;
        }
    }

    public void ChooseAction()
    {
        choosingAction = true;

        ActLeft.gameObject.SetActive(true);
        ActRight.gameObject.SetActive(true);

        Action.gameObject.SetActive(false);
        Investigate.gameObject.SetActive(false);
        Run.gameObject.SetActive(false);

        ChangeAction();
    }

    public void ChooseInvestigate()
    {
        Action.gameObject.SetActive(false);
        Investigate.gameObject.SetActive(false);
        Run.gameObject.SetActive(false);

        encounter.GetComponent<Enemy>().TriggerConversation();
    }

    public void ChooseRun()
    {
        PlayerMovement.interacting = false;
        LevelChanger.instance.FadeOut(levelToLoadIndex);
    }

    public void ShowInfo(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            Debug.Log(sentence);
            sentences.Enqueue(sentence);
        }

        NextSentence();
    }

    IEnumerator PrintInfo(string sentence, float typingSpeed)
    {
        Info.gameObject.SetActive(true);
        Info.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            Info.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        infoReady = true;
    }

    public void NextSentence()
    {
        if (sentences.Count == 0)
        {
            EndInfo();
            return;
        }

        infoReady = false;
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(PrintInfo(sentence, typingSpeed));
    }

    void EndInfo()
    {
        infoReady = false;
        Debug.Log("End of info");
        Info.text = "";
        Action.gameObject.SetActive(true);
        Investigate.gameObject.SetActive(true);
        Run.gameObject.SetActive(true);

        Info.gameObject.SetActive(false);
        chosen = false;

        if(choosingAction)
        {
            PlayerMovement.interacting = false;
            LevelChanger.instance.FadeOut(levelToLoadIndex);
        }
    }

}
