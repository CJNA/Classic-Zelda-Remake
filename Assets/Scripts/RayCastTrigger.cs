using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTrigger : MonoBehaviour
{
	public char targetDirection1;
	public char targetDirection2;
	public float speed;

	private float step;
	private bool coroutineRunning;
	private Vector3 originalPosition;
	private Vector3[] targetPositions = new Vector3[4];
    // Update is called once per frame
	void Start() {
		originalPosition = this.transform.position;
		targetPositions[0] = new Vector3(this.transform.position.x - 5.5f, this.transform.position.y, 0);
		targetPositions[1] = new Vector3(this.transform.position.x + 5.5f, this.transform.position.y, 0);
		targetPositions[2] = new Vector3(this.transform.position.x, this.transform.position.y - 2.5f, 0);
		targetPositions[3] = new Vector3(this.transform.position.x, this.transform.position.y + 2.5f, 0);
	}

    void FixedUpdate()
    {	
    	LayerMask layer = LayerMask.GetMask("Player");
    	RaycastHit hit;
    	step = speed * Time.fixedDeltaTime;

    	if (!coroutineRunning) {
    		if ((targetDirection1 == 'W' || targetDirection2 == 'W') && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 15, layer)) {
        	this.transform.position = Vector3.MoveTowards(this.transform.position, targetPositions[0], step);
	        } else if ((targetDirection1 == 'E' || targetDirection2 == 'E') && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 15, layer)) {
	        	this.transform.position = Vector3.MoveTowards(this.transform.position, targetPositions[1], step);
	    	} else if ((targetDirection1 == 'S' || targetDirection2 == 'S') && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10, layer)) {
	    		this.transform.position = Vector3.MoveTowards(this.transform.position, targetPositions[2], step);
			} else if ((targetDirection1 == 'N' || targetDirection2 == 'N') && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 10, layer)) {
				this.transform.position = Vector3.MoveTowards(this.transform.position, targetPositions[3], step);
			} else {
				CoroutineWrapper();
			}
    	}
    }

    void OnTriggerEnter(Collider collider) {
    	if (collider.transform.tag == "Player") {
    		collider.gameObject.GetComponent<Killable>().Damage(1);
    	}

    	CoroutineWrapper();
    }

    void CoroutineWrapper() {
    	if (!coroutineRunning) {
	    	coroutineRunning = true;
	    	StartCoroutine(CoroutineGoesBack());
    	}
    }

    IEnumerator CoroutineGoesBack() {
    	while(this.transform.position != originalPosition) {
    		this.transform.position = Vector3.MoveTowards(this.transform.position, originalPosition, step);
    		yield return null;
    	}
    	coroutineRunning = false;
    }
}
