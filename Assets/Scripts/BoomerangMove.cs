using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangMove : MonoBehaviour
{
    public Vector3 targetLoc;
    public float dmg = 0.5f;
    public float push = 20f;
	public float speed;

    // Update is called once per frame
    // transform.parent.position has to constantly get updated
    void Update() {
        targetLoc = transform.parent.position;
    }

    public void returnToOwner() {
        StartCoroutine(returnCoroutine());
    }

    IEnumerator returnCoroutine() {
	  	float step = speed * Time.deltaTime;
	  	while (this.gameObject.transform.position != targetLoc) {
        	this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, targetLoc, step);
        	yield return null;
      	}
    }

    void OnCollisionEnter(Collision collision) {
      if (collision.gameObject == transform.parent.gameObject) {
        if (transform.parent.gameObject.tag == "Player") {
            transform.parent.GetComponent<Weapon_System>().boomerangReturn();
        } else {
            transform.parent.GetComponent<Goriya>().boomerangReturn();
        }

        Destroy(this.gameObject);
      } else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player") {
        if (collision.gameObject.layer == 12 || collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Killable>().Damage(dmg);
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

            Transform otherTransform = collision.gameObject.GetComponent<Transform>();
            Vector3 direction = otherTransform.localPosition - this.gameObject.GetComponent<Transform>().localPosition;
            PushManager.Instance.pushWrapper(direction, collision.gameObject);
            returnToOwner();
        }
      } else if (collision.gameObject.tag == "Untagged") {
        returnToOwner();
      }
    }
}
