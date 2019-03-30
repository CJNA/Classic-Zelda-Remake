using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombDamage : MonoBehaviour
{
	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Enemy") {
			collider.gameObject.GetComponent<Killable>().Damage(1);
		}
	}
}
