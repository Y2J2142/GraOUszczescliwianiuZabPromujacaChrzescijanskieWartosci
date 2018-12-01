using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerModifier {

	public float modifier;

	public float timer;

	public bool permanent;
	

	public FlowerModifier(float modifier, float timer, bool permanent)
	{
		this.modifier = modifier;
		this.timer = timer;
		this.permanent = permanent;
	}
}
