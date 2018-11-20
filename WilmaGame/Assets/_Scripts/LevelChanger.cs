using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    Animator anim;
    public static LevelChanger instance;
    int levelToLoad;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);

        if (scene.name != "_Preload")
        {
            FadeIn();
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void FadeIn()
    {
        anim.SetTrigger("In");
    }

    public void FadeOut(int index)
    {
        levelToLoad = index;
        anim.SetTrigger("Out");
    }

    public void FadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        anim = GetComponentInChildren<Animator>();
    }

}
