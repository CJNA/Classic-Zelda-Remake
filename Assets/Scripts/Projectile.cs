// Decompiled with JetBrains decompiler
// Type: Projectile
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D498C5C2-3FB9-4AE3-B8B6-DAF35472A93A
// Assembly location: C:\Users\ghi03\OneDrive\Documents\Academic_Year_2019_Winter\EECS 494\p1_milestone_example_1\p1_milestone_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Projectile : MonoBehaviour
{
  private Rigidbody rb;

  private void Start()
  {
    this.rb = this.GetComponent<Rigidbody>();
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (!collision.gameObject.CompareTag("Enemy"))
      return;
    Destroy((Object) this.gameObject);
  }
}
