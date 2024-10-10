using UnityEngine;
[CreateAssetMenu(menuName = "Progression/Effects/Attack speed Increase")]
public class AttackSpeed : Effect
{
    [Min(0f)]
    public float Amount;

    [SerializeField] private AddMode addMode = AddMode.mul;
    public override void Apply(GameObject target)
    {
        if (addMode == AddMode.mul)
        {
            target.GetComponent<Player>().stats.attackSpeedMod *= Amount;
        }
        else if (addMode == AddMode.add)
        {
            target.GetComponent<Player>().stats.attackSpeedMod += Amount;
        }
        else
        {
            target.GetComponent<Player>().stats.attackSpeedMod = Amount;
        }
    }
}