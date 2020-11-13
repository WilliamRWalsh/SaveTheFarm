using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
  private bool isMoving = false;
  private float moveSpeed = 1.5f;
  private Vector3 target = new Vector3(0, 13, 0);

  void Start()
  {
    StartBtn.OnStartBtnPressed += HandleOnStartBtnPressed;
  }
  void Update()
  {
    if (isMoving) Move();
  }
  void Move()
  {
    transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
  }
  void HandleOnStartBtnPressed()
  {
    isMoving = true;
  }
}
