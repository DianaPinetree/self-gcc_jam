using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCircle : MonoBehaviour
{
    private SpriteRenderer _renderer;
    [SerializeField] private float range = 10f;
    [SerializeField] private float timeBeforeTarget;
    [SerializeField] private Color dangerColor;
    [SerializeField] private Color defaultColor;
    private Transform target;
    private string state = "wait";

    private float waitTimer = -1;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        target = GameObject.FindWithTag("Player").transform;
        stateCheck = WaitCheck;
    }

    private Action stateCheck;

    private void WaitCheck()
    {
        if (target == null) return;
        
        if (Vector3.Distance(target.position, transform.position) > range)
        {
            SetState("trigger");
        }
    }

    private void TriggerCheck()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                SetState("follow");
            }
        }
    }

    private void FollowCheck()
    {
        if (Vector3.Distance(target.position, transform.position) < 0.1f)
        {
            if (reached != null)
            {
                reached();
            }
            SetState("wait");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        stateCheck();
    }

    public void SetState(string newState)
    {
        if (state == newState) return;
        Debug.Log("Set State: " + newState);
        LeanTween.cancel(_renderer.gameObject);
        state = newState;
        switch (state)
        {
            case "trigger":
                LeanTween.color(_renderer.gameObject, dangerColor, 0.8f);
                waitTimer = UnityEngine.Random.Range(timeBeforeTarget, timeBeforeTarget + timeBeforeTarget / 2f);
                stateCheck = TriggerCheck;
                break;
            case "follow":
                LeanTween.move(gameObject, target, 0.3f).setEaseOutBounce();
                GameController.instance.FastTrackWave();
                stateCheck = FollowCheck;
                break;
            case "wait":
                LeanTween.color(_renderer.gameObject, defaultColor, 1f);
                stateCheck = WaitCheck;
                break;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
        
    }

    public event Action reached;
}
