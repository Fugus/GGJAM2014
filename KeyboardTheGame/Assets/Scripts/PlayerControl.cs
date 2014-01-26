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

	public enum RecordableSounds
	{
		jumping,
		teleporting,
		surveying,
		victory,
		reading,
		confused
	}
	
	public Dictionary<RecordableSounds, AudioClip> sounds;
	public Dictionary<RecordableSounds, int> soundLengths;

	void Awake()
	{
		// Setting up references.
		anim = GetComponentInChildren<Animator>();
		sounds = new Dictionary<RecordableSounds, AudioClip>();
		// Set up sound lengths, these really should be stored elsewhere but whatever its been about 46 hours since I slept. If it works it works. Plus, we can make Fus Roh DA noises if I do this.
		soundLengths = new Dictionary<RecordableSounds, int>();
		soundLengths.Add(RecordableSounds.jumping, 1);
		soundLengths.Add(RecordableSounds.teleporting, 3);
		soundLengths.Add(RecordableSounds.surveying, 2);
		soundLengths.Add(RecordableSounds.victory, 6);
		soundLengths.Add(RecordableSounds.reading, 1);
		soundLengths.Add(RecordableSounds.confused, 1);
	}

    void Start()
    {
        TileManager_ = GameObject.Find("/Managers").GetComponent<TileManager>();
        targetPosition = TileManager_.GetTilePosition(3, 2);
    }

	public void Reset()
	{
        sounds = new Dictionary<RecordableSounds, AudioClip>();
		currentTile = null;
		// position on initial tilee
		GameObject.Find("/Player").transform.position = Vector3.zero;
		unfogFirstTile = false;
		//EnterCurrentTile();
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
		if(sounds.ContainsKey(sound)) sounds[sound] = clip;
		else sounds.Add(sound, clip);
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

    void OrientToDirection(Vector2 movementDirection)
    {
        GameObject playerVisuals = GameObject.FindGameObjectWithTag("PlayerVisuals");

        float angle = Vector3.Angle(Vector3.right, new Vector3(movementDirection.x, movementDirection.y, 0.0f));
        if (Vector3.Dot(Vector3.up, new Vector3(movementDirection.x, movementDirection.y, 0.0f)) < 0.0f)
        {
            angle *= -1;
        }

        Quaternion newRot = playerVisuals.transform.rotation;
        Vector3 newEulerAngles = newRot.eulerAngles;
        newEulerAngles.z = 60.0f + Mathf.Round(angle / 60.0f) * 60.0f;
        newRot.eulerAngles = newEulerAngles;
        playerVisuals.transform.rotation = newRot;
    }

    void FixedUpdate()
	{
        if (buttonPressed)
        {
            targetPosition = transform.position;
            Vector2 movementDirection = GetDirection(direction);

            OrientToDirection(movementDirection);

            movementDirection *= TileManager.tileWidth;
            targetPosition.x += movementDirection.x;
            targetPosition.y += movementDirection.y;
            buttonPressed = false;
            canReadInput = false;
            jumpDone = false;

            rigidbody.AddForce((targetPosition - transform.position) / (timeToNextTile * Time.fixedDeltaTime));
            PlaySound(RecordableSounds.jumping);
            anim.SetTrigger("jump");
        }

        Vector3 toTarget = targetPosition - transform.position;

        if (toTarget.magnitude < 0.01f && !canReadInput)
        {
            // At destination
            if (jumpDone)
            {
                //StopSound(RecordableSounds.jumping);
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
