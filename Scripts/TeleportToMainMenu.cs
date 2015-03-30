using UnityEngine;
using System.Collections;

public class TeleportToMainMenu : MonoBehaviour {
	public string sceneToLoad = "MainMenu";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.layer == 8)
			Application.LoadLevel (sceneToLoad);
	}
}
