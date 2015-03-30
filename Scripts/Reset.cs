using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour {

	private Vector3 startingPosition;
	
	// Use this for initialization
	void Start () {
		
		startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResetObject()
	{
		transform.position = startingPosition;
	}
}
