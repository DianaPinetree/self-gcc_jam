    using UnityEngine;

    [CreateAssetMenu(menuName = "Progression/Effects/Projectile Increase")]
    public class ProjectileIncrease : Effect
    {
        [SerializeField] private float Amount;
        public override void Apply(GameObject target)
        {
            target.GetComponent<Player>().stats.projectiles += Amount;
        }
    }
