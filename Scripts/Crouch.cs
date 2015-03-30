using UnityEngine;
using System.Collections;

public class Crouch : MonoBehaviour 
{
	/* The speed at which the player stands back up after crouching. */
	public float standUpTransitionSpeed = 20.0f;
	/* The speed at which the player transitions from his walking to crouching height. */
	public float crouchTransitionSpeed = 10.0f;

	/* The height of the player when he's walking. */
	public float normalHeight = 2.0f;
	/* The height of the player when crouching. */
	public float crouchHeight = 1.0f;
	
	/* The height of the camera relative to the player when standing up. */
	public float normalCameraHeight = 1.75f;
	/* The height of the camera relative to the player whilst crouching. */
	public float crouchCameraHeight = 0.75f;

	/* The player's CharacterController */
	private CharacterController characterController;
	/* The camera used for the player to look around the world. */
	private Camera playerCamera;

	/* True if the player is crouching. */
	private bool crouch = false;

	// Use this for initialization
	void Start () 
	{
		// Caches the player's CharacterController 
		characterController = GetComponent<CharacterController>();
		
		// Retrieves the camera used by the player.
		playerCamera = GetComponentInChildren <Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// If the player is pressing down the crouch button
		if(Input.GetButtonDown ("Crouch"))
		{
			// Make the player crouch.
			crouch = true;
		}
		else if(Input.GetButtonUp ("Crouch"))
		{
			// Stop the player from crouching.
			crouch = false;
		}

		// Updates the player's height depending on his crouching state
		UpdateCrouch ();
	}

	// Makes the player crouch or stand up depending on his current state
	public void UpdateCrouch()
	{
		// Stores the target height of the character controller and the camera
		float targetHeight = 0.0f;
		float targetCameraHeight = 0.0f;
		
		// Holds the speed at which the player transitions from his current to target height
		float transitionSpeed = 0.0f;
		
		// If the player is crouching
		if(crouch)
		{
			// Set the target player and camera height to their crouching values
			targetHeight = crouchHeight;
			targetCameraHeight = crouchCameraHeight;
			
			// Set the transition speed of the player's height
			transitionSpeed = crouchTransitionSpeed;
		}
		// Else, if the player isn't crouching
		else
		{
			// Set the target player and camera height to their normal values
			targetHeight = normalHeight;
			targetCameraHeight = normalCameraHeight;
			
			// Set the speed at which the player stands back up.
			transitionSpeed = standUpTransitionSpeed;
		}
		
		// Lerp the character controller's height to his target height
		characterController.height = Mathf.Lerp (characterController.height, targetHeight, 
		                                         transitionSpeed * Time.deltaTime);
		// Computes the pivot point of the character controller to be at his feet
		float pivotY = characterController.height + 0.01f;
		
		// Updates the player's pivot point
		characterController.center.Set (0,pivotY,0);
		
		// Move the camera to follow his new height. 
		float cameraY = Mathf.Lerp (playerCamera.transform.localPosition.y, 
		                            targetCameraHeight, transitionSpeed * Time.deltaTime);
		
		// Updates the height of the player camera to match the player's height.
		playerCamera.transform.localPosition.Set (0,cameraY,0);
		
	}
}
