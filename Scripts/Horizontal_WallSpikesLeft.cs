using UnityEngine;
using System.Collections;

public class Horizontal_WallSpikesLeft : MonoBehaviour {
	public bool move;
	bool done;
	float hardcodedDistance;
	GameObject otherSpikes;

	private Vector3 startingPosition;

	// Use this for initialization
	void Start () {
		hardcodedDistance = 0;
		otherSpikes = GameObject.Find ("HR_T_spikeSetWITHDOOR_1");
		//compareDistance = transform.position.x + (otherSpikes.transform.position.x - transform.position.x)/2;
		//Debug.Log (compareDistance + " " + transform.position.x + " " + otherSpikes.transform.position.x);

		startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () { 
		if (!done) {
			if (hardcodedDistance < 650) {
				if (move) {
					hardcodedDistance += 1.0f * Time.deltaTime;
					transform.Translate (1.0f * Time.deltaTime, 0, 0);
				}
			}
			else {
				done = true;
				otherSpikes.GetComponent<Horizontal_WallSpikesRight>().done = true;
			}
		}
	}
}