    using UnityEngine;

    [CreateAssetMenu(menuName = "Progression/Effects/Movement Speed")]
    public class MoveSpeedEffect : Effect
    {
        public float Amount;

        public override void Apply(GameObject target)
        {
            target.GetComponent<Player>().stats.playerSpeed += Amount;
        }
    }
