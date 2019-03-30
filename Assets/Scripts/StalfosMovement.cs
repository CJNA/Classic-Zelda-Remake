using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalfosMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
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

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Enemy") {
            StopCoroutine(currentMove);
            currentMove = StartCoroutine(Move());
        }
    }
}
