using UnityEngine;
using System.Collections.Generic;
using System;

public class GSM : MonoBehaviour
{


  // Events to send
  public static event Action onStartTruck = delegate { };

  private float STARTING_ROW_TIMER = 8f;
  private int STATE = 0;
  public float newRowTimer;
  public float maxRowTimer;
  private bool hasTruckStarted;



  private void Awake()
  {
    newRowTimer = 5f;
    hasTruckStarted = false;

    //TODO: Call on StartGame Event
    newRowTimer = STARTING_ROW_TIMER;
    maxRowTimer = STARTING_ROW_TIMER;
  }

  private void Update()
  {
    newRowTimer -= Time.deltaTime;
    if (newRowTimer < 0)
    {
      Board.Instance.createNewRow();
      //
      hasTruckStarted = false;

      maxRowTimer = Mathf.Max(STARTING_ROW_TIMER - 0.5f, 3.5f);
      newRowTimer = maxRowTimer;
    }
    else if (!hasTruckStarted && newRowTimer < 2.6f)
    {
      onStartTruck();
      hasTruckStarted = true;
    }
  }

}
