using UnityEngine;
using System.Collections;

public class Platform2 : MonoBehaviour {

	bool platformUp;
	bool platformDown;
	Vector3 startingPosition;
	// Use this for initialization
	void Start () {
		platformUp = true;
		platformDown = false;
		startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (platformDown == true) {
			if(transform.position.y >= startingPosition.y-47){
				transform.Translate (new Vector3 (0, -5.5f, 0) * Time.deltaTime);
			}
			
		}
		if ( platformUp== true) {
			if(transform.position.y <= startingPosition.y){
				transform.Translate (new Vector3 (0, 5.5f, 0) * Time.deltaTime);
			}
			
			
		}
	}
	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.layer == 8) {
			platformUp = false;
			platformDown = true;
		}
		
	}
	void OnTriggerExit(Collider collider){
		if (collider.gameObject.layer == 8) {
			platformDown = false;
			platformUp = true;
		}
		
	}
}