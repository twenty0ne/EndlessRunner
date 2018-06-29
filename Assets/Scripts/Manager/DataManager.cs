using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 需要存储的数据
public class DataManager : MonoBehaviour 
{
	public static DataManager instance = null;

	public event Action<string> valueChanged;

	[NonSerialized]
	private PlayerData datPlayer;
	[NonSerialized]
	private IDataSaver datSaver;

	private bool _bSavePlayerData = false;

	public int step
	{
		get { return datPlayer.step; }
		set { 
			datPlayer.step = value;
			_bSavePlayerData = true;
		}
	}

	public int coin
	{
		get { return datPlayer.coin; }
		set { 
			datPlayer.coin = value; 
			if (valueChanged != null)
				valueChanged ("coin");
			_bSavePlayerData = true;
		}
	}

	public int lv
	{
		get { return datPlayer.lv; }
		set {
			datPlayer.lv = value;
			_bSavePlayerData = true;
		}
	}

	public int exp
	{
		get { return datPlayer.exp; }
		set { 
			datPlayer.exp = value; 
			_bSavePlayerData = true;
		}
	}

	public int level 
	{
		get { return datPlayer.level; }
		set {
			datPlayer.level = value;
			_bSavePlayerData = true;
		}
	}

    public int soul
    {
        get { return datPlayer.soul; }
        set {
            datPlayer.soul = value;
            _bSavePlayerData = true;
        }
    }

	public List<WalkerData> walkerDatas
	{
		get { return datPlayer.walkerDatas; }
	}

	public List<XianData> xianDatas
	{
		get { return datPlayer.xianDatas; }
	}
       
	public void UnlockXian(int xianId)
	{
#if DEBUG
		Debug.Assert(IsUnlockXian(xianId) == false, "CHECK > " + xianId.ToString());
#endif

		var xianData = new XianData();
		xianData.id = xianId;
		xianData.lv = -1;
		datPlayer.xianDatas.Add (xianData);

		_bSavePlayerData = true;
	}

	public bool IsUnlockXian(int xianId)
	{
		return GetXianData (xianId) != null;
	}

	public void InviteXian(int xianId)
	{
		var xianData = GetXianData (xianId);
		Debug.Assert (xianData != null, "CHECK > " + xianId.ToString ());
		xianData.lv = 0;

		_bSavePlayerData = true;
	}

	public XianData GetXianData(int xianId)
	{
		for (int i = 0; i < datPlayer.xianDatas.Count; ++i) 
		{
			if (datPlayer.xianDatas [i].id == xianId)
				return datPlayer.xianDatas [i];
		}

		return null;
	}

	void Awake()
	{
		if (instance != null) 
		{
			Destroy (gameObject);
			Debug.LogWarning ("GameMain is not null");
			return;
		}

		instance = this;
		DontDestroyOnLoad (gameObject);

		LoadData ();
	}

	void Update()
	{
		if (_bSavePlayerData) 
		{
			_bSavePlayerData = false;
			this.SavePlayerData ();
		}
	}

	private void LoadData()
	{
		datSaver = new JsonDataSaver();
		datPlayer = new PlayerData ();

		bool hadSaveFile = datSaver.Load (datPlayer);
		if (!hadSaveFile) 
		{
			datPlayer.step = 1000;
			datPlayer.coin = 1000;
			datPlayer.lv = 0;

			var datWalker = new WalkerData ();
			datWalker.id = 0;
			datWalker.lv = 0;
			datPlayer.walkerDatas.Add (datWalker);

			datWalker = new WalkerData ();
			datWalker.id = 1;
			datWalker.lv = 0;
			datPlayer.walkerDatas.Add (datWalker);

			datWalker = new WalkerData ();
			datWalker.id = 2;
			datWalker.lv = 0;
			datPlayer.walkerDatas.Add (datWalker);

			datWalker = new WalkerData ();
			datWalker.id = 3;
			datWalker.lv = 0;
			datPlayer.walkerDatas.Add (datWalker);

			this.SavePlayerData ();
		}
	}

	public void SavePlayerData()
	{
		datSaver.Save (datPlayer);
		_bSavePlayerData = false;
	}
}
