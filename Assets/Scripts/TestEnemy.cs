using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int spawnCount;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;
        var controller = GetComponent<GameController>();
        for (int i = 0; i <spawnCount; i++)
        {
            controller.SpawnEnemy(enemyPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
