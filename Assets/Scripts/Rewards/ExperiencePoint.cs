using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExperiencePoint : PickUp
{
    public int Amount;

    [SerializeField] private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    protected override IEnumerator Start()
    {
        if (Amount > 5)
        {
            _renderer.color = Color.blue;
        }
        return base.Start();
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.fixedDeltaTime * 10f);
            if (Vector3.Distance(transform.position, target.transform.position ) < target.hitboxRadius)
            {
                Add(target);
            }
        }
    }

    public override void Add(Player player)
    {
        player.AddXP(Amount);
        base.Add(player);
    }
}