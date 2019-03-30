using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowRoom : MonoBehaviour
{
	public Vector3 location;
	public Vector3 cameraLoc;

	//23.11, 50.39

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player") {
			collider.gameObject.transform.position = location;
			Camera.main.transform.position = cameraLoc;
		}
	}
}
