using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Upgrades Collection")]
public class GameUpgrades : ScriptableObject
{
    public List<PowerUp> gameUpgrades;

    public List<PowerUp> GetRandom(HashSet<PowerUp> collected, int count)
    {
        var available = gameUpgrades.Where(x => !collected.Contains(x) && x.unique); // filter collected uniques
        var indexes = new HashSet<PowerUp>();
        if (count > gameUpgrades.Count)
        {
            count = gameUpgrades.Count;
        }
        
        while (indexes.Count < count)
        {
            int r = Random.Range(0, gameUpgrades.Count);
            indexes.Add(gameUpgrades[r]);
        }

        return indexes.ToList();
    }

}