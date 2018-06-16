using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xian : Actor 
{
	private XianData _datXian;
	private XianConfig _cfgXian;

	public void Init(XianData datXian)
	{
		_datXian = datXian;
		_cfgXian = GameManager.instance.xianConfigs [_datXian.id];

		maxHP = _cfgXian.MaxHP (_datXian.lv);
		hp = maxHP;
        atk = _cfgXian.MaxAtk (_datXian.lv);
		// atkSpeed = _cfgXian.atkSpeedAtLv0 + _datXian.lv * _cfgXian.atkSpeedFactor;

		var sprRender = GetComponent<SpriteRenderer> ();
		sprRender.sprite = _cfgXian.spr;
	}
}
