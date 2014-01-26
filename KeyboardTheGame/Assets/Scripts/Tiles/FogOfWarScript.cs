using UnityEngine;
using System.Collections;

public class FogOfWarScript : MonoBehaviour
{
    public Material materialToSubstitute;
    Material oldMaterial;

    public void Start()
    {
        oldMaterial = transform.parent.renderer.material;
        transform.parent.renderer.material = materialToSubstitute;
        particleSystem.Play();
    }

    public void OnDisable()
    {
        particleSystem.Stop();
        transform.parent.renderer.material = oldMaterial;
    }
}
