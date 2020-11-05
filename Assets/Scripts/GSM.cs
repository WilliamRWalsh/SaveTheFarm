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

  private static GSM _instance;
  public static GSM Instance { get { return _instance; } }
  private void Awake()
  {
    if (_instance != null && _instance != this)
    {
      Destroy(this.gameObject);
    }
    else
    {
      _instance = this;
    }
  }

  private void Start()
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
      maxRowTimer = Mathf.Max(maxRowTimer - 0.25f, 3.5f);
      newRowTimer = maxRowTimer;

      bool makeIce = false;
      if (maxRowTimer <= 7) makeIce = true;
      Board.Instance.createNewRow(makeIce);
      hasTruckStarted = false;

    }
    else if (!hasTruckStarted && newRowTimer < 2.6f)
    {
      onStartTruck();
      hasTruckStarted = true;
    }
  }

}
