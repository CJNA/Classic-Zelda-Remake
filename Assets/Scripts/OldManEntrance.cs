using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManEntrance : MonoBehaviour
{
	public Sprite sprite;

	public void ChangeDoor() {
		this.GetComponent<SpriteRenderer>().sprite = sprite;
		this.GetComponent<RoomTransition>().enabled = true;
		this.GetComponent<BoxCollider>().isTrigger = true;
	}
}
