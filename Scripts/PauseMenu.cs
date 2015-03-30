using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	GameObject pauseCanvas;
	GameObject pauseOptionsCanvas;
	bool pauseMode;
	bool mainOrSub;	//main is true, sub is false
	// Use this for initialization
	void Start () {
		mainOrSub = true;
		pauseCanvas = GameObject.Find("pauseCanvas");
		pauseOptionsCanvas = GameObject.Find ("optionsCanvas");
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		pauseCanvas.SetActive (false);
		pauseOptionsCanvas.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && !pauseMode && mainOrSub) {
			pauseMode = true;
			pauseCanvas.SetActive(true);
			Time.timeScale = 0f;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		else if (Input.GetKeyDown (KeyCode.Escape) && pauseMode){
			if (mainOrSub) {
				pauseMode = false;
				pauseCanvas.SetActive(false);
				Time.timeScale = 1f;
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
			}
			else {
				mainOrSub = true;
				pauseCanvas.SetActive(true);
				pauseOptionsCanvas.SetActive(false);
			}
		}
	}

	public void resumeButton () {
		pauseMode = false;
		pauseCanvas.SetActive(false);
		Time.timeScale = 1f;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void optionsButton () {
		mainOrSub = false;
		pauseOptionsCanvas.SetActive (true);
		pauseCanvas.SetActive (false);
	}

	public void back() {
		mainOrSub = true;
		pauseCanvas.SetActive (true);
		pauseOptionsCanvas.SetActive (false);
	}

	public void exitToMainMenu () {
		Time.timeScale = 1f;
		Application.LoadLevel ("MainMenu");
	}
	public void exitToDesktopButton () {
		Application.Quit();
	}
}