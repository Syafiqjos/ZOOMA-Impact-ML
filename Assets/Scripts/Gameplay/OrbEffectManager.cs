using UnityEngine;
using System.Collections;

public class OrbEffectManager : MonoBehaviour
{
    [SerializeField] private OrbGenerator orbGenerator;
    [SerializeField] private GameObject poppedOrbEffect;

    private void Start()
    {
        orbGenerator.OnOrbRemoved += SpawnPoppedOrbEffect;
    }

    private void SpawnPoppedOrbEffect(Orb orb) {
        if (orb) {
            GameObject ne = Instantiate(poppedOrbEffect, orb.transform.position, Quaternion.identity);
            ParticleSystem p = ne.GetComponent<ParticleSystem>();
            if (p) {
                p.startColor = orb.GetOrbData().GetMaterial().color;
            }
        }
    }
}
