using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WidgetFade : MonoBehaviour {

	/* The fading in/out speed of the stamina bar. */
	public float fadeInSpeed = 10.0f, fadeOutSpeed = 5.0f;

	/* Stores the Slider script of the widget. */
	private Slider slider;

	/* Stores the CanvasGroup to which the slider belongs. */
	private CanvasGroup canvasGroup;

	/* True if the widget is fading out. */
	private bool fade = true;

	// Use this for initialization
	void Start ()
	{
		// Caches the Slider component of the widget
		slider = GetComponent<Slider>();

		// Caches the canvasGroup to which the slider belongs
		canvasGroup = slider.GetComponentInParent<CanvasGroup>();

		// The stamina bar is initially invisible.
		canvasGroup.alpha = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// If the slider is fading out
		if(fade)
		{
			// Lerp the alpha of the slider to zero
			canvasGroup.alpha = Mathf.Lerp (canvasGroup.alpha, 0.0f, fadeOutSpeed * Time.deltaTime); 
		}
		// Else, if the slider is fading in
		else
		{
			// Fade in the canvas group, thus fading in the slider.
			canvasGroup.alpha = Mathf.Lerp (canvasGroup.alpha, 1.0f, fadeInSpeed * Time.deltaTime);
		}
	}

	public void FadeIn()
	{
		// Tell the Update function to fade in the stamina bar
		fade = false;
	}

	public void FadeOut()
	{
		fade = true;
	}
}
