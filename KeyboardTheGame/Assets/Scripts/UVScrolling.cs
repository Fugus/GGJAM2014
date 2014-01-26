using UnityEngine;
using System.Collections;

public class UVScrolling : MonoBehaviour {

	public bool isVertical = false;
	public float scrollSpeed = 0.01f;
	 float startTime = 0.0f;
	 float endTime = 0.5f;
	public bool isReverse = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float offset = Time.time * -scrollSpeed;
		if (isVertical)
		{
			float timeTaken = startTime + Time.timeSinceLevelLoad;
			renderer.material.mainTextureOffset = new Vector2 (0, offset);
			if (timeTaken >= endTime)
			{
				renderer.material.mainTextureOffset = new Vector2(0, -offset);
				timeTaken = 0f;
			}
		}else
		{
			if( isReverse == false)
			renderer.material.mainTextureOffset = new Vector2(offset, 0);
			else
				renderer.material.mainTextureOffset = new Vector2(-offset, 0);
		
		}
	}
}
