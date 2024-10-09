using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public static Vector3 direction;
    [SerializeField] private Transform aimSprite;
    [SerializeField] private LayerMask hitMask = ~0; // everything
    private Camera mainCam;
    // Start is called before the first frame update
    private RaycastHit2D[] _hit2D;
    void Start()
    {
        Cursor.visible = false;
        _hit2D = new RaycastHit2D[4];
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 coords = Input.mousePosition;
        Vector3 worldPos = mainCam.ScreenToWorldPoint(coords);
        worldPos.z = 0;
        direction = worldPos - transform.position;
        direction.Normalize();
        if (aimSprite != null)
        {
            aimSprite.position = Vector3.Lerp(aimSprite.position, worldPos, Time.deltaTime * 10f);
            int hits = Physics2D.RaycastNonAlloc(transform.position, direction, _hit2D, 500f, hitMask);
            if (hits > 0)
            {
                aimSprite.Rotate(new Vector3(0, 0, -10f) * (Time.deltaTime * 30f));
            }
        }
        
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        Cursor.visible = hasFocus;
    }
}
