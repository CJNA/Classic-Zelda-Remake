using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallmaster : MonoBehaviour
{
    public float moveSpeed = 2f;
    public GameObject wallMasterSpawn;
    Rigidbody rb;

    Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left};

    Coroutine currentMove;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentMove = StartCoroutine(Move());
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

    void OnCollisionEnter(Collision collision) {
    	if (collision.gameObject.tag == "Player") {
    		collision.transform.position = new Vector3(39.5f, 3f, 0.0f);
    		Camera.main.transform.position = new Vector3(39.5f, 6.5f, -10f);
    	}
        if (collision.gameObject.name == "Tile_WALL") {
            this.gameObject.transform.position = wallMasterSpawn.transform.position;
        }
    }
}
