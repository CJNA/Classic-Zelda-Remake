using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HokutoManager : MonoBehaviour
{
	public static HokutoManager Instance = null;

	public bool comboSuccess = false;

	public GameObject[] HokutoEnemies; 
    public int numberOfEntry;
    public float arrowDistance;
    public float inputTime;

    public Canvas displayCanvas;
    public Image arrow_prefab;
    public Vector2 arrowStartPosition;
	public Sprite[] directionImage = new Sprite[4];
	public Direction directionComponent;
    public GameObject bombRangePrefab;
    public Vector3[] bombLoc = new Vector3[7];
    public float waitTime = 1.0f;

	private bool coroutineRunning = false;

    private GameObject[] bombNumber = new GameObject[7];
    
	Image[] arrowLists;
	char[] targetChar;

    // Start is called before the first frame update
   	void Awake() {
   		if (Instance != null) {
   			Destroy(gameObject);
   		} else {
   			Instance = this;
   		}
   		DontDestroyOnLoad(gameObject);
        displayCanvas = (Canvas)FindObjectOfType(typeof(Canvas));
   	}

    public void HokutouKen(GameObject target) {
		Debug.Log("Hokutouken");
    	Time.timeScale = 0;
        displayCanvas.transform.GetChild(1).gameObject.SetActive(true);
    	// Randomly Instantiate the arrows under the Canvas so that it's always visible by the player of keys, and take player input to check them 
    	numberOfEntry = Random.Range(numberOfEntry, numberOfEntry + 3);
    	arrowLists = new Image[numberOfEntry];
    	targetChar = new char[numberOfEntry];
    	for (int i = 0; i < numberOfEntry; i++) {
    		arrowLists[i] = Instantiate(arrow_prefab);
    		arrowLists[i].transform.SetParent(displayCanvas.transform, false);
    		arrowLists[i].GetComponent<RectTransform>().anchoredPosition = arrowStartPosition + new Vector2(arrowDistance * i, 0);
    		int randomNumber = Random.Range(0,3);

    		arrowLists[i].GetComponent<Image>().sprite = directionImage[randomNumber];
    		if (randomNumber == 0) {
    			targetChar[i] = 'E';
    		} else if (randomNumber == 1) {
    			targetChar[i] = 'W';
    		} else if (randomNumber == 2) {
    			targetChar[i] = 'S';
    		} else {
    			targetChar[i] = 'N';
    		}
    	}
    	StartCoroutine(forLoopRunner(target));
    }

    IEnumerator forLoopRunner(GameObject target) {
    	for (int i = 0; i < numberOfEntry; i++) {
    		Debug.Log("forloop");
    		yield return StartCoroutine(directionChecking(i));
    		if (comboSuccess) {
    			// Change the color when the color is yellow
    			arrowLists[i].GetComponent<Image>().color = Color.yellow;
    		} else {
    			break;
    		}
    	}
    	if (comboSuccess) {
    		StartCoroutine(OmaeDeath(target));
    	}
    	displayCanvas.transform.GetChild(1).gameObject.SetActive(false);
        for (int i = 0; i < numberOfEntry; i++) {
            Destroy(arrowLists[i]);
        }
        Vector3 direction = target.GetComponent<Rigidbody>().velocity * -1;
        PushManager.Instance.pushWrapper(direction, target);
    	Time.timeScale = 1.0f;

    }

    // Start timing / set float differecne with current time
    // As long as that sets comobo success, then it will exit correctly.
    IEnumerator inputCheck() {
    	// Instead of the corotuineRunning I check the timing
    	while (directionComponent.ManualUpdate() == 'X' && coroutineRunning) {
	        yield return null;
      	}
    }

    // Wait on the coroutine set the flag if it was succesful-> Check the flag and see if they succeeed the contiue

    IEnumerator directionChecking(int i) {
    	coroutineRunning = true;
    	directionComponent.SetNeutral();
    	StartCoroutine(inputCheck());
    	// Code after this executes after inputTime
    	yield return new WaitForSecondsRealtime(inputTime);
    	if (targetChar[i] == directionComponent.currentRot) {
    		comboSuccess = true;
    	} else {
    		comboSuccess = false;
    	}
    	coroutineRunning = false;
    }

    IEnumerator OmaeDeath(GameObject target) {
        AudioManager.Instance.OmaeSound();
        // Death Image / Destroy this object after few sec
        target.GetComponent<SpriteRenderer>().color = Color.red;
        Destroy(target.GetComponent<Hokudou_Movement>());
        yield return new WaitForSeconds(5.2f);
        target.GetComponent<Killable>().HealHP(1);
        StartCoroutine(detonate(target));
    }

    IEnumerator detonate(GameObject target) {
        yield return new WaitForSeconds(waitTime);
        AudioManager.Instance.BombSound();
        for (int i = 0; i < 7; i++) {
            bombNumber[i] = (GameObject)Instantiate(bombRangePrefab,target.transform);
            bombNumber[i].transform.localPosition = bombLoc[i];
            bombNumber[i].SetActive(true);
        }
        target.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < 7; i++) {
            Destroy(bombNumber[i]);
        }
        Destroy(target);
    } 
}
