using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
  static int rows = 7;
  static int cols = 6;

  private AnimalController[,] board = new AnimalController[rows, cols];
  public bool[,] boardState = new bool[rows, cols];

  private static Board _instance;
  public static Board Instance { get { return _instance; } }
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

    UserController.OnAnimalsToRemove += handleAnimalsToRemove;
  }

  private void Start()
  {
    for (int r = 0; r < rows; r++)
    {
      for (int c = 0; c < cols; c++)
      {
        boardState[r, c] = false;
      }
    }
    GenerateBoard(5);
  }

  private void GenerateBoard(int startingRows)
  {
    for (int r = 0; r < startingRows; r++)
    {
      for (int c = 0; c < cols; c++)
      {
        board[r, c] = NewAnimal(r, c);
        boardState[r, c] = true;
      }
    }
  }

  private AnimalController NewAnimal(int r, int c)
  {
    AnimalController animal = AnimalPool.Instance.Get();
    animal.setCell(r, c);
    animal.gameObject.SetActive(true);
    return animal;
  }

  private void handleAnimalsToRemove(AnimalController[] animals)
  {
    Debug.Log("handleAnimalsToRemove...");

    List<int> colsToDrop = new List<int>();
    int index;

    foreach (AnimalController animal in animals)
    {
      (int r, int c) = animal.getCell();
      boardState[r, c] = false;

      if (!colsToDrop.Contains(c))
      {
        colsToDrop.Add(c);
      }
    }

    index = 0;
    int dropBy = 0;
    foreach (int c in colsToDrop)
    {
      dropBy = 0;
      for (int r = 0; r < rows; r++)
      {
        if (boardState[r, c])
        {
          Debug.Log("dropBy called now.");
          board[r, c].dropBy(dropBy);
          board[r - dropBy, c] = board[r, c];

          boardState[r, c] = false;
          boardState[r - dropBy, c] = true;
        }
        else
        {
          dropBy++;
        }
      }
      index++;
    }
  }

  public void createNewRow()
  {

    for (int r = rows - 1; r >= 0; r--)
    {
      for (int c = 0; c < cols; c++)
      {
        if (boardState[r, c])
        {
          if (rows == r)
          {
            // Send GameOver Event
            return;
          }

          AnimalController animal = board[r, c];

          boardState[r, c] = false;
          boardState[r + 1, c] = true;
          board[r + 1, c] = board[r, c];

          animal.setCell(r + 1, c);
        }
      }
    }

    for (int c = 0; c < cols; c++)
    {
      board[0, c] = NewAnimal(0, c);
      boardState[0, c] = true;
    }
  }


}
