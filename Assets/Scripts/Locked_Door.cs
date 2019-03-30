using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locked_Door : MonoBehaviour
{
	public Inventory inventory;
	public GameObject door1;
	public GameObject door2;
	public Sprite door1Sprite;
	public Sprite door2Sprite;

    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision) {
 		if (collision.gameObject.tag == "Player") {
 			if (inventory.GetKeys() > 0) {
 				inventory.AddKey(-1);
 				if (door1 && door2) {
	 				door1.GetComponent<SpriteRenderer>().sprite = door1Sprite;
					door2.GetComponent<SpriteRenderer>().sprite = door2Sprite;
 					door1.GetComponent<BoxCollider>().enabled = false;
 					door2.GetComponent<BoxCollider>().enabled = false;
 				} else {
 					this.GetComponent<SpriteRenderer>().sprite = door1Sprite;
 				}
 				this.GetComponent<BoxCollider>().isTrigger = true;
 				this.GetComponent<RoomTransition>().enabled = true;
 				AudioManager.Instance.OpenLockedDoor();
 			}
 		}
   	}
}
