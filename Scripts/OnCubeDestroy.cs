using UnityEngine;
using System.Collections;

public class OnCubeDestroy : MonoBehaviour {

	public DoorEvent doorToOpen;

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.layer == 15)
			doorToOpen.Activate ();
	}

}
