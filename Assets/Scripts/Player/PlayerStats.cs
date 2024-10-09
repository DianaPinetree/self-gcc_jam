using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public float playerSpeed = 7f;
    public float attackSpeedMod = 1f;
    public float xpRange = 4f;
    public float projectiles = 1;
    public float attackAccuracy = 8f;
    public float pierce = 0f;
    public float weaponRange = 50f;
}
