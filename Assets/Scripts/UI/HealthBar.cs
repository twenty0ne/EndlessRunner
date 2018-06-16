using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour 
{
	public Slider healthBar;

	private Actor _actor;
	private int _lastHP = -1;

	void Start()
	{
		_actor = GetComponentInParent<Actor> ();
	}

	void Update()
	{
		if (_actor != null)
		{
			if (_lastHP != _actor.hp) 
			{
				_lastHP = _actor.hp;
				healthBar.value = _actor.hp * healthBar.maxValue / _actor.maxHP;
			}
		}
	}
}
