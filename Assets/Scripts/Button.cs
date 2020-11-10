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
    Debug.Log("Down");
    this.GetComponent<SpriteRenderer>().sprite = buttonDown;
  }

  void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
  {
    Debug.Log("Up");
    this.GetComponent<SpriteRenderer>().sprite = buttonUp;
  }

}
