using UnityEngine;

[CreateAssetMenu(menuName = "Progression/Effects/Range Pickup")]
public class RangePickupEffect : Effect
{
    public float Amount;
    
    public override void Apply(GameObject target)
    {
        target.GetComponent<Player>().stats.xpRange *= Amount;
    }
}