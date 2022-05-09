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
            newOrb.SetIndexForce(newOrb.GetIndex());
            foremostOrb = newOrb;
            latestOrb = newOrb;
        } else
        {
            int index = latestOrb.GetFollower().GetOffsetIndex();
            int newIndex = index - 1;

            Orb newOrb = SpawnOrb(newIndex);
            newOrb.SetIndexForce(newOrb.GetIndex());

            latestOrb.SetPrevOrb(newOrb);
            newOrb.SetNextOrb(latestOrb);

            latestOrb = newOrb;
        }
    }

    public Orb SpawnOrb(int index)
    {
        GameObject ne = Instantiate(orbPrefab.gameObject, orbsContainer);
        Orb orb = ne.GetComponent<Orb>();

        orb.SetManager(followerManager);
        orb.SetGenerator(this);
        orb.SetIndex(index);

        orb.transform.position = orbsContainer.transform.position;

        return orb;
    }

    public void PlaceNextOrb(Orb orb)
    {
        int currentIndex = orb.GetIndex();
        int nextIndex = currentIndex + 1;
        Orb newOrb = SpawnOrb(nextIndex);

        ReassignOrbLink(newOrb, orb, orb.GetNextOrb());
        ReindexNextOrbLink(newOrb);

        newOrb.SetIndexForce(currentIndex);
        newOrb.SetIndex(nextIndex);
    }

    private void ReassignOrbLink(Orb newOrb, Orb prevOrb, Orb nextOrb)
    {
        newOrb.SetPrevOrb(prevOrb);
        newOrb.SetNextOrb(nextOrb);

        prevOrb.SetNextOrb(newOrb);

        if (nextOrb)
        {
            // Placing orb in the center
            nextOrb.SetPrevOrb(newOrb);
        } else
        {
            // Placing orb in the foremost
            foremostOrb = newOrb;
        }
    }

    private void ReindexNextOrbLink(Orb orb)
    {
        Orb currentOrb = orb;

        Orb nextOrb;
        while (nextOrb = currentOrb.GetNextOrb())
        {
            int currentIndex = currentOrb.GetIndex();
            int nextIndex = currentIndex + 1;

            nextOrb.SetIndex(nextIndex);

            currentOrb = nextOrb;
        }
    }

    public Orb GetForemostOrb()
    {
        return foremostOrb;
    }

    public Orb GetLatestOrb()
    {
        return latestOrb;
    }
}
