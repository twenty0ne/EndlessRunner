using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterConfig
{
	public int id;
	public string name;
	public Sprite spr;
	// public float scale;

	public int hpAtLv0;
	public int atkAtLv0;
	// public int defAtLv0;
	// public float atkSpeedAtLv0;

	public float hpFactor;
	public float atkFactor;
	// public float defFactor;
	// public float atkSpeedFactor;

	public int MaxHP(int lv) { return (int)(hpAtLv0 + lv * hpFactor); }
	public int MaxAtk(int lv) { return (int)(atkAtLv0 + lv * atkFactor); }
	// public int MaxDef(int lv) { return (int)(defAtLv0 + lv * defFactor); }
	// public float MaxAtkSpeed(int lv) { return atkSpeedAtLv0 + lv * atkSpeedFactor; }
}

[CreateAssetMenu(fileName = "MonsterConfigs", menuName = "XiYou/MonsterConfigs", order=1)]
public class MonsterConfigs : ScriptableObject
{
	[SerializeField]
	private List<MonsterConfig> monsterConfigs;

	public MonsterConfig this [int index]
	{
		get { 
			Debug.Assert (index >= 0 && index < Count, "CHECK");
            return monsterConfigs[index];
		}
	}

	public int Count
	{
		get { return monsterConfigs.Count; }
	}

    public MonsterConfig GetById(int id)
    {
        return monsterConfigs.Find((x)=>x.id == id);
    }
}