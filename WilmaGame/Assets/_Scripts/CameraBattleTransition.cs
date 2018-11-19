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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(Transition(1, 0, 1));
        }
    }

    public IEnumerator Transition(float duration, float from, float to)
    {
        float percent = 0;

        do
        {
            percent += Time.deltaTime / duration;
            float newCutoff = Mathf.Lerp(from, to, percent);
            TransitionMat.SetFloat("_Cutoff", newCutoff);
            yield return null;

        } while (percent < 1);

        // load level here
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, TransitionMat);
    }
}
