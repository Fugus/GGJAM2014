using UnityEngine;
using System.Collections;

public class FogOfWarScript : MonoBehaviour
{
    public Material materialToSubstitute;
    Material oldMaterial;

    bool firstUpdate = true;

    public void Update()
    {
        if (firstUpdate)
        {
            firstUpdate = false;
            if (transform.parent != null)
            {
                oldMaterial = transform.parent.renderer.material;
                transform.parent.renderer.material = materialToSubstitute;
                particleSystem.Play();
            }
        }
    }

    public void OnDisable()
    {
        if (transform.parent != null)
        {
            particleSystem.Stop();
            transform.parent.renderer.material = oldMaterial;
        }
    }

	public void OnEnable()
	{
		if (transform.parent != null)
		{
			particleSystem.Play();
			oldMaterial = transform.parent.renderer.material;
			transform.parent.renderer.material = materialToSubstitute;
		}
	}
}
