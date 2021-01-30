using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Path
{
	public Transform One;
	public Transform Two;

	public Path(Transform one, Transform two)
	{
		One = one;
		Two = two;
	}


}
