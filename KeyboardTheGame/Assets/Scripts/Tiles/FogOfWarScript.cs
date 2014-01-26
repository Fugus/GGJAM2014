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
			EnableFOW();
        }
    }

    public void OnDisable()
    {
		DisableFOW();
    }

	public void OnEnable()
	{
		EnableFOW();
	}

	public void EnableFOW()
	{
		if (transform.parent != null)
		{
			particleSystem.Play();
			oldMaterial = transform.parent.renderer.material;
			transform.parent.renderer.material = materialToSubstitute;
		}
	}

	public void DisableFOW()
	{
		if (transform.parent != null)
		{
			particleSystem.Stop();
			if(oldMaterial != null)
				transform.parent.renderer.material = oldMaterial;
		}
	}

}
