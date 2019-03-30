using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushManager : MonoBehaviour
{
	public static PushManager Instance = null;
	public float pushForce = 2.0f;
	public float knockBackTime = 2f;
	public bool coRoutineRunning = false;
    // Start is called before the first frame update
   	void Awake() {
   		if (Instance != null) {
   			Destroy(gameObject);
   		} else {
   			Instance = this;
   		}
   		DontDestroyOnLoad(gameObject);
   	}

    // Update is called once per frame
    public void pushWrapper(Vector3 direction, GameObject target)
    {
        if (!coRoutineRunning) {
        	StartCoroutine(pushApply(direction, target));
        }
    }

    private IEnumerator pushApply(Vector3 direction, GameObject target) {
      if (target) {
          target.GetComponent<Rigidbody>().velocity = pushForce * direction;
      }
        yield return new WaitForSeconds(knockBackTime);
      if (target) {
          target.GetComponent<Rigidbody>().velocity = Vector3.zero;
      }
    }
}
