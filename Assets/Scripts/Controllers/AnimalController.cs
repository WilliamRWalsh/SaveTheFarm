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
  private float startingScale = 0.1f;
  private float maxScale = 0.9f;
  [SerializeField]
  private int row, col;
  private float moveSpeed = 10f;
  [SerializeField]
  private bool hasTarget = false;
  private Vector3 target;
  private float stopDistance = 0.05f;

  private void OnEnable()
  {
    hasTarget = false;
    turnOffGlow();
    unfreeze();
    setAnimalType();
    gameObject.transform.localScale = new Vector3(startingScale, startingScale, 1);
  }

  private void FixedUpdate()
  {

    if (gameObject.transform.localScale.x < maxScale)
    {
      float delta = Time.deltaTime;
      gameObject.transform.localScale += new Vector3(1.4f * delta, 1.4f * delta, 0);
    }
    else if (gameObject.transform.localScale.x != maxScale)
    {
      gameObject.transform.localScale = new Vector3(maxScale, maxScale, 1);
    }

    if (hasTarget && Vector3.Distance(transform.position, target) < stopDistance)
    {
      setCell(row, col);
      hasTarget = false;
    }
    else if (hasTarget)
    {
      moveToTarget();
    }
  }

  public void setCell(int r, int c)
  {
    //TODO: Move SetActive elsewhere (probably object pool get)
    this.gameObject.name = r + ", " + c;

    Vector3 newPosition = new Vector3(-2.5f + c, -4 + r, 0);
    transform.position = newPosition;
    row = r;
    col = c;
  }

  private void moveToTarget()
  {
    transform.position = Vector3.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
  }

  public void setTarget(int r, int c)
  {
    hasTarget = true;
    target = new Vector3(-2.5f + c, -4 + r, 0);
    row = r;
    col = c;
  }

  public void dropBy(int num)
  {
    setTarget(row - num, col);
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
