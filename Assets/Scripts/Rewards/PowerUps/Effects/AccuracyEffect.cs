using UnityEngine;

[CreateAssetMenu(menuName = "Progression/Effects/Accuracy")]
public class AccuracyEffect : Effect
{
    [SerializeField] private AddMode addMode = AddMode.mul;
    public float Amount;

    public override void Apply(GameObject target)
    {
        PlayerStats stats = target.GetComponent<Player>().stats;

        if (addMode == AddMode.mul)
        {
            stats.attackAccuracy *= Amount;
            stats.attackAccuracy = Mathf.Clamp(stats.attackAccuracy, -180, 180);
        }
        else if (addMode == AddMode.add)
        {
            stats.attackAccuracy += Amount;
            stats.attackAccuracy = Mathf.Max(stats.attackAccuracy, 0f);
        }
        else if (addMode == AddMode.set)
        {
            stats.attackAccuracy = Amount;
        }
    }
}