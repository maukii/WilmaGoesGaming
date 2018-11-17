using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{

    public AnimationCurve PositionCurve;
    public AnimationCurve ScaleCurve;

    public Vector2 startPos, endPos;
    public Vector2 startScale, endScale;

    public float effectTime = 1;

    public bool ready = false;

    void Start()
    {
        //SetupVariables(); // mayabe use later
        StartEncounter();
    }

    void SetupVariables()
    {
        endPos = transform.localPosition;
        endScale = transform.localScale;
    }

    public void StartEncounter()
    {
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        ready = false;

        transform.localPosition = startPos;
        transform.localScale = startScale;

        float time = 0;
        float percent = 0;
        float lastTime = Time.realtimeSinceStartup;

        do
        {
            time += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;

            percent = Mathf.Clamp01(time / effectTime);
            Vector2 tempScale = Vector2.LerpUnclamped(startScale, endScale, ScaleCurve.Evaluate(percent));
            Vector2 tempPos = Vector2.LerpUnclamped(startPos, endPos, PositionCurve.Evaluate(percent));

            transform.localPosition = tempPos;
            transform.localScale = tempScale;
            yield return null;

        } while (percent < 1);

        transform.localScale = endScale;
        transform.localPosition = endPos;
        ready = true;
    }

}