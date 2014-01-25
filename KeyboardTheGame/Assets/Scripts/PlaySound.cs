using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour
{
    public PlayerControl.RecordableSounds soundToPlay;

    public void OnApplyTriggerAction()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        PlayerControl playerControlScript = playerGameObject.GetComponent<PlayerControl>();

        playerControlScript.PlaySound(soundToPlay);
    }
}
