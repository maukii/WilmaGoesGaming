using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEncounter : MonoBehaviour
{

    public int LevelToLoad;
    public bool dead = false;
    public bool fading = false;
    SpriteRenderer sr;

    public int index = 1;

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    
    void Update()
    {
        switch(index)
        {
            case 1:
                dead = GameManager.instance.firstEnemyWon;
                break;

            case 2:
                dead = GameManager.instance.secondEnemyWon;
                break;
        }

        if (dead && !fading)
        {
            StartCoroutine(Die());
        }

    }

    IEnumerator Die()
    {
        fading = true;

        for (float f = 1; f >= -0.01f; f -= 0.01f)
        {
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return null;
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!dead)
        {
            PlayerMovement.interacting = true;
            Camera.main.GetComponent<CameraBattleTransition>().StartEncounter(1, 0, 1, LevelToLoad);
        }
    }

}
