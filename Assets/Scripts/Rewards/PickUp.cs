using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickUp : MonoBehaviour
{
    public int SpatialGroup = 0;
    protected Player target = null;

    private bool collected = false;
    // spawn
    protected virtual IEnumerator Start()
    {
        Vector3 point =  Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector3.one;
        point.Normalize();
        float speed = 40f;
        float time = Random.Range(0.2f, 0.5f);
        while (time > 0)
        {
            time -= Time.deltaTime;
            transform.position += point * (Time.deltaTime * speed);
            speed *= Mathf.Pow(0.0001f, Time.deltaTime);
            yield return null;
        }
        
        SpatialGroup = GameController.instance.GetSpatialGroupStatic(transform.position.x , transform.position.y); // GET spatial group
        GameController.instance.pickupsSpacialGroup[SpatialGroup].Add(this);
    }

    public virtual void Collect(Player target)
    {
        if (collected) return;
        
        collected = true;
        this.target = target;
        Debug.Log("Collect Pickup");
        GameController.instance.pickupsSpacialGroup[SpatialGroup].Remove(this);
    }

    public virtual void Add(Player player)
    {
        Destroy(gameObject);
    }
}