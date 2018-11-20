using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBattleTransition : MonoBehaviour {

    public static CameraBattleTransition instance;

    public Material TransitionMat;

    void Awake()
    {
        TransitionMat.SetFloat("_Cutoff", 0);

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

    public void StartEncounter(float duration, float from, float to, int sceneIndex)
    {
        StartCoroutine(Transition(duration, from, to, sceneIndex));
    }

    IEnumerator Transition(float duration, float from, float to, int sceneIndex)
    {
        float percent = 0;

        do
        {
            percent += Time.deltaTime / duration;
            float newCutoff = Mathf.Lerp(from, to, percent);
            TransitionMat.SetFloat("_Cutoff", newCutoff);
            yield return null;

        } while (percent < 1);

        
        SceneManager.LoadScene(sceneIndex);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, TransitionMat);
    }
}
