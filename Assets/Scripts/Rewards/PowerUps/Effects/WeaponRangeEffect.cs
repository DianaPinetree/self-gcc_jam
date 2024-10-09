using UnityEngine;
[CreateAssetMenu(menuName = "Progression/Effects/Weapon Range")]
public class WeaponRangeEffect : Effect
{
    public float Amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<Player>().stats.weaponRange += Amount;
    }
}