using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    private CanvasGroup fader;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        fader = GetComponentInChildren<CanvasGroup>();
    }

    public void GoTo(int sceneIndex)
    {
        StartCoroutine(LoadLevel(sceneIndex));
    }

    private IEnumerator LoadLevel(int index)
    {
        fader.LeanAlpha(1f, 0.7f).setEaseOutCubic();

        yield return new WaitForSecondsRealtime(0.7f);
        AsyncOperation op = SceneManager.LoadSceneAsync(index);
        while (!op.isDone)
        {
            yield return null;
        }


        fader.LeanAlpha(0f, 0.6f).setEaseInCubic();
    }
}
