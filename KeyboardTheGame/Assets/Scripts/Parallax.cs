using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	private float X;
	private float Y;
	public float offset;
	public bool FollowCamera;
	// Use this for initialization
	void Start () {
		X = Camera.main.transform.position.x;
		//Y = Camera.main.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = transform.position;
		if (FollowCamera)
		{
			newPos.x = (Camera.main.transform.position.x - X)/offset;
			newPos.y = transform.position.y;
			//newPos.y = (Camera.main.transform.position.y - Y)/offset;
			transform.position = newPos;
		}
		else
		{
			newPos.x = (X - Camera.main.transform.position.x )/offset;
			newPos.y = transform.position.y;
			//newPos.y = (Y - Camera.main.transform.position.y)/offset;
			transform.position = newPos;
		}
	}
}
