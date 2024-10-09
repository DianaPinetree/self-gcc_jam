using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats blueprintStats;
    public PlayerStats stats;
    public float invincibilityTime = 0.1f;
    private float invincibilityElapsed = 0f;
    public float hitboxRadius = 0.4f;
    private Rigidbody2D rb;
    private Vector3 movement;
    private Health playerHealth;

    // Experience & level
    long xp = 0;
    long xpFromLastLevel = 0;
    long xpToNextLevel = 0;
    int level = 0;

    // Spatial groups
    int spatialGroup = -1;

    public int SpatialGroup
    {
        get { return spatialGroup; }
    }

    // Start is called before the first frame update
// Nearest enemy position (for weapons)
    Vector2 nearestEnemyPosition = Vector2.zero;

    public Vector2 NearestEnemyPosition
    {
        get { return nearestEnemyPosition; }
        set { nearestEnemyPosition = value; }
    }

    bool noNearbyEnemies = false; // shoot randomly

    public bool NoNearbyEnemies
    {
        get { return noNearbyEnemies; }
        set { noNearbyEnemies = value; }
    }

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        movement = new Vector3();
        stats = Instantiate(blueprintStats); // get base stats
    }

    void Start()
    {
        spatialGroup =
            GameController.instance.GetSpatialGroup(transform.position.x, transform.position.y); // GET spatial group

        rb = GetComponent<Rigidbody2D>();

        LevelUp();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement * stats.playerSpeed, ForceMode2D.Force);

        // Calculate nearest enemy direction
        spatialGroup =
            GameController.instance.GetSpatialGroup(transform.position.x, transform.position.y); // GET spatial group
        // CalculateNearestEnemyDirection();

        // Colliding with any enemy? Lose health?
        // only check if invincibility is off
        invincibilityElapsed += Time.deltaTime;
        if (invincibilityElapsed > invincibilityTime)
        {
            CheckCollisionWithEnemy();
            invincibilityElapsed = 0;
        }
    }

    void CheckCollisionWithEnemy()
    {
        List<int> surroundingSpatialGroups = Utils.GetExpandedSpatialGroups(spatialGroup);
        List<Enemy> surroundingEnemies = Utils.GetAllEnemiesInSpatialGroups(surroundingSpatialGroups);

        foreach (Enemy enemy in surroundingEnemies)
            // foreach (Enemy enemy in GameController.instance.enemySpatialGroups[spatialGroup])
        {
            if (enemy == null) continue;

            // float distance = Mathf.Abs(transform.position.x - enemy.transform.position.x) + Mathf.Abs(transform.position.y - enemy.transform.position.y);
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < hitboxRadius)
            {
                // Take damage
                playerHealth.TakeDamage(3f);
                break;
            }
        }
    }

    void CalculateNearestEnemyDirection()
    {
        // Just checks enemies in the same spatial group
        float minDistance = 100f;
        Vector2 closestPosition = Vector2.zero;
        bool foundATarget = false;

        List<int> spatialGroupsToSearch = new List<int>() { spatialGroup };

        // No enemies in your spatial group, expand search to surrounding spatial groups
        // if (GameController.instance.enemySpatialGroups[spatialGroup].Count == 0)
        //     spatialGroupsToSearch = Utils.GetExpandedSpatialGroupsV2(spatialGroup, 6);
        spatialGroupsToSearch = Utils.GetExpandedSpatialGroupsV2(spatialGroup, 6);

        // Get all enemies
        List<Enemy> nearbyEnemies = Utils.GetAllEnemiesInSpatialGroups(spatialGroupsToSearch);

        // No nearby enemies?
        if (nearbyEnemies.Count == 0)
        {
            noNearbyEnemies = true;
        }
        else
        {
            noNearbyEnemies = false;

            // Filter thru enemies
            foreach (Enemy enemy in nearbyEnemies)
            {
                if (enemy == null) continue;

                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPosition = enemy.transform.position;
                    foundATarget = true;
                }
            }

            if (!foundATarget) // Somehow no targets found? randomize
                noNearbyEnemies = true;
            else
                nearestEnemyPosition = closestPosition;
        }
    }

    public void AddXP(int amount)
    {
        xp += amount;
        UIExperienceBar.instance.UpdateExperienceBar(xp - xpFromLastLevel, xpToNextLevel - xpFromLastLevel);
        if (xp >= xpToNextLevel) LevelUp();
    }

    public void LevelUp()
    {
        level++;
        xpFromLastLevel = xpToNextLevel;
        xpToNextLevel = Utils.GetExperienceRequired(level) - xpToNextLevel;
        Debug.Log("Level up: " + level + " XP to next level: " + xpToNextLevel);
        UIExperienceBar.instance.SetLevelText(level);

        if (level > 1)
        {
            UIUpgradesView.instance.GetUpgrade(gameObject);
        }
    }

    public void OnKill()
    {
        EndScreen.instance.Open();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, hitboxRadius);
    }
}