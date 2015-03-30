using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public Transform teleportObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.layer == 8) {
			collider.transform.position = teleportObject.position;
			collider.transform.rotation = teleportObject.rotation;
		}
	}

}
