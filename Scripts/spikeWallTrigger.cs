using UnityEngine;
using System.Collections;

public class SpikeWallTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.layer == 8) {;
			GameObject.Find("HR_T_spikeSetWITHDOOR_1").GetComponent<Horizontal_WallSpikesRight>().move = true;
			GameObject.Find("HR_T_spikeSet_2").GetComponent<Horizontal_WallSpikesLeft>().move = true;
			GameObject.Find("HR_T_spikeSet_1").GetComponent<Horizontal_WallSpikesLeft>().move = true;
		}
	}
}
