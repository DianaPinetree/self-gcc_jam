using UnityEngine;

[CreateAssetMenu(menuName = "Progression/Power Up")]
public class PowerUp : ScriptableObject
{
    [SerializeField] private Effect[] _effects;
    [TextArea] public string description;
    public bool unique; // show only once
    public void SetPowerUp(GameObject target)
    {
        for (int i = 0; i < _effects.Length; i++)
        {
            Debug.Log("Adding " + _effects[i] + " to " + target.name);
            _effects[i].Apply(target);
        }
    }
}