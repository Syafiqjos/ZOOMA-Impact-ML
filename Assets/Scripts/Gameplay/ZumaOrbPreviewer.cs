using UnityEngine;
using System.Collections;

public class ZumaOrbPreviewer : MonoBehaviour
{
    [SerializeField] ZumaShooter shooter;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] MeshFilter meshFilter;

    private void Start()
    {
        shooter.OnShoot += RefreshPreview;

        shooter.RefreshToBeShootedOrbData();
        RefreshPreview();
    }

    public void RefreshPreview()
    {
        OrbData preservedOrbData = shooter.GetToBeShootedOrbData();

        Material material = preservedOrbData.GetMaterial();
        Mesh mesh = preservedOrbData.GetMesh();

        if (material) meshRenderer.material = material;
        if (mesh) meshFilter.mesh = mesh;
    }
}
