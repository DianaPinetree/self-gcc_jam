using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private const float FLASH_TIME = 0.1f;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private SpriteRenderer sprite;

    private Color startColor;
    private float flash = 0f;
    private void OnEnable()
    {
        startColor = sprite.color;
    }

    public void Run()
    {
        flash = FLASH_TIME;
        AudioPlayer.PlayAudio(hitClip, new Vector2(0.9f, 1.1f), new  Vector2(.8f, 1.2f));
    }

    private void Update()
    {
        if (flash > 0)
        {
            flash -= Time.deltaTime;
            sprite.color = Color.white;
            if (flash <= 0)
            {
                sprite.color = startColor;
            }
        }
    }
}
