using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xian : Actor 
{
    private XianData m_datXian;
    private XianConfig m_cfgXian;

	public void Init(XianData datXian)
	{
		m_datXian = datXian;
		m_cfgXian = GameManager.instance.xianConfigs [m_datXian.id];

		maxHP = m_cfgXian.MaxHP (m_datXian.lv);
		hp = maxHP;
        atk = m_cfgXian.MaxAtk (m_datXian.lv);
		// atkSpeed = _cfgXian.atkSpeedAtLv0 + _datXian.lv * _cfgXian.atkSpeedFactor;

		var sprRender = GetComponent<SpriteRenderer> ();
		sprRender.sprite = m_cfgXian.spr;
	}
}
