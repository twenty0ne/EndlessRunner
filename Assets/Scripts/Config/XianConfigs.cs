using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class XianConfig
{
	// TODO:
	// use [SerializeField] private/protecet

	public int id;
	public string name;
	public Sprite spr;
	public float sprScale;

	// level 0 
	public int hpAtLv0;
	public int atkAtLv0;
	// public int defAtLv0;
	// public float atkSpeedAtLv0;

	public float hpFactor = 1f;
	public float atkFactor = 1f;
	// public float defFactor = 1f;
	// public float atkSpeedFactor = 1f;

	public int coinKaiGuang;

	public int upCoinBase;
	public int upCoinFactor;

	public int MaxHP(int lv) { return (int)(hpAtLv0 + lv * hpFactor); }
	public int MaxAtk(int lv) { return (int)(atkAtLv0 + lv * atkFactor); }
    public int LvUpCoin(int lv) { return (int)(upCoinBase + lv * upCoinFactor); }
}

[CreateAssetMenu(fileName = "XianConfigs", menuName = "XiYou/XianConfigs", order = 1)]
public class XianConfigs : ScriptableObject
{
	[SerializeField]
	private List<XianConfig> xianConfigs;

	public XianConfig this [int index]
	{
		get { 
            Debug.Assert (index >= 0 && index < Count, "CHECK");
            return xianConfigs[index]; 
		}
	}

	public int Count
	{
		get { return xianConfigs.Count; }
	}

    public XianConfig GetById(int id)
    {
        return xianConfigs.Find((x) => x.id == id);
    }
}