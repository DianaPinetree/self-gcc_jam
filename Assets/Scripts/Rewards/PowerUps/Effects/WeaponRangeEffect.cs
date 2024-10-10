using UnityEngine;
[CreateAssetMenu(menuName = "Progression/Effects/Weapon Range")]
public class WeaponRangeEffect : Effect
{
    public float Amount;
    [SerializeField] private AddMode addMode = AddMode.add;
    public override void Apply(GameObject target)
    {
        if (addMode == AddMode.add)
        {
            
            target.GetComponent<Player>().stats.weaponRange += Amount;
        }
        else if (addMode == AddMode.set)
        {
            target.GetComponent<Player>().stats.weaponRange = Amount;
        }
    }
}