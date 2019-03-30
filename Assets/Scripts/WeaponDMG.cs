using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDMG : MonoBehaviour
{
  public string weaponType;
  public float dmg;

  void OnCollisionEnter(Collision collision) {
      if (collision.gameObject.tag == "Enemy") {
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
