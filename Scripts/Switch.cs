using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour 
{
	/* The objects which will be activated once the switch is pressed. */
	public GameObject[] objectsToActivate;

	/* The max distance (squared) at which the player can press the button. */
	private float maxPressDistance = 3.0f * 3.0f;

	/* The max angle at which the player can look at the button to press it. */
	private float maxPressAngle = 50.0f;

	/* Stores the player in cache */
	private GameObject player;

	/* The camera used by the player to look around the world. */
	private Camera playerCamera;

	// Use this for initialization
	void Start () 
	{
		// Cache the Player gameObject
		player = GameObject.Find ("Player");

		// Cache the player's camera
		playerCamera = player.GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown (KeyCode.E))
		{
			// Computes the distance between the player and the switch
			Vector3 distance = transform.position - playerCamera.transform.position;

			// Computes the angle between the player's forward direction and the button
			float angle = Vector3.Angle (distance, player.transform.forward);

			Debug.Log ("Distance: " + distance.magnitude + " Angle: " + angle);

			// If the player is close enough to the button, and is looking straight at the button
			if(distance.sqrMagnitude <= maxPressDistance && Mathf.Abs (angle) <= maxPressAngle)
			{
				// Activate the button
				PressButton();
			
				Debug.Log ("Button Pressed");
			}
		}
	}

	/* Press the button and activate the corresponding GameObjects. */
	public void PressButton()
	{
		// Cycles through each object the button has to activate
		for(int i = 0; i < objectsToActivate.Length; i++)
		{
			// Stores the GameObject being cycled through
			GameObject gameObject = objectsToActivate[i];

			// Activate the GameObject linked to the button
			gameObject.GetComponent<SwitchEvent>().Activate();
		}

		// Play the button's "Pressed" animation.
		GetComponent<Animation>().Play ();
	}
}
