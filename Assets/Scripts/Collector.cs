using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public Inventory inventory;
    Killable HPManager;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        HPManager = GetComponent<Killable>();
        if (inventory == null && GetComponentInParent<Inventory>() == null)
        {
            Destroy(this);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        GameObject object_collided_with = other.gameObject;
        if (object_collided_with.tag == "rupee")
        {
            if(inventory != null)
            {
                inventory.AddRupee(1);
            }
            Destroy(object_collided_with);
            AudioManager.Instance.RupeeEffect();
        } else if (object_collided_with.tag == "Restoring_Heart")
        {
            if (HPManager != null) {
                HPManager.HealHP(1);
            }
            Destroy(object_collided_with);
            AudioManager.Instance.RestroingHeartEffect();
        } else if (object_collided_with.tag == "Key") {
            if(inventory != null) {
                inventory.AddKey(1);
            }
            Destroy(object_collided_with);
            AudioManager.Instance.KeyEffect();
        } else if (object_collided_with.tag == "Bomb") {
        	if (inventory != null) {
        		inventory.AddBomb(1);
        	}
        	Destroy(object_collided_with);
        	AudioManager.Instance.KeyEffect();
        } else if (object_collided_with.tag == "ImportantItem") {
            if (object_collided_with.name == "HPStone(Clone)") {
                if (HPManager != null) {
                    HPManager.AddHP();
                }
            }
            if (object_collided_with.name == "Boomerang_item(Clone)") {
                gameObject.GetComponent<Weapon_System>().gotBoomerang();
            }
            if (object_collided_with.name == "Bow") {
                gameObject.GetComponent<Weapon_System>().gotBow();
                GetComponent<AnimationManager>().ObtainItem();
            }
            Destroy(object_collided_with);
            AudioManager.Instance.ObtainItem();
            if (object_collided_with.name == "Triforce") {
                AudioManager.Instance.BeatTheDungeon();
                GetComponent<AnimationManager>().ObtainItem();
            }

        }
    }
}
