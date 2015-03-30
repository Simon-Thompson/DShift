using UnityEngine;
using System.Collections;

public class Horizontal_WallSpikesRight : MonoBehaviour {
	public bool move;
	public bool done;

	private Vector3 startingPosition;

	// Use this for initialization
	void Start () {
		startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () { 
		if (!done) {
			if (move) {
				transform.Translate (-1 * Time.deltaTime, 0, 0);
			}
		}
	}
	
	//React when colliding with player 
	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.layer == 8) {	//TODO: Needs bug check
			//player.die();	//TODO: combine scripts
			//DestroyObject(collider.gameObject);
		}	Debug.Log (collider.tag);
	}

	public void Reset()
	{
		transform.position = startingPosition;
	}




}