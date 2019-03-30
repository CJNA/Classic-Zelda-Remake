using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMovement : MonoBehaviour
{
	public static bool isArrow;
	public static bool bombActive;
    public static bool god_Mode;
    public static bool is_Froze;
    public static float movement_speed = 4.0f;

    AnimationManager animationManager;
    Inventory inventory;
    Weapon_System weapon;
    Rigidbody rb;
    Killable HPManager;
    Direction direction;
    // Start is called before the first frame update
    void Start()
    {
    	isArrow = false;
    	bombActive = false;
        god_Mode = false;
        is_Froze = false;

        rb = GetComponent<Rigidbody>();
        HPManager = GetComponent<Killable>();
        animationManager = GetComponent<AnimationManager>();
        weapon = GetComponent<Weapon_System>();
        inventory = GetComponent<Inventory>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!is_Froze) {
            Vector2 current_input = GetInput();
            
            rb.velocity = current_input * movement_speed;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("God mode ran");
                if(god_Mode)
                {
                    god_Mode = false;
                }
                else
                {
                    god_Mode = true;
                }
            }
            if (Input.GetButtonDown("Swing")) {
                weapon.GetSwing();
                animationManager.Attack();
            } 

            if (Input.GetButtonDown("Arrow/Boomerang")) {
            	if(!isArrow) {
            		isArrow = true;
            	} else {
        			isArrow = false;
            	}
            }

            if (Input.GetButtonDown("Bomb_Activate")) {
            	if (bombActive) {
            		bombActive = false;
            	} else {
            		bombActive = true;
            	}
            }

            if (Input.GetButtonDown("Shoot")) {
                if (inventory.GetRupees() > 0 && isArrow) {
                    inventory.AddRupee(-1);
                    weapon.ShootArrow();
                } else if (inventory.GetBombs() > 0 && bombActive){
                    inventory.AddBomb(-1);
                    weapon.ShootBomb();
                } else {
                    weapon.ShootBoomerang();
                }
                animationManager.Attack();
            }
        }
    }

    Vector2 GetInput ()
    {
        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs (horizontal_input) > 0.0f)
        {
            vertical_input = 0.0f;
        }

        if(Mathf.Abs(vertical_input) > 0.0f)
        {
            horizontal_input = 0.0f;
        }

        return new Vector2(horizontal_input, vertical_input);
    }
}
