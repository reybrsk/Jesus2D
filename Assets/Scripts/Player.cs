using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public bool IsFight { get; set; } = false;
  public int Coins { get; set; } = 0;

  public int cococo;
  
  private void Update()
  {
    cococo = Coins;
  }
}
  
 
