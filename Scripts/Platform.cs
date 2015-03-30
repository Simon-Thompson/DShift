using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	bool platformUp;
	bool platformDown;
	Vector3 startingPosition;
	// Use this for initialization
	void Start () {
		platformUp = false;
		platformDown = true;
		startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (platformUp == true) {
			if(transform.position.y <= startingPosition.y+52){
				transform.Translate (new Vector3 (0, 5.5f, 0) * Time.deltaTime);
			}
			
		}
		if ( platformDown== true) {
			if(transform.position.y >= startingPosition.y){
				transform.Translate (new Vector3 (0, -5.5f, 0) * Time.deltaTime);
			}
			
			
		}
	}
	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.layer == 8) {
			platformUp = true;
			platformDown = false;
		}
		
	}
	void OnTriggerExit(Collider collider){
		if (collider.gameObject.layer == 8) {
			platformDown = true;
			platformUp = false;
		}
		
	}
}
