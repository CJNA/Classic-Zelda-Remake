using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Item : MonoBehaviour
{
	public GameObject Item;

	// What can i use instead of OnDestroy?
    public void spawn_Item()
    {
        GameObject drop = (GameObject)Instantiate(Item, transform.position, transform.rotation);
        drop.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
}
