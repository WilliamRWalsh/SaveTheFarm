using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{
  [SerializeField]
  private T prefab;

  public static GenericObjectPool<T> Instance { get; private set; }
  private Queue<T> objects = new Queue<T>();

  private void Awake()
  {
    Instance = this;
  }

  public T Get()
  {
    if (objects.Count == 0)
      AddObject();

    var newObject = objects.Dequeue();
    if (newObject.gameObject.activeSelf != true) newObject.gameObject.SetActive(true);
    return newObject;
  }

  public void ReturnToPool(T objectToReturn)
  {
    if (objectToReturn.gameObject.activeSelf != false) objectToReturn.gameObject.SetActive(false);
    objects.Enqueue(objectToReturn);
  }

  private void AddObject()
  {
    var newObject = GameObject.Instantiate(prefab);
    if (newObject.gameObject.activeSelf != false) newObject.gameObject.SetActive(false);
    objects.Enqueue(newObject);
  }
}
