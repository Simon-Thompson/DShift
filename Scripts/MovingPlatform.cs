using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
	public float moveAmount = 7.0f;

	bool move;
	bool reverseDir;
	bool wait;
	Vector3 startingPosition;
	// Use this for initialization
	void Start () {
		startingPosition = transform.position;
	}	
	
	// Update is called once per frame
	void Update () {
		if (move) {	
			if (!reverseDir && !wait) {
				if(transform.position.y <= (startingPosition.y+moveAmount)){
					transform.Translate (0, 3 * Time.deltaTime, 0);
				}
				else {
					reverseDir = true;
				}
			} 
			else if (!wait) {
				transform.Translate (0, -3 * Time.deltaTime, 0);
				if(transform.position.y <= startingPosition.y){ //check for return to beginning position
					reverseDir = false;
				}
			}
		}
	}
	void OnTriggerEnter(Collider collider){
		if (!move) {
			if (collider.gameObject.layer == 8) {
				StartCoroutine(waitAWhile(1));
				move = true;
			}
		}
	}
	/*void OnTriggerExit(Collider collider){
		if (collider.gameObject.layer == 8) {
			move = false;
		}
	}*/

	IEnumerator waitAWhile(int x){
		wait = true;
		yield return new WaitForSeconds(x);
		wait = false;
	}
}