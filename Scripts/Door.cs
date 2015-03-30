using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	bool doorOpen;
	bool doorClose;
	Vector3 startingPosition;
	// Use this for initialization
	void Start () {
		doorOpen = false;
		doorClose = false;
		startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (doorOpen == true) {
			if(transform.position.y >= startingPosition.y-7.5){
				transform.Translate (new Vector3 (0, -4.5f, 0) * Time.deltaTime);
			}
		
		}
		if (doorClose == true) {
			if(transform.position.y <= startingPosition.y){
			transform.Translate (new Vector3 (0, 4.5f, 0) * Time.deltaTime);
			}

		
		}
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.layer == 8) {
			doorOpen = true;
			doorClose = false;
		}
	
	}
	void OnTriggerExit(Collider collider){
		if (collider.gameObject.layer == 8) {
			doorClose = true;
			doorOpen = false;
		}

	}
}
