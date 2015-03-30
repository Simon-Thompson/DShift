using UnityEngine;
using System.Collections;

public class Stomp3Mover : MonoBehaviour {
	
	bool reverseDir;
	bool wait;
	float hardcodedDistance;
	// Use this for initialization
	void Start () {
		
	}	
	
	// Update is called once per frame
	void Update () {	
		if (!reverseDir && !wait) {
			if(!(hardcodedDistance > 2.75f)){
				hardcodedDistance += Time.deltaTime;
				transform.Translate (8 * Time.deltaTime, 0, 0);
			}
			else {
				StartCoroutine(waitAWhile(1));
				reverseDir = true;
			}
		} 
		else if (!wait) {
			transform.Translate (-8 * Time.deltaTime, 0, 0);
			hardcodedDistance -= Time.deltaTime;
			if(hardcodedDistance < 0){ //check for return to beginning position
				StartCoroutine(waitAWhile(1));
				reverseDir = false;
			}
		}
	}
	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.layer == 8) {
			//TODO: kill player
		}
	}
	
	IEnumerator waitAWhile(int x){
		wait = true;
		yield return new WaitForSeconds(x);
		wait = false;
	}
}