using UnityEngine;
using System.Collections;

public class SaveState : MonoBehaviour 
{
	// The save points the player has moved through.
	private ArrayList savePoints;

	private ArrayList resetObjects;
	
	// Use this for initialization
	void Start () 
	{
		savePoints = new ArrayList();

		resetObjects = FindGameObjectsWithLayer (13);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	private ArrayList FindGameObjectsWithLayer (int layer) {
		GameObject[] goArray = GameObject.FindObjectsOfType<GameObject>();
		var goList = new ArrayList();
		for (var i = 0; i < goArray.Length; i++) {
			if (goArray[i].layer == layer) {
				goList.Add(goArray[i]);
			}
		}
		if (goList.Count == 0) {
			return null;
		}
		return goList;
	}

	/* Spawns the player at the position of his last save. */
	public void loadFromLastSave()
	{
		// Spawn the player from his last save point.
		GameObject lastSavePoint = ((GameObject)savePoints[savePoints.Count-1]);
		//Vector3 savePointForward = lastSavePoint.transform.TransformDirection(Vector3.forward);

		transform.position = lastSavePoint.transform.position;
		transform.rotation = lastSavePoint.transform.rotation;

		foreach (GameObject go in resetObjects) {
			go.GetComponent<Reset>().ResetObject ();
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		// If the player hit a save point
		if(collider.gameObject.layer == 11)
		{
			Debug.Log("Player hit save point");

			// If the SavePoint has not been encountered before
			if(!savePoints.Contains(collider.gameObject))
			{
				Debug.Log ("Add save point " + collider.gameObject);
				// Add the gameObject to the list of player save points.
				savePoints.Add(collider.gameObject);
			}
		}
		// Else, if the player ran into a "Death" layer
		else if(collider.gameObject.layer == 12)
		{
			DeathObject deathComponent = collider.GetComponent <DeathObject>();

			if(deathComponent == null || deathComponent.doesKill)
				// Respawn the player at his last save point.
				loadFromLastSave ();

		}
	}


}
