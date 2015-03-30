using UnityEngine;
using System.Collections;

public class VerticalBackNForth_SpikeStomper : MonoBehaviour {
	Vector3 startingPos;
	bool reverseDir;
	bool wait;
	Rigidbody rigidBody;
	// Use this for initialization
	void Start () {
		startingPos = transform.position;
		reverseDir = false;
		rigidBody = this.gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!reverseDir && !wait) {
			transform.Translate (0, -5 * Time.deltaTime, 0);
		} 
		else if (!wait) {
			transform.Translate (0, 3 * Time.deltaTime, 0);
			if (startingPos.y <= transform.position.y) {	//check for return to beginning position
				reverseDir = false;
				StartCoroutine(waitAWhile(2));
			}
		}
		rigidBody.angularVelocity = Vector3.zero;
		//Debug.Log ("Stomper 1: " + startingPos.x + " " + startingPos.y + " " + startingPos.z);
	}

	//React when colliding with player or the floor
	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.layer == 8) {
			//player.die();	//TODO: combine scripts
			//DestroyObject(collider.gameObject);
		}
		if (collider.gameObject.layer == 9) {
			reverseDir = true;
			StartCoroutine(waitAWhile(2));
		}
	}

	IEnumerator waitAWhile(int x){
		wait = true;
		yield return new WaitForSeconds(x);
		wait = false;
	}
}