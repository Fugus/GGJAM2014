using UnityEngine;
using System.Collections;

public class ForwardToParentJumpScript : MonoBehaviour {

    public void InitialJumpDone()
    {
        gameObject.SendMessageUpwards("JumpDone");
    }
}
