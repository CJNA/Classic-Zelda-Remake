using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goriya : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int direction = 0;
    public float timer = 0;
    public float projectileSpeed = 5.0f;
    public GameObject boomerang_prefab;
    public Sprite[] boomerangSprites = new Sprite[2];

    GameObject boomerang;
    Animator anim;
    Animator boomerang_animator;
    Direction directionComponent;
    Rigidbody rb;
    private bool boomerangFlying;

    Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left};

    Coroutine currentMove;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentMove = StartCoroutine(Move());
        directionComponent = GetComponent<Direction>();
        anim = GetComponent<Animator>();
    }

    void Update() {
    	if (directionComponent.GetRot() == 'E') {
    		anim.SetInteger("Direction", 1);
		} else if (directionComponent.GetRot() == 'W') {
			anim.SetInteger("Direction", 2);
		} else if (directionComponent.GetRot() == 'N') {
			anim.SetInteger("Direction", 3);
		} else if (directionComponent.GetRot() == 'S') {
			anim.SetInteger("Direction", 0);
		}
		timer += Time.deltaTime;
		if (timer > 4) {
			ShootBoomerang();
			timer = 0;
		}
    }

    IEnumerator Move()
    {
        while (true)
        {
            Vector2 dir = directions[Random.Range(0, directions.Length)];
            for (float moved = 0; moved < 1; moved += moveSpeed * Time.deltaTime)
            {
                rb.velocity = dir * moveSpeed;
                yield return null;
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Enemy") {
            StopCoroutine(currentMove);
            currentMove = StartCoroutine(Move());
        }
    }

    public void ShootBoomerang() {
        if (!boomerangFlying) {
            // Shoot the collider
            rb.velocity = Vector3.zero;
            Vector3 boomerangLoc = Vector3.zero;
            Vector3 rotation = new Vector3(0, 0, 0);

            boomerang = Instantiate(boomerang_prefab, this.GetComponent<Transform>());
            boomerang.AddComponent<SpriteRenderer>();
            boomerang.GetComponent<SpriteRenderer>().sortingOrder = 1;
            boomerang_animator = boomerang.GetComponent<Animator>();

            if(directionComponent.GetDiagonal() == "None") {
                if(directionComponent.GetRot() == 'E') {
                    boomerangLoc = new Vector3(1f, 0, 0);
                    rotation = new Vector3(0, 0, 90f);
                    boomerang.GetComponent<SpriteRenderer>().sprite = boomerangSprites[0];
                    boomerang_animator.Play("Boomerang East");
                } else if (directionComponent.GetRot() == 'W') {
                    boomerangLoc = new Vector3(-1f, 0, 0);
                    rotation = new Vector3(0, 0, -90f);
                    boomerang.GetComponent<SpriteRenderer>().sprite = boomerangSprites[0];
                    boomerang_animator.Play("Boomerang West");
                } else if (directionComponent.GetRot() == 'N') {
                    boomerangLoc = new Vector3(0, 1f, 0);
                    rotation = new Vector3(0, 0, 0);
                    boomerang_animator.Play("Boomerang North");
                    boomerang.GetComponent<SpriteRenderer>().sprite = boomerangSprites[1];
                } else if (directionComponent.GetRot() == 'S') {
                    boomerangLoc = new Vector3(0, -1f, 0);
                    rotation = new Vector3(0, 0, 0);
                    boomerang.GetComponent<SpriteRenderer>().sprite = boomerangSprites[0];
                    boomerang_animator.Play("Boomerang South");
                }
            } else {
                if (directionComponent.GetDiagonal() == "NE") {
                    boomerangLoc = new Vector3(1, 1f, 0);
                    rotation = new Vector3(0, 0, 45);
                    boomerang_animator.SetTrigger("Boomerang North");
                    boomerang.GetComponent<SpriteRenderer>().sprite = boomerangSprites[1];
                } else if (directionComponent.GetDiagonal() == "NW") {
                    boomerangLoc = new Vector3(-1, 1f, 0);
                    rotation = new Vector3(0, 0, -45);
                    boomerang_animator.SetTrigger("Boomerang North");
                    boomerang.GetComponent<SpriteRenderer>().sprite = boomerangSprites[1];
                } else if (directionComponent.GetDiagonal() == "SW") {
                    boomerangLoc = new Vector3(-1, -1f, 0);
                    rotation = new Vector3(0, 0, 0);
                    boomerang.GetComponent<SpriteRenderer>().sprite = boomerangSprites[0];
                    boomerang_animator.SetTrigger("Boomerang South");
                } else {
                    boomerangLoc = new Vector3(1, -1f, 0);
                    rotation = new Vector3(0, 0, 0);
                    boomerang.GetComponent<SpriteRenderer>().sprite = boomerangSprites[0];
                    boomerang_animator.SetTrigger("Boomerang South");
                }
            }

            boomerang.transform.localPosition = boomerangLoc;
            boomerang.transform.localEulerAngles = rotation;

            boomerang.SetActive(true);

            StartCoroutine(DelayShoot(boomerang, projectileSpeed));
        }
    }

    public void boomerangReturn() {
        boomerangFlying = false;
    }

     IEnumerator DelayShoot(GameObject weapon, float shootForce) {
        Vector3 forceVector = Vector3.zero;

        AudioManager.Instance.BoomerangAttack();
        if (directionComponent.GetRot() == 'E') {
            forceVector = new Vector3(shootForce, 0 , 0);
        }   else if (directionComponent.GetRot() == 'W') {
            forceVector = new Vector3(-shootForce, 0 , 0);
        } else if (directionComponent.GetRot() == 'N') {
            forceVector = new Vector3(0, shootForce, 0);
        } else if (directionComponent.GetRot() == 'S') {
            forceVector = new Vector3(0, -shootForce, 0);
        }
     	
        weapon.GetComponent<Rigidbody>().AddForce(forceVector);
        yield return new WaitForSeconds(0.3f);
    }
}
