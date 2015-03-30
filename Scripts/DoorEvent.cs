using UnityEngine;
using System.Collections;

public class DoorEvent : SwitchEvent
{
	// The sounds for the opening and closing door
	public AudioClip doorOpenSound, doorCloseSound;

	public bool doorOpen = false;

	// The amount which the door moves down when opening.
	public float openAmount = 4;
	
	Vector3 startingPosition;

	// Use this for initialization
	void Start () 
	{
		startingPosition = transform.position;

		if(doorOpen)
		{
			Vector3 position = startingPosition;
			transform.position.Set (position.x,position.y-openAmount,position.z);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (doorOpen == true) 
		{
			if(transform.position.y >= startingPosition.y-openAmount)
			{
				transform.Translate (new Vector3 (0, -3.5f, 0) * Time.deltaTime);
			}			
		}
		if (doorOpen == false) 
		{
			if(transform.position.y <= startingPosition.y)
			{
				transform.Translate (new Vector3 (0, 3.5f, 0) * Time.deltaTime);
			}
		}

		Vector3 position = transform.position;

		position.y = Mathf.Clamp (position.y, startingPosition.y-4, startingPosition.y);
		transform.position.Set (position.x,position.y,position.z);
	}
	
	public override void Activate()
	{

		if (doorOpen == true) 
		{
			AudioSource.PlayClipAtPoint (doorOpenSound, transform.position);
			doorOpen = false;
		}
		else if (doorOpen == false) 
		{
			AudioSource.PlayClipAtPoint (doorCloseSound, transform.position);
			doorOpen = true;
		}
		
	}
}
