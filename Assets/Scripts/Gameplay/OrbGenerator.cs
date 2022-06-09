using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrbGenerator : MonoBehaviour
{
    [SerializeField] private Orb orbPrefab;
    [SerializeField] private SplineFollowerManager followerManager;
    [SerializeField] private Transform orbsContainer;

    [SerializeField] private List<OrbData> orbsData;

    private Orb foremostOrb = null;
    private Orb latestOrb = null;

    private float latestMoveAmount = 0;

    [SerializeField] private ScoreManager scoreManager;

    public delegate void OnOrbRemovedEventHandler(Orb orb);
    public event OnOrbRemovedEventHandler OnOrbRemoved;

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
        orb.SetOrbData(
            GetRandomOrbDataDifferently(
                new List<OrbData> {
                    latestOrb?.GetOrbData(),
                    latestOrb?.GetNextOrb()?.GetOrbData()
                })
        );

        orb.transform.position = orbsContainer.transform.position;

        return orb;
    }

    public void PlaceNextOrb(Orb orb, OrbData newOrbData)
    {
        int currentIndex = orb.GetIndex();
        int nextIndex = currentIndex + 1;
        Orb newOrb = SpawnOrb(nextIndex);
        newOrb.SetOrbData(newOrbData);

        ReassignOrbLink(newOrb, orb, orb.GetNextOrb());
        ReindexNextOrbLink(newOrb);

        newOrb.SetIndexForce(currentIndex);
        newOrb.SetIndex(nextIndex);

        StartCoroutine(CheckPopSequentialOrbs(newOrb));
    }

    public void RemoveOrb(Orb orb)
    {
        ReassignOrbLink(orb);
        ReindexNextOrbLink(orb.GetPrevOrb());

        OnOrbRemoved?.Invoke(orb);
        Destroy(orb.gameObject);
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

    private void ReassignOrbLink(Orb orb)
    {
        Orb nextOrb = orb.GetNextOrb();
        Orb prevOrb = orb.GetPrevOrb();

        prevOrb?.SetNextOrb(nextOrb);
        nextOrb?.SetPrevOrb(prevOrb);

        // Orb is a foremost
        if (orb.GetNextOrb() == null)
        {
            foremostOrb = foremostOrb.GetPrevOrb();
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

    private IEnumerator CheckPopSequentialOrbs(Orb orb, int combo = 1)
    {
        Orb foremostSequentialOrb = orb;

        // Travel to foremost that have the same color
        while (foremostSequentialOrb.GetNextOrb() 
            && foremostSequentialOrb.GetNextOrb().GetOrbData().GetOrbType() == foremostSequentialOrb.GetOrbData().GetOrbType())
        {
            foremostSequentialOrb = foremostSequentialOrb.GetNextOrb();
        }

        List<Orb> sequentialOrbs = new List<Orb>();
        Orb currentOrb = foremostSequentialOrb;
        Orb nextForemostOrb = currentOrb.GetNextOrb();

        sequentialOrbs.Add(currentOrb);

        // Travel to latest that have the same color
        while (currentOrb.GetPrevOrb()
            && currentOrb.GetPrevOrb().GetOrbData().GetOrbType() == currentOrb.GetOrbData().GetOrbType())
        {
            currentOrb = currentOrb.GetPrevOrb();
            sequentialOrbs.Add(currentOrb);
        }

        // Check if sequential has more than 2 orbs then remove all of them
        if (sequentialOrbs.Count > 2)
        {
            foreach (Orb checkedOrb in sequentialOrbs)
            {
                if (checkedOrb)
                {
                    RemoveOrb(checkedOrb);
                }
            }
            // Add Score
            scoreManager.ExecuteScore(combo);
            if (nextForemostOrb)
            {
                yield return new WaitForSeconds(1.0f);
                yield return CheckPopSequentialOrbs(nextForemostOrb, combo + 1);
            }
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

    public OrbData GetRandomOrbData()
    {
        return orbsData[Random.Range(0, orbsData.Count)];
    }

    public OrbData GetRandomOrbDataDifferently(List<OrbData> usedOrbsData)
    {
        List<OrbData> randomOrbDataListTemp = new List<OrbData>(orbsData);
        if (usedOrbsData[0] == usedOrbsData[1])
        {
            randomOrbDataListTemp.Remove(usedOrbsData[0]);
        }

        return randomOrbDataListTemp[Random.Range(0, randomOrbDataListTemp.Count)];
    }

    public Orb GetExistForemostSequentialOrb(OrbData orbDataMatch = null, int depthLimit = 1000)
    {
        Orb foremostSequentialOrb = GetForemostOrb();

        // Travel to foremost that have the same color
        while (foremostSequentialOrb && depthLimit > 0)
        {
            Orb prevOrb = foremostSequentialOrb.GetPrevOrb();

            if (prevOrb)
            {
                int foremostOrbType = foremostSequentialOrb.GetOrbData().GetOrbType();
                int prevOrbType = prevOrb.GetOrbData().GetOrbType();
                if (foremostOrbType == prevOrbType
                    && (orbDataMatch == null || orbDataMatch.GetOrbType() == foremostOrbType))
                {
                    return foremostSequentialOrb;
                }
            }

            foremostSequentialOrb = prevOrb;
            depthLimit -= 1;
        }

        return null;
    }

    public Orb GetExistForemostOrb(OrbData orbDataMatch = null, int depthLimit = 1000)
    {
        Orb foremostSequentialOrb = GetForemostOrb();

        // Travel to foremost that have the same color
        while (foremostSequentialOrb && depthLimit > 0)
        {
            Orb prevOrb = foremostSequentialOrb.GetPrevOrb();

            if (prevOrb)
            {
                int foremostOrbType = foremostSequentialOrb.GetOrbData().GetOrbType();
                if ((orbDataMatch == null || orbDataMatch.GetOrbType() == foremostOrbType))
                {
                    return foremostSequentialOrb;
                }
            }

            foremostSequentialOrb = prevOrb;
            depthLimit -= 1;
        }

        return null;
    }
}
