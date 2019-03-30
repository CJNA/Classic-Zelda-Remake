using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
	public Vector3 targetLoc;
	public float push;
  public float dmg;
	public float speed;

	void Start() {
		followPlayer();
	}

	void Update() {
	  	targetLoc = GameObject.Find("Player").transform.position;
	}

  	public void followPlayer() {
        StartCoroutine(followCoroutine());
	}

	IEnumerator followCoroutine() {
	  	float step = speed * Time.deltaTime;
	  	while (this.gameObject.transform.position != targetLoc) {
        	this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, targetLoc, step);
        	yield return null;
      	}
    }

  void OnCollisionEnter(Collision collision) {
      if (collision.gameObject.tag == "Player") {
        collision.gameObject.GetComponent<Killable>().Damage(dmg);
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Transform otherTransform = collision.gameObject.GetComponent<Transform>();
        Vector3 direction = otherTransform.localPosition - this.gameObject.GetComponent<Transform>().localPosition;
        PushManager.Instance.pushWrapper(direction, collision.gameObject);
        Destroy(this.gameObject);
      } else if (collision.gameObject.tag == "Untagged") {
        Destroy(this.gameObject);
      }
    }
}
