using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] List<GameObject> interactables = new List<GameObject>();
    [SerializeField] KeyCode actionKey = KeyCode.Space;
    [SerializeField] GameObject Wilma;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<NPC>() != null && !interactables.Contains(other.gameObject))
        {
            interactables.Add(other.gameObject);
        }

        if(other.gameObject.GetComponent<Door>() != null)
        {
            other.gameObject.GetComponent<Door>().ChangeLevel(other.gameObject.GetComponent<Door>().levelIndexToLoad);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (interactables.Contains(other.gameObject))
        {
            interactables.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(actionKey) && interactables.Count != 0 && !PlayerMovement.interacting)
        {
            // interact with objects

            for (int i = 0; i < interactables.Count; i++)
            {
                PlayerMovement.interacting = true;
                interactables[i].GetComponent<NPC>().TriggerConversation();
            }
        }
        else if(Input.GetKeyDown(actionKey) && interactables.Count == 0 && !PlayerMovement.interacting)
        {
            // interact with Wilma
            NPC[] scripts = Wilma.GetComponents<NPC>();
            scripts[Random.Range(0, scripts.Length)].TriggerConversation();
        }

        if (Input.GetKeyDown(actionKey) && PlayerMovement.interacting && DialogueManager.instance.sentenceReady)
        {
            DialogueManager.instance.NextSentence();
        }
    }

}
