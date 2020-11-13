using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
  [SerializeField]
  private Sprite buttonUp;
  [SerializeField]
  private Sprite buttonDown;

  void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
  {
    this.GetComponent<SpriteRenderer>().sprite = buttonDown;
    onDown();
  }

  void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
  {
    this.GetComponent<SpriteRenderer>().sprite = buttonUp;
    onUp();
  }

  protected virtual void onDown()
  {
    return;
  }

  protected virtual void onUp()
  {
  }
}
