using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected float attackRate;
    [SerializeField] protected float speed;
    [SerializeField] protected int pierce;
    protected float attackAccumulator;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        attackAccumulator = 0f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        attackAccumulator += attackRate * Time.deltaTime;
        if (attackAccumulator > 1.0f)
        {
            Attack();
        }
    }

    public virtual void Attack()
    {
        attackAccumulator = 0f;
    }
}
