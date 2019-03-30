using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable_Wall : MonoBehaviour
{
	public Vector3[] location = {Vector3.up, Vector3.down, Vector3.left, Vector3.right};
	public Direction directionComponent;
	public OldManEntrance oldManEntrance;
	public bool pushCondition;
	public int timer;
	public float speed;

	private Vector3 initialLoc;
	private float timeVar = 0;
	private bool isMoving;
	private bool coroutineRunning = false;

	void Start() {
		initialLoc = transform.position;
	}

	void Update() {
		if (timeVar > timer) {
			isMoving = true;
			Vector3 targetLoc;
			if (directionComponent.GetRot() == 'E') {
				targetLoc = initialLoc + Vector3.right;
			} else if (directionComponent.GetRot() == 'W') {
				targetLoc = initialLoc + Vector3.left;
			} else if (directionComponent.GetRot() == 'S') {
				targetLoc = initialLoc + Vector3.down;
			} else {
				targetLoc = initialLoc + Vector3.up;
			}
			moveWrapper(targetLoc);
		}
	}

	void OnCollisionStay(Collision collision) {
		if (collision.gameObject.tag == "Player" && pushCondition) {
			timeVar += Time.deltaTime;
		}
	}

	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			if (!isMoving) {
				timeVar = 0;	
			}
		}
	}

	public void moveWrapper(Vector3 target) {
		if (!coroutineRunning) {
	        StartCoroutine(moveCoroutine(target));
		}
	}

	IEnumerator moveCoroutine(Vector3 target) {
		coroutineRunning = true;
	  	float step = speed * Time.deltaTime;
	  	while (this.gameObject.transform.position != target) {
        	this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, target, step);
        	yield return null;
      	}
      	if (oldManEntrance) {
			oldManEntrance.ChangeDoor();
      	}
		Destroy(this);
    }
}
