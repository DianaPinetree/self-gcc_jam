using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static void SetShake(float duration, float intensity)
    {
        if (instance == null)
        {
            Camera.main.gameObject.AddComponent<CameraShake>();
        }

        instance.shake.m_AmplitudeGain = intensity;
        instance.shakeTime = duration;
    }

    private static CameraShake instance;
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    private CinemachineVirtualCamera camTransform;
    private CinemachineBasicMultiChannelPerlin shake;
    private float shakeTime = 0f;
    Vector3 originalPos;
	
    void Awake()
    {
        camTransform = GetComponent<CinemachineVirtualCamera>();
        shake = camTransform.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        instance = this;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if (shakeTime <= 0)
            {
                shake.m_AmplitudeGain = 0f;
            }
        }
    }
}