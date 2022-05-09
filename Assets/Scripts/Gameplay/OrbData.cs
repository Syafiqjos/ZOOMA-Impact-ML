using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrbData", menuName = "ScriptableObjects/OrbDataScriptableObject", order = 1)]
public class OrbData : ScriptableObject
{
    [SerializeField] private int orbType = 0; // 0 will pop with 0, 1 will pop with 1, etc
    [SerializeField] private Material material;
    [SerializeField] private Mesh mesh;

    public int GetOrbType()
    {
        return orbType;
    }

    public Material GetMaterial()
    {
        return material;
    }

    public Mesh GetMesh()
    {
        return mesh;
    }
}
