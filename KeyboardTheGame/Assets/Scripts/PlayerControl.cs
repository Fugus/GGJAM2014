using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerControl : MonoBehaviour
{
    TileManager TileManager_;

    public float timeToNextTile = 0.2f;
    public float tileRadius = 20.0f;

    bool jumpDone = true;

    public enum RecordableSounds
    {
        Walking,
        GuitarSolo
    }

    public Dictionary<RecordableSounds, AudioClip> sounds;

    public enum MovementDirection
    {
        TopLeft,
        TopRight,
        Left,
        Right,
        BottomLeft,
        BottomRight
    }

	private Animator anim;					// Reference to the player's animator component.

    public bool unfogFirstTile = false;
    bool canReadInput = true;
    bool buttonPressed = false;
    MovementDirection direction;
    Vector3 targetPosition;

    GameObject currentTile;

	void Awake()
	{
		// Setting up references.
		anim = GetComponentInChildren<Animator>();
		Reset ();
	}

    void Start()
    {
        TileManager_ = GameObject.Find("/Managers").GetComponent<TileManager>();
        targetPosition = TileManager_.GetTilePosition(3, 2);

        //targetPosition = transform.position;
    }

	public void Reset()
	{
        sounds = new Dictionary<RecordableSounds, AudioClip>();
        unfogFirstTile = false;
        canReadInput = true;
        buttonPressed = false;

        rigidbody.WakeUp();

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

        return result;
    }

    void Update()
    {
        if (!unfogFirstTile && currentTile != null)
        {
            currentTile.BroadcastMessage("OnUnfog");
            unfogFirstTile = true;
        }

        if (canReadInput && currentTile != null)
        {

            TileScript tileScript = currentTile.GetComponent<TileScript>();
            if (tileScript != null)
            {

                Tile currentTileTile = tileScript.GetTile();

                Debug.Log("curr tile " + currentTileTile.letter);

                Tile neighbour = tileScript.GetNeighbour(MovementDirection.TopLeft);
                if (neighbour != null && Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), neighbour.letter)))
                {
                    direction = MovementDirection.TopLeft;
                    buttonPressed = true;
                }

                neighbour = tileScript.GetNeighbour(MovementDirection.TopRight);
                if (neighbour != null && Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), neighbour.letter)))
                {
                    direction = MovementDirection.TopRight;
                    buttonPressed = true;
                }

                neighbour = tileScript.GetNeighbour(MovementDirection.Left);
                if (neighbour != null && Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), neighbour.letter)))
                {
                    direction = MovementDirection.Left;
                    buttonPressed = true;
                }

                neighbour = tileScript.GetNeighbour(MovementDirection.Right);
                if (neighbour != null && Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), neighbour.letter)))
                {
                    direction = MovementDirection.Right;
                    buttonPressed = true;
                }

                neighbour = tileScript.GetNeighbour(MovementDirection.BottomLeft);
                if (neighbour != null && Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), neighbour.letter)))
                {
                    direction = MovementDirection.BottomLeft;
                    buttonPressed = true;
                }

                neighbour = tileScript.GetNeighbour(MovementDirection.BottomRight);
                if (neighbour != null && Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), neighbour.letter)))
                {
                    direction = MovementDirection.BottomRight;
                    buttonPressed = true;
                }

                if (buttonPressed && !CanMoveInDirection(direction))
                {
                    buttonPressed = false;
                }
            }
        }
    }

    bool CanMoveInDirection(MovementDirection direction)
    {        
        if (currentTile != null)
        {
            TileScript tileScript = currentTile.GetComponent<TileScript>();
            if (tileScript != null)
            {
                switch (direction)
                {
                    case MovementDirection.TopLeft:
                        return !TileManager_.HasWall(4, tileScript.TileIndexX, tileScript.TileIndexY) && !TileManager_.HasWall(1, tileScript.TileIndexX - 1 , tileScript.TileIndexY);
                    case MovementDirection.TopRight:
                        return !TileManager_.HasWall(5, tileScript.TileIndexX, tileScript.TileIndexY) && !TileManager_.HasWall(2, tileScript.TileIndexX - 1, tileScript.TileIndexY + 1);
                    case MovementDirection.Left:
                        return !TileManager_.HasWall(3, tileScript.TileIndexX, tileScript.TileIndexY) && !TileManager_.HasWall(0, tileScript.TileIndexX, tileScript.TileIndexY - 1);
                    case MovementDirection.Right:
                        return !TileManager_.HasWall(0, tileScript.TileIndexX, tileScript.TileIndexY) && !TileManager_.HasWall(3, tileScript.TileIndexX, tileScript.TileIndexY + 1);
                    case MovementDirection.BottomLeft:
                        return !TileManager_.HasWall(2, tileScript.TileIndexX, tileScript.TileIndexY) && !TileManager_.HasWall(5, tileScript.TileIndexX + 1, tileScript.TileIndexY - 1);
                    case MovementDirection.BottomRight:
                        return !TileManager_.HasWall(1, tileScript.TileIndexX, tileScript.TileIndexY) && !TileManager_.HasWall(4, tileScript.TileIndexX + 1, tileScript.TileIndexY);
                }

            }
        }

        return true;
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

    void OnTriggerExit(Collider other)
    {
        other.gameObject.BroadcastMessage("OnExitTile");
    }

    void EnterCurrentTile()
    {
        currentTile.BroadcastMessage("OnEnterTile");
    }

    void FixedUpdate()
	{
        if (buttonPressed)
        {
            targetPosition = transform.position;
            Vector2 movementDirection = GetDirection(direction);
            movementDirection *= TileManager.tileWidth;
            targetPosition.x += movementDirection.x;
            targetPosition.y += movementDirection.y;
            buttonPressed = false;
            canReadInput = false;
            jumpDone = false;

            rigidbody.AddForce((targetPosition - transform.position) / (timeToNextTile * Time.fixedDeltaTime));
            PlaySound(RecordableSounds.Walking);
            anim.SetTrigger("jump");
        }

        Vector3 toTarget = targetPosition - transform.position;

        if (toTarget.magnitude < 0.01f && !canReadInput)
        {
            // At destination
            if (jumpDone)
            {
                StopSound(RecordableSounds.Walking);
                canReadInput = true;
                EnterCurrentTile();
            }
            rigidbody.velocity = Vector3.zero;
            rigidbody.position = targetPosition;
        }
	}

    public void JumpDone()
    {
        jumpDone = true;
    }

}
