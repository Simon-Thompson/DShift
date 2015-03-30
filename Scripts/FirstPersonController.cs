using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

	/* The player's movement speed */
	public float walkSpeed = 5.0f;

	/* The player's movement speed whilst crouching. */
	public float crouchSpeed = 5.0f;
	
	/* The player's vertical jump speed. */
	public float jumpSpeed = 10.0f;

	/* The normal falling gravity and jumping gravity of the player. */
	public float fallGravity = -9.81f;
	public float jumpGravity = -26.0f;
	
	/* The speed of the camera movement. */
	public float mouseSensitivity = 2.0f;

	/* The maximum angle at which the player can look up or down. */
	public float rotationLimit = 60.0f;

	/* The player's CharacterController */
	private CharacterController characterController;
	/* The camera used for the player to look around the world. */
	private Camera playerCamera;

	/* The vertical rotation of the player's camera. */
	private float rotationVertical = 0.0f;

	/* The ground speed of the player. */
	private float movementSpeed;
	/* The current y-velocity of the player. */
	private float verticalVelocity = 0.0f;

	/* True if the player is jumping. */
	private bool isJumping = false;

	/* The amount of time the player can jump after having left the ground. */
	private float jumpHelperTime = 0.1f;

	private float lastJumpTime = 0.0f;

	/* The amount of time the player has been off the ground. */
	private float notGroundedTimer = 0.0f;

	private bool previousGrounded = false;

	/* The vertical gravity currently being applied on the player. */
	private float gravity = Physics.gravity.y;

	// Use this for initialization
	void Start () 
	{
		// Hide the cursor
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		// Caches the player's CharacterController 
		characterController = GetComponent<CharacterController>();

		// The player initially moves at the speed of his walk
		movementSpeed = walkSpeed;

		// The default amount of gravity applied on the player is his fall gravity.
		gravity = fallGravity;

		// Retrieves the camera used by the player.
		playerCamera = GetComponentInChildren <Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Upda the player's general movement.
		UpdateMovement();

	}

	public void UpdateMovement()
	{
		//Rotation

		if (Time.timeScale != 0) {
			// Coumput the rotation of the player based on mouse movement.
			float rotationHorizontal = Input.GetAxis ("Mouse X") * mouseSensitivity;
			
			// Rotate the player horizontally by the pre-computed values
			transform.Rotate (0, rotationHorizontal, 0);
			
			// Compute the vertical rotation of the camera based on mouse input
			rotationVertical -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
			rotationVertical = Mathf.Clamp (rotationVertical, -rotationLimit, rotationLimit);
			
			// Rotate the camera vertically based on mouse input
			playerCamera.transform.localRotation = Quaternion.Euler (rotationVertical, 0, 0);
		}
		
		// Movement
		
		// Computes  the player's vertical and horizontal speed based on input direction
		float forwardSpeed = Input.GetAxis ("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis ("Horizontal") * movementSpeed;

		// Apply gravity to the player's vertical velocity
		verticalVelocity += gravity * Time.deltaTime;
		
		// If the player can jump
		if (CanJump ()) {
			// Apply the jump speed on the player's vertical velocity
			verticalVelocity = jumpSpeed;

			// The player is jumping.
			isJumping = true;

			lastJumpTime = Time.time;

			// Sets the gravity applied on the player to his jumping gravity.
			gravity = jumpGravity;
		}
		// If the player is grounded but is not jumping
		else if (characterController.isGrounded) {
			// Set the gravity of the player to his normal falling gravity.
			gravity = fallGravity;
		}

		if (characterController.isGrounded) 
		{
			// The player is not jumping
			isJumping = false;
			notGroundedTimer = 0;
		}
		else if (!characterController.isGrounded) 
		{
			if(Time.time - lastJumpTime >= jumpHelperTime && notGroundedTimer == 0)
			{
				verticalVelocity = -7;
				Debug.Log ("Zero gravity");
			}

			notGroundedTimer += Time.deltaTime;
		}

		// If the player is pressing down the crouch button
		if(Input.GetButtonDown ("Crouch"))
		{
			// Make the player move at crouching speed.
			movementSpeed = crouchSpeed;
		}
		// Else, if the player released the crouch button
		else if(Input.GetButtonUp ("Crouch"))
		{
			// Make the player move at normal walking speed
			movementSpeed = walkSpeed;
		}
		
		// Stores the speed of the player in a Vector3
		Vector3 speed = new Vector3 (sideSpeed, verticalVelocity, forwardSpeed);
		// Make the player move in the direction of the camera. 
		speed = transform.rotation * speed;
		
		// Applies the input speed to move the player.
		characterController.Move (speed * Time.deltaTime);

		previousGrounded = characterController.isGrounded;
	}

	private bool CanJump()
	{
		if (isJumping || !Input.GetButtonDown ("Jump"))
			return false;

		if (Time.time - lastJumpTime <= jumpHelperTime)
			return false;

		return (characterController.isGrounded)
			|| (!characterController.isGrounded && notGroundedTimer <= jumpHelperTime);
	}
	
}
