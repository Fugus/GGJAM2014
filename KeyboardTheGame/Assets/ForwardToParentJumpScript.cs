using UnityEngine;
using System.Collections;

public class ForwardToParentJumpScript : MonoBehaviour {

    public void JumpDone()
    {
        PlayerControl playrControlScript = transform.parent.GetComponent<PlayerControl>();
        if (playrControlScript)
        {
            playrControlScript.JumpDone();
        }
    }
}
