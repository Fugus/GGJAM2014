using UnityEngine;
using System.Collections;

public class CameraDisablingScript : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
        UIManager UIManager_ = GameObject.Find("/Managers").GetComponent<UIManager>();

        Transform regularCamera = transform.Find("Main Camera");
        Transform occulusCamera = transform.Find("OVRCameraController");

        if (UIManager_.isOculus && !occulusCamera.gameObject.activeInHierarchy)
        {
            occulusCamera.gameObject.SetActive(true);
            regularCamera.gameObject.SetActive(false);
        }
        if (!UIManager_.isOculus && !regularCamera.gameObject.activeInHierarchy)
        {
            occulusCamera.gameObject.SetActive(false);
            regularCamera.gameObject.SetActive(true);
        }
    }
}
