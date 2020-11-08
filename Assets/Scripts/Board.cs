using UnityEngine;
using System.Collections.Generic;

public class Board : MonoBehaviour
{

  static int rows = 7;
  static int cols = 6;

  private AnimalController[,] board = new AnimalController[rows, cols];
  public bool[,] boardState = new bool[rows, cols];

  static int numPatterns = 3;


  bool[,] iceBlockPatterns = new bool[,] {
    { false, false, false, false, false, false },
    { false, false, true, true, false, false },
    { false, false, true, true, true, false },
    { false, false, true, true, false, false },
    { false, false, true, true, false, false },
    { false, false, true, true, false, false },
    };

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
  }

  private void Start()
  {
    UserController.OnAnimalsToRemove += handleAnimalsToRemove;

    for (int r = 0; r < rows; r++)
    {
      for (int c = 0; c < cols; c++)
      {
        boardState[r, c] = false;
      }
    }
    GenerateBoard(4);

  }

  public void createNewRow(int numIceBlocks)
  {
    for (int r = rows - 1; r >= 0; r--)
    {
      for (int c = 0; c < cols; c++)
      {
        if (boardState[r, c])
        {
          if (rows == r + 1)
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

    int startIceIndex = (int)UnityEngine.Random.Range(0, 7 - numIceBlocks);
    for (int c = 0; c < cols; c++)
    {
      AnimalController animal = NewAnimal(0, c);
      board[0, c] = animal;
      boardState[0, c] = true;

      if (c >= startIceIndex && numIceBlocks > 0)
      {
        numIceBlocks -= 1;
        animal.freeze();
      }
    }

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
    unfreezeBlocks(animals);

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

  private void unfreezeBlocks(AnimalController[] animals)
  {
    foreach (AnimalController animal in animals)
    {
      (int r, int c) = animal.getCell();

      // Check up
      if (r + 1 < rows)
      {
        if (boardState[r + 1, c] && board[r + 1, c].getIsIceBlock())
        {
          board[r + 1, c].unfreeze();
        }
      }

      // Check down
      if (r - 1 >= 0)
      {
        if (boardState[r - 1, c] && board[r - 1, c].getIsIceBlock())
        {
          board[r - 1, c].unfreeze();
        }
      }

      // Check left
      if (c + 1 < cols)
      {
        if (boardState[r, c + 1] && board[r, c + 1].getIsIceBlock())
        {
          board[r, c + 1].unfreeze();
        }
      }

      // Check right
      if (c - 1 >= 0)
      {
        if (boardState[r, c - 1] && board[r, c - 1].getIsIceBlock())
        {
          board[r, c - 1].unfreeze();
        }
      }
    }
  }
}
