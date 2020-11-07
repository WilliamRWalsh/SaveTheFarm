using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class ScoreController : MonoBehaviour
{
  private int score;

  private void Start()
  {
    score = 0;
    GSM.onCreateNewRow += IncreaseScore;
  }

  private void IncreaseScore()
  {
    score++;
    RenderScore(score);
  }

  private void RenderScore(int score)
  {
    List<int> digits = GetDigits(score);

    // foreach (digit in digits)
    // {

    // }
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
