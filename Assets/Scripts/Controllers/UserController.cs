using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UserController : MonoBehaviour
{
  // Events to send
  public static event Action<AnimalController[]> OnAnimalsToRemove = delegate { };

  private AnimalController[] selectedAnimals = new AnimalController[3];
  private int selectedIndex = 0;

  private void Awake()
  {
    AnimalController.OnAnimalSelected += handleNewSelection;
  }

  private void handleNewSelection(AnimalController animal)
  {
    if (animal.getIsIceBlock()) return;

    if (selectedIndex == 0)
    {
      animal.turnOnGlow();
      selectedAnimals[selectedIndex] = animal;
      selectedIndex++;
      return;
    }

    if (selectedAnimals[selectedIndex - 1].getAnimalType() == animal.getAnimalType()
        && !isAnimalAlreadySelected(animal))
    {
      animal.turnOnGlow();
      selectedAnimals[selectedIndex] = animal;
      selectedIndex++;
    }
    else
    {
      foreach (AnimalController sa in selectedAnimals)
      {
        if (sa)
        {
          sa.turnOffGlow();
        }
      }
      selectedAnimals = new AnimalController[3];
      selectedIndex = 0;
    }

    if (selectedIndex == 3)
    {
      // dispatch event to remove the animals
      OnAnimalsToRemove(selectedAnimals);
      selectedAnimals = new AnimalController[3];
      selectedIndex = 0;
    }
  }

  private bool isAnimalAlreadySelected(AnimalController animal)
  {
    foreach (AnimalController sa in selectedAnimals)
    {
      if (sa) if (sa == animal) return true;
    }
    return false;
  }

}
