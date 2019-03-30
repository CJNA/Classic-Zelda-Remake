using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 39 6.5 -10
public class OldManRoomTransition : MonoBehaviour
{
	private bool isEntering;

	public GameObject OldManEntrance;
    public GameObject exit;
	public Sprite lockedDoorSprite;
	public GameObject[] display = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        isEntering = true;
    }

    // Update is called once per frame
    void OnTriggerEnter() {
    	if (isEntering) {
    		for (int i = 0; i < 4; i++) {
    			display[i].SetActive(true);
    		}
    		isEntering = false;
    	} else {
    		exit.GetComponent<RoomTransition>().enabled = true;
    		OldManEntrance.GetComponent<BoxCollider>().isTrigger = false;
    		OldManEntrance.GetComponent<RoomTransition>().enabled = false;
    		OldManEntrance.GetComponent<SpriteRenderer>().sprite = lockedDoorSprite;
    	}
    }
}
