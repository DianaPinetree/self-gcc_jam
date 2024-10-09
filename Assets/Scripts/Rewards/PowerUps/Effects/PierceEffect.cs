using UnityEngine;

[CreateAssetMenu(menuName = "Progression/Effects/Pierce")]
public class PierceEffect : Effect
{
    public float Amount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<Player>().stats.pierce += Amount;
    }
}