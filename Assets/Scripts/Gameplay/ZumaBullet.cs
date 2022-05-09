using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumaBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshFilter meshFilter;

    private OrbData orbData;

    private bool placedFlag = false;

    public void LaunchBullet(Vector3 direction, float speed)
    {
        rb.AddForce(direction * speed, ForceMode.Impulse);
    }

    public bool GetPlacedFlag()
    {
        return placedFlag;
    }

    public void SetPlacedFlag(bool flag)
    {
        placedFlag = flag;
    }

    public void SetOrbData(OrbData data)
    {
        orbData = data;

        if (orbData.GetMaterial()) meshRenderer.material = orbData.GetMaterial();
        if (orbData.GetMesh()) meshFilter.mesh = orbData.GetMesh();
    }

    public OrbData GetOrbData()
    {
        return orbData;
    }
}
