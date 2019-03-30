using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_System : MonoBehaviour
{
    public GameObject sword_prefab;
    public Sprite[] swordSprites = new Sprite[4];

    private GameObject sword;
    private IEnumerator coroutine;
    private SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = this.GetComponent<SpriteRenderer>();
    }

    public void getSwing() {

    	// If i click many times, the bug arises and it's not deleting
    	if (sword) {
    		Destroy(sword);
    	}

        Vector3 swordLoc = Vector3.zero;
        Vector3 rotation = new Vector3(0, 0, 0);

        sword = Instantiate(sword_prefab, this.GetComponent<Transform>());
        sword.AddComponent<SpriteRenderer>();
        sword.GetComponent<SpriteRenderer>().sortingOrder = 1;

        if(spriteRend.sprite.name == "link_sprites_15") {
            swordLoc = new Vector3(0.5f, 0, 0);
            rotation = new Vector3(0, 0, 90f);
            sword.GetComponent<SpriteRenderer>().sprite = swordSprites[0];
        } else if (spriteRend.sprite.name == "link_sprites_13") {
            swordLoc = new Vector3(-0.5f, 0, 0);
            rotation = new Vector3(0, 0, -90f);
            sword.GetComponent<SpriteRenderer>().sprite = swordSprites[0];
        } else if (spriteRend.sprite.name == "link_sprites_2") {
            swordLoc = new Vector3(0, 0.5f, 0);
            rotation = new Vector3(0, 0, 0);
            sword.GetComponent<SpriteRenderer>().sprite = swordSprites[2];
        } else if (spriteRend.sprite.name == "link_sprites_0") {
            swordLoc = new Vector3(0, -0.5f, 0);
            rotation = new Vector3(0, 0, 0);
            sword.GetComponent<SpriteRenderer>().sprite = swordSprites[0];
        }
        
        sword.transform.localPosition = swordLoc;
        sword.transform.localEulerAngles = rotation;
        // sword.transform.parent = null;
        sword.SetActive(true);

        coroutine = delayDestroy();
        StartCoroutine(coroutine);

        // Sword shoots damaging beam granted that player is in full hp (if current_hp == max_hp)
        // 2. Raycasting for the shooting. and then the sword flies as well.
    }

    IEnumerator delayDestroy() {
        yield return new WaitForSeconds(0.5f);
        Destroy(sword);
    }
}

    //Zelda has to stop when the attack button is clicked.
    // How do we find which sprite we are looking at
        // 1. When animation runs, instantiate the collider object, about link's hand and then 


    // Implement weapon UI -> With the B / A
