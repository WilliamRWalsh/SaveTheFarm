using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class ScoreController : MonoBehaviour
{
  private int score;
  [SerializeField]
  private Sprite[] digitSprites;

  private void Start()
  {
    score = 0;
    UserController.OnAnimalsToRemove += IncreaseScore;
    StartBtn.OnStartBtnPressed += ClearScore;
  }

  private void IncreaseScore(AnimalController[] animals)
  {
    score++;
    RenderScore(score);
  }

  private void ClearScore()
  {
    score = 0;
    RenderScore(score);
  }

  private void RenderScore(int score)
  {
    List<int> digits = GetDigits(score);

    if (score < 10)
    {
      this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = digitSprites[digits[0]];
    }
    else if (score < 100)
    {
      this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = digitSprites[digits[1]];
      this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = digitSprites[digits[0]];
      this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

    }
    else
    {
      this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = digitSprites[digits[2]];
      this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = digitSprites[digits[1]];
      this.gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = digitSprites[digits[0]];
      this.gameObject.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
    }
  }

  public static List<int> GetDigits(int number)
  {
    List<int> digits = new List<int>();
    while (number >= 10)
    {
      digits.Add(number % 10);
      number = (number - (number % 10)) / 10;
    }
    digits.Add(number);

    return digits;
  }
}
