using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class AnimalController : MonoBehaviour, IPointerClickHandler
{
  // Events to send
  public static event Action<AnimalController> OnAnimalSelected = delegate { };

  [SerializeField]
  private Sprite[] animalSprites;
  private int type;
  private bool isIceBlock;

  private int row, col;

  private void OnEnable()
  {
    turnOffGlow();
    setAnimalType();
    // TODO: remove this so that it changes with game clock
    int r = (int)UnityEngine.Random.Range(0, 5);
    if (r == 0) freeze();
  }

  public void setCell(int r, int c)
  {
    Vector3 newPosition = new Vector3(-2.5f + c, -4 + r, 0);
    transform.position = newPosition;
    row = r;
    col = c;
  }

  public void dropBy(int num)
  {
    print("dropping by " + num);
    // TODO: transform instead of set
    setCell(row - num, col);
  }

  public (int, int) getCell()
  {
    return (row, col);
  }

  private void setAnimalType()
  {
    type = (int)UnityEngine.Random.Range(0, animalSprites.Length);
    gameObject.GetComponent<SpriteRenderer>().sprite = animalSprites[type];
  }

  public int getAnimalType()
  {
    return type;
  }

  public bool getIsIceBlock()
  {
    return isIceBlock;
  }

  public void unfreeze()
  {
    isIceBlock = false;
    this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
  }

  public void freeze()
  {
    isIceBlock = true;
    this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
  }

  public void turnOnGlow()
  {
    this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
  }

  public void turnOffGlow()
  {
    this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
  }

  void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
  {
    OnAnimalSelected(this);
  }
}
