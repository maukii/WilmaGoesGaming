using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBattleTransition : MonoBehaviour {

    public Material TransitionMat;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, TransitionMat);
    }
}
