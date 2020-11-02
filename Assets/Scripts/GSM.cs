using UnityEngine;
using System.Collections.Generic;
using System;

public class GSM : MonoBehaviour
{
  // Events to send
  public static event Action onStartTruck = delegate { };

  private int STATE = 0;
  public float newRowTimer = 3f;
  private bool hasTruckStarted;

  private void Awake()
  {
    newRowTimer = 3f;
    hasTruckStarted = false;
  }

  private void Update()
  {
    newRowTimer -= Time.deltaTime;
    if (newRowTimer < 0)
    {
      Board.Instance.createNewRow();
      newRowTimer = 3;
      hasTruckStarted = false;
    }
    else if (!hasTruckStarted && newRowTimer < 2.6f)
    {
      onStartTruck();
      hasTruckStarted = true;
    }
  }

}
