using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Actor
{
    public void Init(int monsterId, int lv)
	{
        var monsterConfig = GameManager.instance.monsterConfigs.GetById(monsterId);

		maxHP = monsterConfig.MaxHP(lv);
		hp = maxHP;
	    atk = monsterConfig.MaxAtk(lv);
	}
}
