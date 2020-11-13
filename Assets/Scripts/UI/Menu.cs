using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
  private bool isStarting = false;
  private bool isRestarting = false;
  private float moveSpeed = 1.5f;
  private Vector3 offScreenTarget = new Vector3(0, 13, 0);
  private Vector3 middleTarget = new Vector3(0, -0.78f, 0);
  private Vector3 bottomTarget = new Vector3(0, -10, 0);

  void Start()
  {
    StartBtn.OnStartBtnPressed += HandleOnStartBtnPressed;
    GSM.OnGameOver += HandleGameOver;
  }
  void Update()
  {
    if (isStarting) MoveOffScreen();
    if (isRestarting) MoveMiddle();
  }
  void MoveOffScreen()
  {
    transform.position = Vector3.Lerp(transform.position, offScreenTarget, moveSpeed * Time.deltaTime);
    if (Mathf.Abs(transform.position.y - offScreenTarget.y) < .5)
    {
      transform.position = bottomTarget;
      isStarting = false;
    }
  }
  void MoveMiddle()
  {
    transform.position = Vector3.Lerp(transform.position, middleTarget, moveSpeed * Time.deltaTime);
    if (Mathf.Abs(transform.position.y - middleTarget.y) < 0.03)
    {
      transform.position = middleTarget;
      isRestarting = false;
    }
  }
  void HandleOnStartBtnPressed()
  {
    moveSpeed = 1.5f;
    isStarting = true;
    isRestarting = false;
  }
  void HandleGameOver()
  {
    moveSpeed = 3f;
    isStarting = false;
    isRestarting = true;
    this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
  }
}
