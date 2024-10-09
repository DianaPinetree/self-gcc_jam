using UnityEngine;
[CreateAssetMenu(menuName = "Progression/Effects/Attack speed Increase")]
public class AttackSpeed : Effect
{
    [Min(0f)]
    public float Amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<Player>().stats.attackSpeedMod *= Amount;
    }
}