using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
    public float timeToNextTile = 0.2f;
    public float tileRadius = 20.0f;

    public enum RecordableSounds
    {
        Walking,
        GuitarSolo
    }

    public Dictionary<RecordableSounds, AudioClip> sounds;

    enum MovementDirection
    {
        TopLeft,
        TopRight,
        Left,
        Right,
        BottomLeft,
        BottomRight
    }

	private Animator anim;					// Reference to the player's animator component.

    bool canReadInput = true;
    bool buttonPressed = false;
    MovementDirection direction;
    Vector3 targetPosition;

    GameObject currentTile;

	void Awake()
	{
		// Setting up references.
		anim = GetComponent<Animator>();
        sounds = new Dictionary<RecordableSounds, AudioClip>();
	}

    void Start()
    {
        targetPosition = transform.position;
    }

    Vector2 GetDirection(MovementDirection mvtDirection)
    {
        float angle = 0.0f;

        switch (mvtDirection)
        {
            case MovementDirection.TopLeft:
                angle = 120 * Mathf.Deg2Rad;
                break;
            case MovementDirection.TopRight:
                angle = 60 * Mathf.Deg2Rad;
                break;
            case MovementDirection.Left:
                angle = 180 * Mathf.Deg2Rad;
                break;
            case MovementDirection.Right:
                angle = 0 * Mathf.Deg2Rad;
                break;
            case MovementDirection.BottomLeft:
                angle = 240 * Mathf.Deg2Rad;
                break;
            case MovementDirection.BottomRight:
                angle = 300 * Mathf.Deg2Rad;
                break;
        }

        Vector2 result = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        Debug.Log(result.ToString());

        return result;
    }

    void Update()
    {
        if (canReadInput)
        {
            if (Input.GetButtonUp("TopLeft"))
            {
                direction = MovementDirection.TopLeft;
                buttonPressed = true;
            }
            if (Input.GetButtonUp("TopRight"))
            {
                direction = MovementDirection.TopRight;
                buttonPressed = true;
            }
            if (Input.GetButtonUp("Left"))
            {
                direction = MovementDirection.Left;
                buttonPressed = true;
            }
            if (Input.GetButtonUp("Right"))
            {
                direction = MovementDirection.Right;
                buttonPressed = true;
            }
            if (Input.GetButtonUp("BottomLeft"))
            {
                direction = MovementDirection.BottomLeft;
                buttonPressed = true;
            }
            if (Input.GetButtonUp("BottomRight"))
            {
                direction = MovementDirection.BottomRight;
                buttonPressed = true;
            }
        }
    }

    #region Sounds

    public void AddSound(RecordableSounds sound, AudioClip clip)
    {
        sounds.Add(sound, clip);
    }

    public void PlaySound(RecordableSounds sound)
    {
        AudioClip soundToPlay;

        if (sounds.TryGetValue(sound, out soundToPlay))
        {
            audio.clip = soundToPlay;
            audio.Play();
        }
    }

    public void StopSound(RecordableSounds sound)
    {
        AudioClip soundToPlay;

        if (sounds.TryGetValue(sound, out soundToPlay))
        {
            if (audio.clip == soundToPlay)
                audio.Stop();
        }
    }

    #endregion

    void OnTriggerEnter(Collider other)
    {
        currentTile = other.gameObject;
    }

    void ApplyTriggerAction()
    {
        currentTile.BroadcastMessage("OnApplyTriggerAction");
    }

    void FixedUpdate ()
	{
        if (buttonPressed)
        {
            targetPosition = transform.position;
            Vector2 movementDirection = tileRadius * GetDirection(direction);
            targetPosition.x += movementDirection.x;
            targetPosition.y += movementDirection.y;
            buttonPressed = false;
            canReadInput = false;

            rigidbody.AddForce((targetPosition - transform.position) / (timeToNextTile * Time.fixedDeltaTime));
            PlaySound(RecordableSounds.Walking);
        }

        Vector3 toTarget = targetPosition - transform.position;

        if (toTarget.magnitude < 0.01f && !canReadInput)
        {
            // At destination
            StopSound(RecordableSounds.Walking);
            canReadInput = true;
            ApplyTriggerAction();
            rigidbody.velocity = Vector3.zero;
            rigidbody.position = targetPosition;
        }
	}
}
