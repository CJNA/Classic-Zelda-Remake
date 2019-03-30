using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Manager : MonoBehaviour
{
	public GameObject[] targetMonsters;
	public GameObject triggerObject;
	public GameObject rewardObject;
	public Transform rewardLocation;

	public Sprite closedDoorSprite;
	private Sprite openedDoorSprite;
	private int targetDestroyed = 0;
    // Update is called once per frame

   	void Start() {
   		if (this.name == "Tile_Trap") {
   			openedDoorSprite = GetComponent<SpriteRenderer>().sprite;
   			GetComponent<SpriteRenderer>().sprite = closedDoorSprite;
   		}
   	}
    void Update()
    {
    	for (int i = 0; i < targetMonsters.Length; i++) {
    		if (!targetMonsters[i]) {
    			targetDestroyed++;
    		}
    	}

     	if (targetMonsters.Length == targetDestroyed) {
   			if (this.name == "Tile_Trap") {
   				GetComponent<SpriteRenderer>().sprite = openedDoorSprite;
  			} 

     		if (triggerObject) {
				triggerObject.GetComponent<Pushable_Wall>().pushCondition = true;
     		}

     		if (rewardObject) {
     			if (rewardLocation) {
     				GameObject alpha = (GameObject)Instantiate(rewardObject, rewardLocation.position, rewardLocation.rotation);
     				alpha.GetComponent<SpriteRenderer>().sortingOrder = 1;
     			}
          		AudioManager.Instance.RoomClear();
     		}
     		Destroy(this);
     	}
     	targetDestroyed = 0;
    }

    void OnTriggerEnter(Collider collider)
    {
      if (collider.gameObject.tag == "Player") {
          for (int i = 0; i < targetMonsters.Length; i++) {
          	if (targetMonsters[i]) {
	            targetMonsters[i].SetActive(true);
          	}
          }
      }
    }
}
