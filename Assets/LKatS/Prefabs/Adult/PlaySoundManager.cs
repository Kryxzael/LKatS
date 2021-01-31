using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundManager : MonoBehaviour
{
    public PlayOnTrigger One, Two, Three, Four;

    public void PlaySound(int i )
	{
		switch (i)
		{
			case 1:
				One.PlaySound();
				break;
			case 2:
				Two.PlaySound();
				break;
			case 3:
				Three.PlaySound();
				break;
			case 4:
				Four.PlaySound();
				break;
			default:
				break;
		}
	}

}
