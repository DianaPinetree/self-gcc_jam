using UnityEngine;

[CreateAssetMenu(menuName = "Progression/Effects/Accuracy")]
public class AccuracyEffect : Effect
{
    public float Amount;
    public override void Apply(GameObject target)
    {
        PlayerStats stats = target.GetComponent<Player>().stats;
        stats.attackAccuracy *= Amount;
        stats.attackAccuracy = Mathf.Clamp(stats.attackAccuracy, -180, 180);
    }
}