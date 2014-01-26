using UnityEngine;
using System.Collections;

public class QuickTimer
{
	private float endTime = 0.0f;
	private bool isStarted = false;

	public QuickTimer ()
	{
	}

	public void Start(float duration)
	{
		endTime = Time.time + duration;
		isStarted = true;
	}
	
	public void Stop()
	{
		isStarted = false;
	}
	
	public bool IsStarted()
	{
		return isStarted;
	}

    public float GetElapsedTime()
    {
        if (!isStarted)
        {
            Debug.LogError("Calling GetElapsedTime() on a stopped QuickTimer");
            return 0.0f;
        }
        
        return endTime - Time.time;
    }

	public bool IsElapsed()
	{
		if (!isStarted)
		{
			Debug.LogError("Calling IsElapsed() on a stopped QuickTimer");
			return true;
		}
		return Time.time > endTime;
	}

}

