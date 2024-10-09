using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RewardMagnet : MonoBehaviour
{
    [SerializeField] private Player player;
    private List<PickUp> suckList = new List<PickUp>();
    [SerializeField] private AudioClip pickupFX;
    private float pitchTimer = 0f;
    private float resetOn = 1.7f;
    private float currentPitch = 0.6f;
    private float maxPitch = 1.3f;
    private float maxPickupsToPitch = 50;
    private void FixedUpdate()
    {
        if (pitchTimer > 0)
        {
            pitchTimer -= Time.fixedDeltaTime;
            if (pitchTimer <= 0)
            {
                pickups = 0;
                currentPitch = 0.6f;
            }
        }
        GetNearXP();
    }

    private int pickups;
    void GetNearXP()
    {
        pickups = 0;
        int spatialGroup = player.SpatialGroup;
        List<int> spatialGroupsToSearch = new List<int>() { spatialGroup };
        spatialGroupsToSearch = Utils.GetExpandedSpatialGroupsV2(spatialGroup, (int)player.stats.xpRange);
        List<PickUp> nearbyPickups = Utils.GetXPInSpatialGroups(spatialGroupsToSearch);
        
        if (nearbyPickups.Count == 0) return;
        pickups = nearbyPickups.Count;
        foreach (var pickup in nearbyPickups)
        {
            float distance = Vector2.Distance(transform.position, pickup.transform.position);
            if (distance < player.stats.xpRange)
            {
                pitchTimer = resetOn;
                currentPitch = Mathf.Lerp(currentPitch, maxPitch, (float)pickups / maxPickupsToPitch);
                AudioPlayer.PlayAudio(pickupFX, currentPitch, UnityEngine.Random.Range(0.7f, .9f));
                pickup.Collect(player);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (player.stats != null)
            Gizmos.DrawWireSphere(transform.position, player.stats.xpRange);
    }
}