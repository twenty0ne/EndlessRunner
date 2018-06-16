using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Actor Level 

// 暂时Actor升级之后的hp,atk,通过公式计算得出
// 全部采用配置，太多，不好管理

[Serializable]
public class WalkerConfig
{
	// TODO:
	// use [SerializeField] private/protecet
	public int id;

	public string name;
	public Sprite spr;
	public float scaleX;
	public float scaleY;

	// level 0 
	public int hpAtLv0;
	public int atkAtLv0;
	public int defAtLv0;
	public float atkSpeedAtLv0;

	public float hpFactor = 1f;
	public float atkFactor = 1f;
	public float defFactor = 1f;
	public float atkSpeedFactor = 1f;

	public int upCoinBase;
	public float upCoinFactor;
    public int upSoulBase;
    public float upSoulFactor;

	public int MaxHP(int lv) { return (int)(hpAtLv0 + lv * hpFactor); }
	public int MaxAtk(int lv) { return (int)(atkAtLv0 + lv * atkFactor); }
}

[CreateAssetMenu(fileName = "WalkerConfigs", menuName = "XiYou/WalkerConfigs", order=1)]
public class WalkerConfigs : ScriptableObject
{
	[SerializeField]
	private WalkerConfig[] walkerConfigs = new WalkerConfig[4];

	public WalkerConfig this [int index]
	{
		get {
			Debug.Assert (index >= 0 && index < Count, "CHECK");
			return walkerConfigs [index]; 
		}
	}

	public int Count
	{
		get { return walkerConfigs.Length; }
	}
}