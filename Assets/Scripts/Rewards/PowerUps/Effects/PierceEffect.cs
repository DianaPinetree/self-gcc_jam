using UnityEngine;

[CreateAssetMenu(menuName = "Progression/Effects/Pierce")]
public class PierceEffect : Effect
{
    public float Amount;
    [SerializeField] private AddMode addMode;
    public override void Apply(GameObject target)
    {
        switch (addMode)
        {
            case AddMode.mul:
            case AddMode.add:
                target.GetComponent<Player>().stats.pierce += Amount;
                break;    
            case AddMode.set:
                target.GetComponent<Player>().stats.pierce = Amount;
                break;    
        }
    }
}