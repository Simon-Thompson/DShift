using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DShift : MonoBehaviour {
	
	/* The max duration of a d-shift */
	public float shiftLength = 3.0f;
	
	/* The relative speed at which the d-shift stamina recharges. */
	public float rechargeRate = 0.75f;
	
	/* Sound that plays once d-shift activates. */
	public AudioClip activateSound;
	
	/* The AudioSource used to play the d-shift sounds. */
	private AudioSource audioSource;
	
	/* Holds the slider widget used to display d-shift stamina */
	private Slider staminaSlider;
	/* Holds the fading script for the stamina slider. */
	private WidgetFade staminaSliderFade;
	
	// Stores all GameObjects that should appear whilst in d-shift
	private GameObject[] dshiftObjects;
	
	/* The amount of time left for d-shift. */
	private float stamina;
	
	/* True if the d-shift has been activated. */
	private bool active = false;
	
	// Use this for initialization
	void Awake () 
	{
		Debug.Log ("ShiftSlider " + GameObject.Find ("ShiftSlider").GetComponent<WidgetFade>());
		
		// Initialize the stamina of the d-shift to the max duration of the d-shift.
		stamina = shiftLength;
		
		// Caches the AudioSource component of the player
		audioSource = GetComponent<AudioSource>();
		
		// Caches all GameObjects in the DShift world
		dshiftObjects = GameObject.FindGameObjectsWithTag ("DShift");
		
		Debug.Log ("ShiftSlider " + staminaSliderFade);
		
		// Hides all d-shift objects initially, until d-shift is activated..
		HideDShiftObjects();
		
		Debug.Log ("ShiftSlider " + GameObject.Find ("ShiftSlider").GetComponent<WidgetFade>());
		
		// Caches the Slider widget to display the d-shift stamina bar
		staminaSlider = GameObject.Find ("ShiftSlider").GetComponent<Slider>();
		
		// Caches the Fade script for the stamina slider.
		staminaSliderFade = GameObject.Find ("ShiftSlider").GetComponent<WidgetFade>();
		
		Debug.Log ("ShiftSlider " + staminaSliderFade);
		
		// Sets the max value of the stamina slider to the max duration of the d-shift
		staminaSlider.maxValue = shiftLength;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// If the right mouse button has been pressed
		if(Input.GetButtonDown("Fire2"))
		{
			// Toggle D-Shift
			ActivateDShift();
		}
		
		// Updates the timer for the D-Shift
		UpdateStamina();
		
		// Updates the stamina slider to display the value of the d-shift timer.
		GameObject.Find ("ShiftSlider").GetComponent<Slider>().value = stamina;
		
		// If the d-shift has passed its maximum duration or the user has released
		// the d-shift button
		if(active && (stamina <= 0 || Input.GetButtonUp ("Fire2")))
		{
			// Deactivate the D-Shift
			DeactivateDShift();
		}
	}
	
	/* Updates the d-shift timer. */
	private void UpdateStamina()
	{
		// If D-shift is currently active
		if(active)
		{
			// Decrement the amount of d-shift stamina left 
			stamina -= Time.deltaTime;
		}
		//Else, if d-shift is inactive
		else
		{
			// Increment the d-shift's stamina to recharge.
			stamina += Time.deltaTime * rechargeRate;
		}
		
		// Clamp the d-shift timer so that it never 
		stamina = Mathf.Clamp(stamina, 0, shiftLength);
		
		// If d-shift stamina is full
		if(stamina >= shiftLength)
		{
			// Fade out the stamina slider
			staminaSliderFade.FadeOut();
		}
		
	}
	
	// Start activating D-Shift
	private void ActivateDShift()
	{
		// Activate d-shift
		active = true;
		
		// Fade in the stamina slider
		staminaSliderFade.FadeIn();
		
		// Plays the d-shift activate sound
		
		
		audioSource.PlayOneShot(activateSound);
		
		// Display all the objects in the d-shift world.
		ShowDShiftObjects();
	}
	
	/* Deactivate D-Shift */
	private void DeactivateDShift()
	{
		// De-activate d-shift
		active = false;
		
		audioSource.Stop();
		
		// Hide all objects in the d-shift world
		HideDShiftObjects ();
	}
	
	// Displays all the GameObjects in the DShift world
	private void ShowDShiftObjects()
	{
		// Cycles through all objects in the d-shift layer.
		foreach(GameObject gameObject in dshiftObjects)
		{
			// Activates the d-shift object.
			gameObject.GetComponent<DShiftObject>().Activate ();
		}
	}
	
	// Hides all objects in the d-shift world.
	private void HideDShiftObjects()
	{
		// Cycles through all objects in the d-shift layer.
		foreach(GameObject gameObject in dshiftObjects)
		{
			if(gameObject.GetComponent<DShiftObject>() != null)
				// Deactivates the d-shift object
				gameObject.GetComponent<DShiftObject>().Deactivate ();
		}
	}
}
