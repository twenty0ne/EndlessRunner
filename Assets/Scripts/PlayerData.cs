using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class WalkerData
{
	public int id;	// same with config id;
	public int lv = 0;	// start from 0
	public int hp = 0;
}

[Serializable]
public class XianData
{
	public int id;
	public int lv = -1;
    public int totalGongFengCoin;
    public int curLvGongFengCoin; // 当前等级已经供奉的

	public bool isKaiGuang { get { return lv >= 0; }}
}

[Serializable]
public class PlayerData
{
	// NOTE:@TWENTY0NE
	// 因为使用序列化json的方式保存，所以不能使用 

	public int step;
	public int coin;
	public int lv;	// player level
    public int exp;
	public int level; // stage level
    public int soul;  // 用于升级 walker

	public List<WalkerData> walkerDatas = new List<WalkerData>();
	// public List<int> unlockXianIds = new List<int>();
	public List<XianData> xianDatas = new List<XianData>();

//	public void SetCoin(int val)
//	{
//		coin = val;
//		if (valueChanged != null)
//			valueChanged ("coin");
//	}
}
