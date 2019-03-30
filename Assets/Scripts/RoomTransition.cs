using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public Camera mainCamera;
    public float speed = 0.005f;
    public string roomDirection;
    public bool transition;
    public GameObject player;

    private Vector3 targetPosition;
    private bool coroutineRunning = false;
    private Vector3 linkTargetPosition;
    // Update is called once per frame

    void OnTriggerEnter(Collider collider) {
     		if (collider.gameObject.tag == "Player") {
     			transition = true;
          calcTargetDist();
     		}
   	}

   	void Update() {
   		if (transition) {
        coroutineWrapper();
   		}
   	}

   	private IEnumerator CameraMovement() {
      // starting Position -> record where the camera is and camera 
      Vector3 startPosition = mainCamera.transform.position;
  		KeyMovement.is_Froze = true;
      float step = speed * Time.deltaTime;
      while (mainCamera.transform.position != targetPosition) {
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, step);
        yield return null;
      }
      // Link Walking animation when entering the door / Walking slowly to get there(?)
      player.transform.position = linkTargetPosition;
      KeyMovement.is_Froze = false;
  		yield return new WaitForSeconds(2.5f);
      // Teleport Complete at this frame.
      transition = false;
      coroutineRunning = false;
   	}

    private void coroutineWrapper() {
      if(!coroutineRunning) {
        coroutineRunning = true;
        StartCoroutine(CameraMovement());
      } 
    }

    void calcTargetDist() {
      if (roomDirection == "East") {
        targetPosition = new Vector3(mainCamera.transform.position.x + 16, mainCamera.transform.position.y, -10);
        linkTargetPosition = this.transform.position + new Vector3(4, 0, 0);
      }  
      if (roomDirection == "West") {
        targetPosition = (mainCamera.transform.position - new Vector3(16, 0, 0));
        linkTargetPosition = this.transform.position + new Vector3(-4, 0, 0);
      }
      if (roomDirection == "North") {
        targetPosition = (mainCamera.transform.position + new Vector3(0, 11, 0));
        linkTargetPosition = this.transform.position + new Vector3(0, 4, 0);
      }
      if (roomDirection == "South") {
        targetPosition = (mainCamera.transform.position - new Vector3(0, 11, 0));
        linkTargetPosition = this.transform.position + new Vector3(0, -4, 0);
      }
    }
}
