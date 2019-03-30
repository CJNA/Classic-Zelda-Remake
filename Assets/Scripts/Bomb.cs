using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	public GameObject bombRangePrefab;
	public Vector3[] bombLoc = new Vector3[7];
	public float waitTime = 1.0f;

	private GameObject[] bombNumber = new GameObject[7];

	// Bomb first flies (certain distance / or onCollisionEnter -> hits the wall etc)
	// Starts the detonating process (coroutine for 2.5 sec etc)
	// Instantiate the bomb detonator to cover more space near it.
	// Destroy itself, and damage all the object that is OntriggerStay 
    // Start is called before the first frame update

    // Update is called once per frame

    void Start() {
    	StartCoroutine(detonate());
    }

    IEnumerator detonate() {
    	yield return new WaitForSeconds(waitTime);
        AudioManager.Instance.BombSound();
    	for (int i = 0; i < 7; i++) {
    		bombNumber[i] = (GameObject)Instantiate(bombRangePrefab,this.transform);
    		bombNumber[i].transform.localPosition = bombLoc[i];
    		bombNumber[i].SetActive(true);
    	}
    	gameObject.GetComponent<SpriteRenderer>().enabled = false;
    	yield return new WaitForSeconds(waitTime);
    	for (int i = 0; i < 7; i++) {
    		Destroy(bombNumber[i]);
    	}
    	Destroy(gameObject);
    } 

}
