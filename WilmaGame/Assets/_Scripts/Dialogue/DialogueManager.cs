using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    #region Variables

    public static DialogueManager instance;
    public Text nameText, dialogueText;
    public Queue<string> sentences;
    public float typingSpeed = 0.02f;
    public bool sentenceReady;

    private float tempTypingSpeed;

    public Animator dialogueUI;

    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        tempTypingSpeed = typingSpeed;
        sentences = new Queue<string>();
    }

    public void StartConversation(Dialogue dialogue)
    {
        dialogueUI.SetTrigger("Enter");
        PlayerMovement.interacting = true;
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        NextSentence();
    }

    public void NextSentence()
    {

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        sentenceReady = false;
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, typingSpeed));
    }

    IEnumerator TypeSentence(string sentence, float typingSpeed)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        sentenceReady = true;

    }

    private void EndDialogue()
    {
        dialogueUI.SetTrigger("Exit");
        PlayerMovement.interacting = false;
        sentenceReady = false;
        Debug.Log("End of conversation");
        dialogueText.text = "";
        nameText.text = "";
    }
}
