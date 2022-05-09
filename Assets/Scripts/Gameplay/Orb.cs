using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour
{
    [SerializeField] private SplineFollowerCentered splineFollower;
    [SerializeField] private OrbGenerator generator;

    private Orb nextOrb; // foremost
    private Orb prevOrb; // latest

    public int GetIndex()
    {
        return splineFollower.GetOffsetIndex();
    }

    public void SetIndexForce(int index)
    {
        splineFollower.SetOffsetIndexForce(index);
    }

    public void SetIndex(int index)
    {
        splineFollower.SetOffsetIndex(index);
    }

    public void SetManager(SplineFollowerManager manager)
    {
        splineFollower.SetManager(manager);
    }

    public void SetGenerator(OrbGenerator generator)
    {
        this.generator = generator;
    }

    public SplineFollowerCentered GetFollower()
    {
        return splineFollower;
    }

    public Orb GetNextOrb()
    {
        return nextOrb;
    }

    public void SetNextOrb(Orb orb)
    {
        nextOrb = orb;
    }

    public void SetPrevOrb(Orb orb)
    {
        prevOrb = orb;
    }

    public Orb GetPrevOrb()
    {
        return prevOrb;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            ZumaBullet bullet = other.GetComponent<ZumaBullet>();
            if (bullet && !bullet.GetPlacedFlag()) {
                generator.PlaceNextOrb(this);
                if (bullet)
                {
                    bullet.SetPlacedFlag(true);
                }
            }
            Destroy(other.gameObject);
        }
    }
}
