using UnityEngine;
using System.Collections;

public class ObjectScrolling : MonoBehaviour {

	public float speed;
	float resetDistance;
	float initialDistance;
	public bool isVertical = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float move = speed * Time.deltaTime;  
		Vector3 newPos = transform.position;
		if (isVertical) {  
			transform.Translate( Vector3.down * move, Space.World);  

			if (transform.position.y < resetDistance)  
			{  
				 transform.position = new Vector3(transform.position.x, initialDistance, transform.position.z);  
			}  
		}else{  
			transform.Translate(Vector3.left * move, Space.World);  
			if (transform.position.x < resetDistance)  
			{  
				transform.position = new Vector3(initialDistance, transform.position.y, transform.position.z);  
			}  
		}  
	}
}
