using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartBtn : Button
{
  public static event Action OnStartBtnPressed = delegate { };

  protected override void onUp()
  {
    OnStartBtnPressed();
  }
}
