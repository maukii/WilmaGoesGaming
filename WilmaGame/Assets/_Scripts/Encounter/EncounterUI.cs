using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterUI : MonoBehaviour
{
    AnimationScript AS;

    public GameObject encounter;

    public Text Action, Investigate, Run, Info;
    private string actionTemp, investigateTemp, runTemp;

    public float typingSpeed;
    public int index;

    bool canInteract;
    float timer = 0.15f;
    float tempTime;

    bool infoReady = false;
    public bool chosen = false;

    public Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();

        Info.text = "";

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

            if(!chosen)
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

            if (Input.GetKeyDown(KeyCode.Space) && !chosen && !infoReady)
            {
                chosen = true;
                StopAllCoroutines();
                Choose();
            }
            else if(Input.GetKeyDown(KeyCode.Space) && infoReady)
            {
                NextSentence();
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
            case 0: // action

                break;

            case 1: // investigate
                Action.gameObject.SetActive(false);
                Investigate.gameObject.SetActive(false);
                Run.gameObject.SetActive(false);

                encounter.GetComponent<Enemy>().TriggerConversation();
                break;

            case 2: // RUN
                print("tryna run");
                break;

            default:
                print("choosing index broke");
                break;
        }
    }

    public void ShowInfo(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
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
    }

}
