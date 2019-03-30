using UnityEngine;

public class Keese : MonoBehaviour
{
  private int count = 0;
  public float speed = 3f;
  private Rigidbody rb;

  private void Start()
  {
    this.rb = this.GetComponent<Rigidbody>();
    this.count = 30;
  }

  private void Update()
  {
    if (this.count++ == 30)
    {
      Vector3 vector3 = Vector3.zero;
      int num = Random.Range(1, 32);
      if (num % 8 == 0)
        vector3.x = this.speed;
      else if (num % 8 == 1)
        vector3.x = -this.speed;
      else if (num % 8 == 2)
        vector3.y = this.speed;
      else if (num % 8 == 3)
        vector3.y = -this.speed;
      else if (num % 8 == 4)
        vector3 = new Vector3(this.speed, this.speed, 0.0f);
      else if (num % 8 == 5)
        vector3 = new Vector3(this.speed, -this.speed, 0.0f);
      else if (num % 8 == 6)
        vector3 = new Vector3(-this.speed, this.speed, 0.0f);
      else if (num % 8 == 7)
        vector3 = new Vector3(-this.speed, -this.speed, 0.0f);
      this.rb.velocity = vector3;
    }
    else
    {
      if (this.count < 80)
        return;
      this.rb.velocity = Vector3.zero;
      this.count = 0;
    }
  }

  private void OnCollisionEnter(Collision coll)
  {
    this.count = 80;
  }
}
