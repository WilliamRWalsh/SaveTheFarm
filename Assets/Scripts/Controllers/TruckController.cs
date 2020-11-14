using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
  private float speedX = 2f;
  private float waitTimer = .7f;

  public void Start()
  {
    waitTimer = .7f;
    transform.position = new Vector2(-4.87f, transform.position.y);
  }

  void Update()
  {

    if (transform.position.x <= 0)
    {
      transform.Translate(speedX * Time.deltaTime, 0, 0);
    }
    else if (waitTimer <= 0)
    {
      transform.Translate(speedX * Time.deltaTime, 0, 0);
    }
    else
    {
      waitTimer -= Time.deltaTime;
    }

    if (transform.position.x > 20) TruckPool.Instance.ReturnToPool(this);
  }
}
