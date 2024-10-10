    using UnityEngine;

    [CreateAssetMenu(menuName = "Progression/Effects/Projectile Increase")]
    public class ProjectileIncrease : Effect
    {
        [SerializeField] private float Amount;
        [SerializeField] private AddMode addMode;
        public override void Apply(GameObject target)
        {
            PlayerStats playerStats = target.GetComponent<PlayerStats>();
            switch (addMode)
            {
                case AddMode.mul:
                case AddMode.add:
                    playerStats.projectiles += Amount;
                    
                    break;    
                case AddMode.set:
                    playerStats.projectiles = Amount;
                    break;    
            }

            playerStats.projectiles = Mathf.Max(playerStats.projectiles, 1);
        }
    }
