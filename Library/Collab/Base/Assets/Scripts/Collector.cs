﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public AudioClip rupee_collection_sound_clip;
    public AudioClip restoring_heart_collection_sound_clip;
    Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogWarning("WARNING: Gameobject with a collector has no inventory to store things in!");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        GameObject object_collided_with = other.gameObject;
        if (object_collided_with.tag == "rupee")
        {
            if(inventory != null)
            {
                inventory.AddRupees(1);
            }

            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(rupee_collection_sound_clip, Camera.main.transform.position);
        }
        // Colliding with "Restoring Heart"
        if (object_collided_with.tag == "Restoring_Heart")
        {
            if (inventory != null) {
                inventory.HealHP(1);
            }
            Destroy(object_collided_with);
            AudioSource.PlayClipAtPoint(restoring_heart_collection_sound_clip, Camera.main.transform.position);
        }
    }
}