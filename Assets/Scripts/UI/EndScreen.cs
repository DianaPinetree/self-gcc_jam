using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public static EndScreen instance;
    [SerializeField] private TextMeshProUGUI time;
    private CanvasGroup root;
    private bool active = false;
    private void Awake()
    {
        instance = this;
        root = GetComponent<CanvasGroup>();
        root.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;
        
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            LevelLoader.instance.GoTo(0);
        }
    }

    public void Open()
    {
        TimeSpan gameTime = TimeSpan.FromSeconds(GameController.instance.elapsed);
        time.text = string.Format($"{gameTime.Minutes}:{gameTime.Seconds}");
        root.LeanAlpha(1f, 0.7f).setEaseInCubic().setOnComplete(() => active = true);
    }
}
