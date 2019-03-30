using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_System : MonoBehaviour
{
    public GameObject sword_prefab;
    public GameObject arrow_prefab;
    public GameObject boomerang_prefab;
    public GameObject bomb_prefab;
    public float laserDistance;
    public float projectileSpeed;
    public float bombSpeed;
    public Sprite[] swordSprites = new Sprite[4];
    public Sprite[] arrowSprites = new Sprite[2];
    public Sprite[] boomerangSprites = new Sprite[4];
    public float swordWaitTime;

    private Rigidbody rb;
    private GameObject sword;
    private GameObject arrow;
    private GameObject boomerang;
    private bool ownBoomerang = false;
    private bool ownBow = false;
    private bool boomerangFlying = false;
    private GameObject bomb;
    private IEnumerator coroutine;
    private SpriteRenderer spriteRend;
    private Killable HPManager;
    private Inventory inventory;
    private Direction dirComponent;
    private Animator animator;
    private Animator boomerang_animator;

    // Start is called before the first frame update
    void Start()
    {    
        HPManager = this.GetComponent<Killable>();
        dirComponent = this.gameObject.GetComponent<Direction>();
        spriteRend = this.gameObject.GetComponent<SpriteRenderer>();
        inventory = GetComponent<Inventory>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void GetSwing() {

        KeyMovement.is_Froze = true;
        KeyMovement.movement_speed = 0;

        Vector3 swordLoc = Vector3.zero;
        Vector3 rotation = new Vector3(0, 0, 0);

        sword = Instantiate(sword_prefab, this.GetComponent<Transform>());
        sword.AddComponent<SpriteRenderer>();
        sword.GetComponent<SpriteRenderer>().sortingOrder = 1;

        if(dirComponent.GetRot() == 'E') {
            if (gameObject.name == "Kenshiro") {
                swordLoc = new Vector3(0.15f, 0, 0);
                rotation = new Vector3(0, 0, 90f);
            } else {
                 swordLoc = new Vector3(1f, 0, 0);
                rotation = new Vector3(0, 0, 90f);
            }
           
            sword.GetComponent<SpriteRenderer>().sprite = swordSprites[0];
        } else if (dirComponent.GetRot() == 'W') {
            if (gameObject.name == "Kenshiro") {
                swordLoc = new Vector3(-0.15f, 0, 0);
                rotation = new Vector3(0, 0, -90f);
            } else {
            swordLoc = new Vector3(-1f, 0, 0);
            rotation = new Vector3(0, 0, -90f);
            }
            sword.GetComponent<SpriteRenderer>().sprite = swordSprites[0];
        } else if (dirComponent.GetRot() == 'N') {
            if (gameObject.name == "Kenshiro") {
                swordLoc = new Vector3(0f, 0.15f, 0);
                rotation = new Vector3(0, 0, 0f);
            } else {
            swordLoc = new Vector3(0, 1f, 0);
            rotation = new Vector3(0, 0, 0);
            }
            sword.GetComponent<SpriteRenderer>().sprite = swordSprites[2];
        } else if (dirComponent.GetRot() == 'S') {
            if (gameObject.name == "Kenshiro") {
                swordLoc = new Vector3(0f, -0.15f, 0);
                rotation = new Vector3(0, 0, 0f);
            } else {
                swordLoc = new Vector3(0, -1f, 0);
                rotation = new Vector3(0, 0, 0);
            }
            sword.GetComponent<SpriteRenderer>().sprite = swordSprites[0];
        }
        
        sword.transform.localPosition = swordLoc;
        sword.transform.localEulerAngles = rotation;

        sword.SetActive(true);

        StartCoroutine(DelayLaser(swordWaitTime));
    }

    public void ShootArrow() {
        if (ownBow) {
            KeyMovement.is_Froze = true;
            KeyMovement.movement_speed = 0;
            // Shoot the collider
            rb.velocity = Vector3.zero;
            Vector3 arrowLoc = Vector3.zero;
            Vector3 rotation = new Vector3(0, 0, 0);

            arrow = Instantiate(arrow_prefab, this.GetComponent<Transform>());
            arrow.AddComponent<SpriteRenderer>();
            arrow.GetComponent<SpriteRenderer>().sortingOrder = 1;

            if(dirComponent.GetRot() == 'E') {
                arrowLoc = new Vector3(1f, 0, 0);
                rotation = new Vector3(0, 0, 90f);
                arrow.GetComponent<SpriteRenderer>().sprite = arrowSprites[0];
            } else if (dirComponent.GetRot() == 'W') {
                arrowLoc = new Vector3(-1f, 0, 0);
                rotation = new Vector3(0, 0, -90f);
                arrow.GetComponent<SpriteRenderer>().sprite = arrowSprites[0];
            } else if (dirComponent.GetRot() == 'N') {
                arrowLoc = new Vector3(0, 1f, 0);
                rotation = new Vector3(0, 0, 0);
                arrow.GetComponent<SpriteRenderer>().sprite = arrowSprites[1];
            } else if (dirComponent.GetRot() == 'S') {
                arrowLoc = new Vector3(0, -1f, 0);
                rotation = new Vector3(0, 0, 0);
                arrow.GetComponent<SpriteRenderer>().sprite = arrowSprites[0];
            }
            
            arrow.transform.localPosition = arrowLoc;
            arrow.transform.localEulerAngles = rotation;

            arrow.SetActive(true);

            StartCoroutine(DelayShoot(arrow, projectileSpeed));
        }
    }


    // Boomerang is just a projectile so it does everything that arrow does 
    // Boomerang goes 
    public void ShootBoomerang() {
        if (!boomerangFlying && ownBoomerang) {
            boomerangFlying = true;
            KeyMovement.is_Froze = true;
            KeyMovement.movement_speed = 0;
            // Shoot the collider
            rb.velocity = Vector3.zero;
            Vector3 boomerangLoc = Vector3.zero;
            Vector3 rotation = new Vector3(0, 0, 0);

            boomerang = Instantiate(boomerang_prefab, this.GetComponent<Transform>());
            boomerang.AddComponent<SpriteRenderer>();
            boomerang.GetComponent<SpriteRenderer>().sortingOrder = 1;
            boomerang_animator = boomerang.GetComponent<Animator>();

            if(dirComponent.GetDiagonal() == "None") {
                if(dirComponent.GetRot() == 'E') {
                    boomerangLoc = new Vector3(1f, 0, 0);
                    rotation = new Vector3(0, 0, 90f);
                    boomerang.GetComponent<SpriteRenderer>().sprite = boomerangSprites[0];
                    boomerang_animator.Play("Boomerang East");
                } else if (dirComponent.GetRot() == 'W') {
                    boomerangLoc = new Vector3(-1f, 0, 0);
                    rotation = new Vector3(0, 0, -90f);
                    boomerang.GetComponent<SpriteRenderer>().sprite = boomerangSprites[0];
                    boomerang_animator.Play("Boomerang West");
                } else if (dirComponent.GetRot() == 'N') {
                    boomerangLoc = new Vector3(0, 1f, 0);
                    rotation = new Vector3(0, 0, 0);
                    boomerang_animator.Play("Boomerang North");
                    boomerang.GetComponent<SpriteRenderer>().sprite = boomerangSprites[1];
                } else if (dirComponent.GetRot() == 'S') {
                    boomerangLoc = new Vector3(0, -1f, 0);
                    rotation = new Vector3(0, 0, 0);
                    boomerang.GetComponent<SpriteRenderer>().sprite = boomerangSprites[0];
                    boomerang_animator.Play("Boomerang South");
                }
            } else {
                if (dirComponent.GetDiagonal() == "NE") {
                    boomerangLoc = new Vector3(1, 1f, 0);
                    rotation = new Vector3(0, 0, 45);
                    boomerang_animator.SetTrigger("Boomerang North");
                    boomerang.GetComponent<SpriteRenderer>().sprite = boomerangSprites[1];
                } else if (dirComponent.GetDiagonal() == "NW") {
                    boomerangLoc = new Vector3(-1, 1f, 0);
                    rotation = new Vector3(0, 0, -45);
                    boomerang_animator.SetTrigger("Boomerang North");
                    boomerang.GetComponent<SpriteRenderer>().sprite = boomerangSprites[1];
                } else if (dirComponent.GetDiagonal() == "SW") {
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

    public void gotBoomerang() {
        ownBoomerang = true;
    }

    public void gotBow() {
        ownBow = true;
    }

    public void ShootBomb() {
        KeyMovement.is_Froze = true;
        KeyMovement.movement_speed = 0;
        // Shoot the collider
        rb.velocity = Vector3.zero;
        Vector3 bombLoc = Vector3.zero;

        bomb = Instantiate(bomb_prefab, this.GetComponent<Transform>());

        if(dirComponent.GetRot() == 'E') {
            bombLoc = new Vector3(1f, 0, 0);
        } else if (dirComponent.GetRot() == 'W') {
            bombLoc = new Vector3(-1f, 0, 0);
        } else if (dirComponent.GetRot() == 'N') {
            bombLoc = new Vector3(0, 1f, 0);
        } else if (dirComponent.GetRot() == 'S') {
            bombLoc = new Vector3(0, -1f, 0);
        }
        
        bomb.transform.localPosition = bombLoc;
        bomb.transform.SetParent(null);

        bomb.SetActive(true);

        KeyMovement.is_Froze = false;
        KeyMovement.movement_speed = 4.0f;
    }

    IEnumerator DelayLaser(float waitTime) {
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(DelayDestroy(sword));
        KeyMovement.is_Froze = false;
        KeyMovement.movement_speed = 4.0f;
    }

    IEnumerator DelayDestroy(GameObject weapon) {
        // Shoot the collider
        if(HPManager.GetCur_HP() == HPManager.GetMax_HP()) {
            AudioManager.Instance.SwordLaserAttack();
            Vector3 forceVector = Vector3.zero;
            if (dirComponent.GetRot() == 'E') {
                forceVector = new Vector3(laserDistance, 0 , 0);
            }   else if (dirComponent.GetRot() == 'W') {
                forceVector = new Vector3(-laserDistance, 0 , 0);
            } else if (dirComponent.GetRot() == 'N') {
                forceVector = new Vector3(0, laserDistance, 0);
            } else if (dirComponent.GetRot() == 'S') {
                forceVector = new Vector3(0, -laserDistance, 0);
            }
            if (weapon) {
                weapon.GetComponent<Rigidbody>().AddForce(forceVector);
            }
            yield return null;
        } else {
            AudioManager.Instance.SwordAttack();
            yield return new WaitForSeconds(0.1f);
            if (weapon) {
                Destroy(weapon);
            }
        }
    }

    IEnumerator DelayShoot(GameObject weapon, float shootForce) {
        AudioManager.Instance.BoomerangAttack();
        Vector3 forceVector = Vector3.zero;

        if (dirComponent.GetRot() == 'E') {
            forceVector = new Vector3(shootForce, 0 , 0);
        }   else if (dirComponent.GetRot() == 'W') {
            forceVector = new Vector3(-shootForce, 0 , 0);
        } else if (dirComponent.GetRot() == 'N') {
            forceVector = new Vector3(0, shootForce, 0);
        } else if (dirComponent.GetRot() == 'S') {
            forceVector = new Vector3(0, -shootForce, 0);
        }
        if (boomerang) {
            if (dirComponent.GetDiagonal() == "NE") {
                forceVector = new Vector3(shootForce, shootForce , 0);
            } else if (dirComponent.GetDiagonal() == "NW") {
                forceVector = new Vector3(-shootForce, shootForce , 0);
            } else if (dirComponent.GetDiagonal() == "SW") {
                forceVector = new Vector3(-shootForce, -shootForce , 0);
            } else if (dirComponent.GetDiagonal() == "SE") {
                forceVector = new Vector3(shootForce, -shootForce , 0);
            }
        }
        weapon.GetComponent<Rigidbody>().AddForce(forceVector);
        yield return new WaitForSeconds(0.3f);
        KeyMovement.is_Froze = false;
        KeyMovement.movement_speed = 4.0f;
    }
}
