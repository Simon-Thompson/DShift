using UnityEngine;
using System.Collections;

public class DShiftObject : MonoBehaviour 
{
	public bool fadeOutOnActivate;
	// The speed at which the object fades in/out
	public float fadeInSpeed = 12.5f, fadeOutSpeed = 10.0f; 

	public bool disableDirectly = false;
	
	// Caches the object's MeshRenderer
	private MeshRenderer meshRenderer;
	
	/* Stores the d-shift object's collider. */
	private BoxCollider boxCollider;
	
	/* True if the object is fading in or out. */
	private bool fadeIn = false, fadeOut = false;
	
	// Use this for initialization
	void Awake () 
	{
		// Caches the GameObject's MeshRenderer
		meshRenderer = GetComponent<MeshRenderer>();
		
		// Caches the GameObject's collider
		boxCollider = gameObject.GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Fades the object in or out depending on its state.
		UpdateFade();
		
	}
	
	// Activates the d-shift object
	public void Activate()
	{
		if(fadeOutOnActivate)
		{
			foreach(BoxCollider boxCollider in GetComponents<BoxCollider>())
				boxCollider.isTrigger = true;

			// If a death object
			if (gameObject.layer == 12) 
			{
				DeathObject deathObject = GetComponent<DeathObject> ();
				if(deathObject != null)
					deathObject.doesKill = false;
			}

			// Fade out the object
			fadeIn = false;
			fadeOut = true;

		} else {
			foreach(BoxCollider boxCollider in GetComponents<BoxCollider>())
				boxCollider.isTrigger = false;

			// If a death object
			if (gameObject.layer == 12)
			{ 
				DeathObject deathObject = GetComponent<DeathObject> ();
				if(deathObject != null)
					deathObject.doesKill = true;
			}

			// Fade in the object
			fadeIn = true;
			fadeOut = false;
		}
	}
	
	// Deactivates the d-shift object
	public void Deactivate()
	{
		if(fadeOutOnActivate)
		{
			foreach(BoxCollider boxCollider in GetComponents<BoxCollider>())
				boxCollider.isTrigger = false;

			// If a death object
			if (gameObject.layer == 12)
			{
				DeathObject deathObject = GetComponent<DeathObject> ();
				if(deathObject != null)
					deathObject.doesKill = true;
			}
			
			fadeOut = false;
			fadeIn = true;
		} else {
			foreach(BoxCollider boxCollider in GetComponents<BoxCollider>())
				boxCollider.isTrigger = true;

			// If a death object
			if (gameObject.layer == 12)
			{
				DeathObject deathObject = GetComponent<DeathObject> ();
				if(deathObject != null)
					deathObject.doesKill = false;
			}

			// Fade out the object
			fadeOut = true;
			fadeIn = false;
		}
	}
	
	// Fades in or out the GameObject depending on its state
	public void UpdateFade()
	{
		// If the object is not fading, return the function.
		if(!fadeIn && !fadeOut)
			return;
		
		// Stores the target transparency of the object
		float targetAlpha = 0.0f;
		// Holds the fading speed of the object.
		float fadeSpeed = fadeOutSpeed;
		
		// If the object has to fade in
		if (fadeIn) {
			// Set the target alpha to opaque
			targetAlpha = 1.0f;
			// Set the speed at which the object fades in
			fadeSpeed = fadeInSpeed;

			if (disableDirectly)
			{
				meshRenderer.enabled = true;
				foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
					mr.enabled = true;
			}
		} else {
			if (disableDirectly)
			{
				meshRenderer.enabled = false;
				foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
					mr.enabled = false;
			}
		}

		// Stores the current color of the object's MeshRenderer
		Color currentColor = meshRenderer.material.color;
		
		// Slerps the object's color to its target transparency.
		currentColor.a = Mathf.Lerp (currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
		
		// Update the color of the d-shift object
		meshRenderer.material.color = currentColor;

		foreach(MeshRenderer mr in transform.GetComponentsInChildren<MeshRenderer>())
		{
			// Stores the current color of the object's MeshRenderer
			Color childColor = mr.material.color;
			
			// Slerps the object's color to its target transparency.
			childColor.a = Mathf.Lerp (childColor.a, targetAlpha, fadeSpeed * Time.deltaTime);

			// Update the color of the d-shift object
			mr.material.color = childColor;

			//if(gameObject.layer == 12)
				//Debug.Log ("Color: " + mr.material);
		}
	}
}
