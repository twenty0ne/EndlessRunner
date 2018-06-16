using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelMonsterConfig
{
	public int id;	// monster id
	public int lv;
}

[Serializable]
public class LevelConfig
{
	public int id;
	public string name;
	public LevelMonsterConfig[] monsters;
	public int coin;
	public int exp;
	public int unlockXianId = -1;
}

[CreateAssetMenu(fileName = "LevelConfigs", menuName = "XiYou/LevelConfigs", order=1)]
public class LevelConfigs : ScriptableObject
{
	[SerializeField]
	private List<LevelConfig> levelConfigs;

	public LevelConfig this [int index]
	{
		get { 
			Debug.Assert (index >= 0 && index < Count, "CHECK");
			return levelConfigs [index]; 
		}
	}

	public int Count
	{
		get { return levelConfigs.Count; }
	}
}