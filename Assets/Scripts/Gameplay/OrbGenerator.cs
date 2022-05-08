using UnityEngine;
using System.Collections;

public class OrbGenerator : MonoBehaviour
{
    [SerializeField] private Orb orbPrefab;
    [SerializeField] private SplineFollowerManager followerManager;
    [SerializeField] private Transform orbsContainer;

    private Orb foremostOrb = null;
    private Orb latestOrb = null;

    private float latestMoveAmount = 0;

    private void Update()
    {
        CheckShouldAddOrb();
    }

    private void CheckShouldAddOrb()
    {
        // if pertama kali ngespawn
        if (foremostOrb == null && latestOrb == null)
        {
            AddOrb();
        } else
        {
            if (followerManager.GetMoveAmount() > latestMoveAmount + followerManager.GetOffsetInterval())
            {
                AddOrb();
                latestMoveAmount = followerManager.GetMoveAmount();
            }
        }
    }

    public void AddOrb()
    {
        // if pertama kali ngespawn
        if (foremostOrb == null && latestOrb == null)
        {
            Orb newOrb = SpawnOrb(0);
            foremostOrb = newOrb;
            latestOrb = newOrb;
        } else
        {
            int index = latestOrb.GetFollower().GetOffsetIndex();
            int newIndex = index + 1;

            Orb newOrb = SpawnOrb(newIndex);
            latestOrb = newOrb;
        }
    }

    public Orb SpawnOrb(int index)
    {
        GameObject ne = Instantiate(orbPrefab.gameObject, orbsContainer);
        Orb orb = ne.GetComponent<Orb>();

        orb.SetManager(followerManager);
        orb.SetIndex(index);

        return orb;
    }
}
