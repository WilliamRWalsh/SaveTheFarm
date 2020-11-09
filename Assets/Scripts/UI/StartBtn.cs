using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartBtn : MonoBehaviour
{
	public static event Action OnStart = delegate { };

	public void ButtonedPressed()
	{
		OnStart();
	}
}
