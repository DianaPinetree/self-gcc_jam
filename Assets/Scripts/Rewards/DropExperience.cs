using System;
using System.Collections.Generic;
using UnityEngine;

public class DropExperience : MonoBehaviour
{
    [SerializeField] private int dropAmount;
    [SerializeField] private GameObject prefab;

    public void Drop()
    {
        for (int i = 0; i < dropAmount; i++)
        {
            GameObject expPointsGO = Instantiate(prefab, transform.position, Quaternion.identity);
            ExperiencePoint xpScript = expPointsGO.GetComponent<ExperiencePoint>();
        }
    }
}