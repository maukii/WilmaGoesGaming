using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    public int levelIndexToLoad;

    public void ChangeLevel(int index)
    {
        LevelChanger.instance.FadeOut(index);
    }

    public void ChangeLevelTo(int index)
    {
        SceneManager.LoadScene(index);
    }

}
