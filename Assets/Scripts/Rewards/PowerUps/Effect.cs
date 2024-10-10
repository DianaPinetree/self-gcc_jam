using UnityEngine;
public abstract class Effect : ScriptableObject
{
    protected enum AddMode
    {
        mul,
        add,
        set
    }
    public abstract void Apply(GameObject target);
}