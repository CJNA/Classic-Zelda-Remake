using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Displayer : MonoBehaviour
{
    public Inventory inventory;

    public Text text_component;
    // Update is called once per frame
    void Update()
    {
        if (inventory != null && text_component != null)
        {
            text_component.text = "  :" + inventory.GetRupees().ToString() 
            + "\n  :" + inventory.GetKeys().ToString() + "\n  :" + inventory.GetBombs().ToString();
        }
    }
}
