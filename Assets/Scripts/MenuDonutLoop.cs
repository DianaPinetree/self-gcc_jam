using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MenuDonutLoop : MonoBehaviour
{
    [SerializeField] private float rotSpeed;
    [FormerlySerializedAs("startcolor")] [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private Vector2 scaleRange;

    private Vector3 startScale;
    private Vector3 endScale;

    private SpriteRenderer[] loops;
    // Start is called before the first frame update
    void Start()
    {
        loops = new SpriteRenderer[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            var spriteRenderer = transform.GetChild(i).GetComponent<SpriteRenderer>();
            loops[i] = spriteRenderer;
        }
        startScale = new Vector3(scaleRange.x, scaleRange.x);
        endScale = new Vector3(scaleRange.y, scaleRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        MoveDonuts();
    }

    public void MoveDonuts()
    {
        for (int i = 0; i < loops.Length; i++)
        {
            float step = (float)i / (float)transform.childCount;
            var child = loops[i];
            child.transform.Rotate(new Vector3(0, 0, rotSpeed) * Time.deltaTime);
            // child.transform.localScale += Vector3.one * (Time.deltaTime * 0.2f);
            // float t = child.transform.localScale.sqrMagnitude / endScale.sqrMagnitude;
            // child.color = Color.Lerp(endColor, startColor, 1 - t);
            // if (child.transform.localScale.sqrMagnitude > endScale.sqrMagnitude)
            // {
            //     child.transform.localScale = startScale;
            //     child.color = startColor;
            // }
        }
    }
}
