using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Direction : MonoBehaviour
{
	public char currentRot;
    public string diagonal;
    public Sprite[] directionalImage = new Sprite[8];
    private Rigidbody rb;
    // Start is called before the first frame update

    void Start()
    {
        currentRot = 'N';
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float rowAxis = Input.GetAxisRaw("Horizontal");
        float verAxis = Input.GetAxisRaw("Vertical");

        if ((double) rowAxis > 0.0) {
            if ((double) verAxis > 0.0)
              this.diagonal = "NE";
            else if ((double) verAxis < 0.0)
              this.diagonal = "SE";
            else
              this.diagonal = "None";
          } else if ((double) rowAxis < 0.0) {
            if ((double) verAxis > 0.0)
                this.diagonal = "NW";
            else if ((double) verAxis < 0.0)
                this.diagonal = "SW";
            else
                this.diagonal = "None";
            } else {
            this.diagonal = "None";
        }
        if ((double) rb.velocity.y > 0.0)
            currentRot = 'N';
        else if ((double) rb.velocity.y < 0.0)
            currentRot = 'S';
        else if ((double) rb.velocity.x > 0.0)
            currentRot = 'E';
        else if ((double) rb.velocity.x < 0.0)
            currentRot = 'W';

        if (this.diagonal == "None") {
            if (currentRot == 'N') {
                this.transform.GetChild(0).Find("DirectionRenderer").gameObject.GetComponent<Image>().sprite = directionalImage[0];
                } else if (currentRot == 'S') {
                    this.transform.GetChild(0).Find("DirectionRenderer").gameObject.GetComponent<Image>().sprite = directionalImage[1];
                } else if (currentRot == 'E') {
                    this.transform.GetChild(0).Find("DirectionRenderer").gameObject.GetComponent<Image>().sprite = directionalImage[2];
                } else if (currentRot == 'W') {
                    this.transform.GetChild(0).Find("DirectionRenderer").gameObject.GetComponent<Image>().sprite = directionalImage[3];
                }
        }  else {
            if (diagonal == "NE") {
                this.transform.GetChild(0).Find("DirectionRenderer").gameObject.GetComponent<Image>().sprite = directionalImage[4];
            } else if (diagonal == "NW") {
                this.transform.GetChild(0).Find("DirectionRenderer").gameObject.GetComponent<Image>().sprite = directionalImage[5];
            } else if (diagonal == "SE") {
                this.transform.GetChild(0).Find("DirectionRenderer").gameObject.GetComponent<Image>().sprite = directionalImage[6];
            } else if (diagonal == "SW") {
                this.transform.GetChild(0).Find("DirectionRenderer").gameObject.GetComponent<Image>().sprite = directionalImage[7];
            }
        }
    }

    public char GetRot() {
        
    	return currentRot;
    }

    public string GetDiagonal() {
        return diagonal;
    }

    public void SetNeutral() {
        currentRot = 'X';
    }

    public char ManualUpdate() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            currentRot = 'N';
        } else if (Input.GetKeyDown(KeyCode.DownArrow))
            currentRot = 'S';
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            currentRot = 'E';
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            currentRot = 'W';
        return currentRot;
    }
}
