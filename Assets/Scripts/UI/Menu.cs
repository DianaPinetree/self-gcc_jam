using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;

    private void Start()
    {
       LeanTween.scale(title.gameObject, Vector3.one * 2.5f, 3f).setLoopPingPong(-1).setEaseInOutCubic();
       LeanTween.value(title.gameObject, 1f, -1f, 1f).setOnUpdate((float value) => 
           { title.characterSpacing = value;}).setLoopPingPong(-1).setEaseOutCubic();
       
       title.transform.Rotate(new Vector3(0, 0, -10f));
       LeanTween.rotateZ(title.gameObject, 10f, 2.3f).setLoopPingPong(-1).setEaseInOutCubic();
       
    }

    private void OnDisable()
    {
        LeanTween.cancel(title.gameObject);
    }

    private void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            LevelLoader.instance.GoTo(1);
        }
    }
}
