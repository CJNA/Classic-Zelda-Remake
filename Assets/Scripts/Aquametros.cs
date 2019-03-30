using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aquametros : MonoBehaviour
{
	private float timer = 0;
	public int shootingTimer = 2;
	public float moveSpeed = 2f;
	public GameObject fireball_prefab;
	public Vector3[] fireball_Loc = new Vector3[3];
	public GameObject[] fireballs = new GameObject[3];
	public Vector3 leftEnd;
	public Vector3 rightEnd;
    public bool roar = false;

    Rigidbody rb;
    Animator anim;

    Vector2[] directions = { Vector2.right, Vector2.left};

    Coroutine currentMove;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentMove = StartCoroutine(Move());
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    	timer += Time.deltaTime;
        if (timer > shootingTimer) {
            AudioManager.Instance.BossAttack(); 
            ShootingFireBall();
            timer = 0;
        } else {
        	if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Aquametros Shooting")) {
	        	anim.SetBool("Shooting", false);
        	}
        }
    }

    void ShootingFireBall() {
    	for (int i = 0; i < 3; i++) {
    		fireballs[i] = (GameObject)Instantiate(fireball_prefab, this.transform);
    		fireballs[i].transform.localPosition = fireball_Loc[i];
    	}
    	anim.SetBool("Shooting", true);
    }

    IEnumerator Move()
    {
        while (true)
        {
            Vector2 dir = directions[Random.Range(0, directions.Length)];
            for (float moved = 0; moved < 1; moved += moveSpeed * Time.deltaTime)
            {
            	if (this.transform.position.x > rightEnd.x) {
            		dir = directions[1];
            	} else if (this.transform.position.x < leftEnd.x) {
            		dir = directions[0];
            	}
                rb.velocity = dir * moveSpeed;
                yield return null;
            }
        }
    }
}
