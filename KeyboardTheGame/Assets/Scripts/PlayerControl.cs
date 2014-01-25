using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
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

	void Awake()
	{
		// Setting up references.
		anim = GetComponent<Animator>();
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

	void FixedUpdate ()
	{
        if (buttonPressed)
        {
            targetPosition = transform.position;
            Vector2 movementDirection = GetDirection(direction);
            targetPosition.x += movementDirection.x;
            targetPosition.y += movementDirection.y;
            buttonPressed = false;
            canReadInput = false;
        }

        Vector3 toTarget = targetPosition - transform.position;

        if (toTarget.magnitude < 0.0001f && !canReadInput)
        {
            // At destination
            canReadInput = true;
            rigidbody2D.velocity = Vector3.zero;
        }
        else
        {
            rigidbody2D.AddForce(toTarget / (Time.fixedDeltaTime * Time.fixedDeltaTime));
        }
	}
}
