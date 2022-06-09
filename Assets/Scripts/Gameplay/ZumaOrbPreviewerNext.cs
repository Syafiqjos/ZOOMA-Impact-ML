using UnityEngine;
using System.Collections;

public class ZumaOrbPreviewerNext : MonoBehaviour
{
    [SerializeField] ZumaShooter shooter;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] MeshFilter meshFilter;

    private void Start()
    {
        shooter.OnShoot += RefreshPreview;

        // shooter.RefreshToBeShootedOrbData();
        RefreshPreview();
    }

    public void RefreshPreview()
    {
        OrbData preservedNextOrbData = shooter.GetToBeShootedNextOrbData();

        Material material = preservedNextOrbData.GetMaterial();
        Mesh mesh = preservedNextOrbData.GetMesh();

        if (material) meshRenderer.material = material;
        if (mesh) meshFilter.mesh = mesh;
    }
}
