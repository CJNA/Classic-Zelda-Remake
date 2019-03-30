using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hokudou_Movement : MonoBehaviour
{
    public int triggerHP;
    public float triggerEveryTime;
    public float triggerDuration;
    public float moveSpeed = 2f;

    private bool canTrigger;

    private Killable HPManager;
    Rigidbody rb;

    Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left};

    Coroutine currentMove;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentMove = StartCoroutine(Move());
        HPManager = GetComponent<Killable>();
        InvokeRepeating("FistTrigger", 0, triggerEveryTime);
        canTrigger = false;
    }

    void FistTrigger() {
        if (HPManager.GetCur_HP() <= triggerHP) {
            // 33% chance
            int randomNumber = Random.Range(0, 2);
            if (randomNumber == 0) {
                TriggerWrapper();
            }
        }
    }

    void TriggerWrapper() {
        if (!canTrigger) {
            StartCoroutine(TriggerOn());
        }
    }

    IEnumerator TriggerOn() {
        canTrigger = true;
        // Later Aesthetic make it a flash
        GetComponent<SpriteRenderer>().color = Color.blue;
        yield return new WaitForSeconds(triggerDuration);
        GetComponent<SpriteRenderer>().color = Color.white;
        canTrigger = false;
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
        if (canTrigger && collision.gameObject.tag == "Player") {
            HokutoManager.Instance.HokutouKen(gameObject);
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
