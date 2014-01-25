using UnityEngine;
using System.Collections;

public class RecordSound : MonoBehaviour
{
    public PlayerControl.RecordableSounds soundToRecord;
    public AudioClip recordedClip;
    public int length = 1;

    QuickTimer timer;

    bool hasAppliedTriggerAction = false;

    void Start()
    {
        timer = new QuickTimer();

    }

    public void OnApplyTriggerAction()
    {
        hasAppliedTriggerAction = true;
    }
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            recordedClip = Microphone.Start(null, false, length, 44100);
            timer.Start(length);
        }

        if (timer.IsStarted() && timer.IsElapsed())
        {
            hasAppliedTriggerAction = false;
            timer.Stop();
            Microphone.End(null);
            GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
            PlayerControl playerControlScript = playerGameObject.GetComponent<PlayerControl>();
            playerControlScript.AddSound(soundToRecord, recordedClip);
        }
    }

    void OnGUI()
    {
        if (hasAppliedTriggerAction)
        {
            if (Microphone.IsRecording(null))
            {
                GUILayout.Label("Recording for " + timer.GetElapsedTime().ToString() + " seconds.");
            }
            else
            {
                GUILayout.Label("Press Space to begin recording the new walking sound");
            }
        }
    }
}
