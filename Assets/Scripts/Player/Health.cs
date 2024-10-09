using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] UnityEvent onKill;
    [SerializeField] UnityEvent onHit;
    
    public void TakeDamage(float amount)
    {
        health -= amount;
        onHit?.Invoke();
        
        if (health < 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        onKill.Invoke();
        Destroy(gameObject);
    }
}
