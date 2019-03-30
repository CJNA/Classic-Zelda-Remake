using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;
    Direction direction;

    public float waitTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        direction = GetComponent<Direction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!KeyMovement.is_Froze) {
            animator.SetFloat("horizontal_input", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("vertical_input", Input.GetAxisRaw("Vertical"));

            if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                animator.speed = 0.0f;
            } 
            else
            {
                animator.speed = 1.0f;
            }
        }
    }

    public void Attack() {
        if (gameObject.name != "Kenshiro") {
            animator.SetFloat("horizontal_input", 0);
            animator.SetFloat("vertical_input", 0);
            
            animator.SetTrigger("weapon_using");
            if (direction.GetRot() == 'S') {
                animator.Play("swingDown");
            } else if (direction.GetRot() == 'E') {
                animator.Play("swingEast");
            } else if (direction.GetRot() == 'W') {
                animator.Play("swingWest");
            } else {
                animator.Play("swingNorth");
            }
        }
        
    }

    private IEnumerator WaitTime() 
    {
        KeyMovement.is_Froze = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        animator.SetFloat("horizontal_input", 0);
        animator.SetFloat("vertical_input", 0);
        animator.Play("Obtain_Item");
        yield return new WaitForSeconds(waitTime);
        KeyMovement.is_Froze = false;
    }

    public void ObtainItem() {
        StartCoroutine(WaitTime());
    }
}
