using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	GameObject mainMenuCanvas;
	GameObject mainOptionsCanvas;

	// Use this for initialization
	void Start () {
		mainMenuCanvas = GameObject.Find ("mainMenuCanvas");
		mainOptionsCanvas = GameObject.Find ("mainOptionsCanvas");
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		mainOptionsCanvas.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void startGame(){
		Application.LoadLevel (1);
	}
	
	public void optionsButton () {
		mainOptionsCanvas.SetActive (true);
		mainMenuCanvas.SetActive (false);
	}

	public void back() {
		mainMenuCanvas.SetActive (true);
		mainOptionsCanvas.SetActive (false);
	}
	
	public void exit () {
		Application.Quit();
	}
}
