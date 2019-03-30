using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int rupee_count = 0;
    public int key_count = 0;
    public int bomb_count = 0;

    public string weapon;

    void Update() {
        if (KeyMovement.god_Mode) {
            rupee_count = 999;
            key_count = 999;
            bomb_count = 999;
        }
    }

    public void AddRupee(int num_rupees)
    {
        rupee_count += num_rupees;
    }

    public void AddKey(int num_key) {
        key_count += num_key;
    }

    public void AddBomb(int num_bomb)
    {
        bomb_count += num_bomb;
    }

    public int GetRupees()
    {
        return rupee_count;
    }

    public int GetKeys()
    {
        return key_count;
    }

    public int GetBombs()
    {
        return bomb_count;
    }

    public void SwitchWeapon(string weaponName)
    {
        weapon = weaponName;
    }

    public string GetCurWeapon() {
        return weapon;
    }
}
