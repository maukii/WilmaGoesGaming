using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backdoor : MonoBehaviour
{

    public Dialogue dialogue;
    public bool canActivate = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(canActivate)
        {
            canActivate = false;
            DialogueManager.instance.StartConversation(dialogue);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        canActivate = true;
    }
}
