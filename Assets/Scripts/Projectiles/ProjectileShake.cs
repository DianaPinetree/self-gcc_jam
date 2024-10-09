using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShake : MonoBehaviour
{
    [SerializeField] private float cameraShake = 0.3f;
    [SerializeField] private float cameraShakeDuration = 0.4f;
    private ProjectileBehaviour _behaviour;
    
    private void Awake()
    {
        _behaviour = GetComponent<ProjectileBehaviour>();
    }

    private void OnEnable()
    {
        _behaviour.OnContactWithEnemy += Hit;
    }

    private void OnDisable()
    {
        _behaviour.OnContactWithEnemy -= Hit;
    }

    public void Hit(Transform t)
    {
        CameraShake.SetShake(cameraShakeDuration, cameraShake);
    }
}
