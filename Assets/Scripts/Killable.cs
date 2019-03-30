using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Killable : MonoBehaviour
{
    public bool isPlayer = false;
    public float current_HP = 3;
    public float max_HP = 3;
    public float invincibleTime = 2;
    public float pushForce = 0;
    public float timeScale = 2.0f;
    public bool isInvincible = false;
    public char curDirection = 'V';

    public RectTransform healthbar;
    public float maxSize;

    private char lastDirection;
    private Vector3 curPosition;
    private Direction dirComponent;

   	void Start() {
   		dirComponent = this.gameObject.GetComponent<Direction>();
        healthbar = this.transform.GetChild(0).Find("Background").GetChild(0).gameObject.GetComponent<RectTransform>();
        maxSize = healthbar.sizeDelta.x;
   	}

   	void Update() {

        // Grid based movement Implementation
   		char rotation = dirComponent.GetRot();

   		if (rotation == 'E' || rotation == 'W') {
   			curDirection = 'H';
   		} else if (rotation == 'S' || rotation == 'N') {
   			curDirection = 'V';
   		}

        if (lastDirection != curDirection) {
        	if (curDirection == 'H' && lastDirection == 'V') {
        		this.transform.position = new Vector3(this.transform.position.x, (float)Math.Round(2.0 * (double)this.transform.position.y, MidpointRounding.AwayFromZero) / 2.0f, 0.0f);	
        	} else if (curDirection == 'V' && lastDirection == 'H') {
        		this.transform.position = new Vector3((float)Math.Round(2.0 * (double)this.transform.position.x, MidpointRounding.AwayFromZero) / 2.0f, this.transform.position.y, 0.0f);
        	}
        }
   		lastDirection = curDirection;
        if (GetCur_HP() < 1 && isPlayer) {
            AudioManager.Instance.DangerEffect();
        }
   	}

    public void Damage(float amount)
    {
        if (KeyMovement.god_Mode && isPlayer) {
            return;
        } 

    	if(!isInvincible) {
    		current_HP -= amount;
	        if (current_HP <= 0) {
	            if (isPlayer) {
	            	//Reset the variables
                    StartCoroutine(deadSoundWait());
		            SceneManager.LoadScene("Dungeon");
	            }
                AudioManager.Instance.MonsterDeath();
                if (gameObject.GetComponent<Spawn_Item>()) {
                    gameObject.GetComponent<Spawn_Item>().spawn_Item();
                }
	            Destroy(this.gameObject);
	        }
            healthbar.sizeDelta = new Vector2((float)current_HP/ max_HP * maxSize, healthbar.sizeDelta.y);
            StartCoroutine(invincibility());
    	}
    }

    public void HealHP(int i) {
        if (current_HP + i > max_HP) {
            current_HP = max_HP;
        } else {
            current_HP += i;
        }
    }

    public void AddHP() {
        max_HP += 1;
        current_HP += 1;
    }

    public float GetCur_HP()
    {
        return current_HP;
    }

    public float GetMax_HP()
    {
        return max_HP;
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.tag == "Enemy" || collided.tag == "Weapon") {
	        if(isPlayer && collided.tag == "Enemy")
	        {
                AudioManager.Instance.PlayerGetPushed();
	            Damage(0.5f);
	        } 
        	Transform otherTransform = collision.gameObject.GetComponent<Transform>();
	       	Vector3 direction = this.gameObject.GetComponent<Transform>().localPosition - otherTransform.localPosition;
	       
	       	PushManager.Instance.pushWrapper(direction, this.gameObject);
        }
    }

    private IEnumerator invincibility() {
        isInvincible = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        if (isPlayer) {
            KeyMovement.is_Froze = true;
        }
        yield return new WaitForSeconds(invincibleTime);
        if (isPlayer) {
            KeyMovement.is_Froze = false;
        }
        isInvincible = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator deadSoundWait() {
        AudioManager.Instance.DeadSound();
        yield return new WaitForSeconds(0.5f);
    }
}
