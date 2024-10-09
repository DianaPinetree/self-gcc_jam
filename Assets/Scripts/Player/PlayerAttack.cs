using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : AttackController
{
    private PlayerStats _stats;
    
    protected override void Start()
    {
        base.Start();
        _stats = GetComponentInParent<Player>().stats;
    }

    protected override void Update()
    {
        attackAccumulator += _stats.attackSpeedMod * Time.deltaTime;
        if (attackAccumulator >= 1.0f)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        base.Attack();
        int projCount = Mathf.FloorToInt(_stats.projectiles);
        for (int i = 0; i < projCount; i++)
        {
            var proj = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<ProjectileBehaviour>();
            proj.pierce = Mathf.FloorToInt(_stats.pierce);
            Vector3 dir = Aim.direction;
            dir = Quaternion.Euler(0, 0, Random.Range(-_stats.attackAccuracy, _stats.attackAccuracy)) * dir;
            proj.MovementDirection = dir * (_stats.weaponRange * Random.Range(0.95f, 1.1f));
        }
    }
}
