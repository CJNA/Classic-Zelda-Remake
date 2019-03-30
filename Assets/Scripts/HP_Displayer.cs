using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_Displayer : MonoBehaviour
{
   	public Killable HP_Manager;

    public Text text_component;
    // Update is called once per frame
    void Update()
    {
        if (text_component != null)
        {
            text_component.text = "-Life-" + "Current Hearts: " + HP_Manager.GetCur_HP().ToString() + "\n Max Hearts: " + HP_Manager.GetMax_HP().ToString();
        }
        // Legacy Code
        // I couldn't find half hearth So I am gonna leave it like dis
        // int heart = 0;

        // // First generate Filled Heart
        // for (int i = 0; i < HP_Manager.GetCur_HP(); i++, heart++) {
        //     if (hearts.Count < HP_Manager.GetCur_HP()) {
        //         hearts.Add(Instantiate(heartSprite[0]));
        //         hearts[hearts.Count - 1].transform.SetParent(canvas.transform, false);
        //         hearts[hearts.Count - 1].GetComponent<RectTransform>().anchoredPosition = startHeart + new Vector2(heartDistance * i, 0);
        //     } else {
        //         hearts[i] = heartSprite[0];
        //     }
        // }
        // // Second generate UnFilled Heart
        // for (int i = heart; i < HP_Manager.GetMax_HP(); i++) {
        //     if (hearts.Count < HP_Manager.GetMax_HP()) {
        //         hearts.Add(Instantiate(heartSprite[1]));
        //         hearts[hearts.Count - 1].transform.SetParent(canvas.transform, false);
        //         hearts[hearts.Count - 1].GetComponent<RectTransform>().anchoredPosition = startHeart + new Vector2(heartDistance * i, 0);
        //     } else {
        //         hearts[i] = heartSprite[1];
        //     }
        // }
    }
}
