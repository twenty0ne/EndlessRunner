using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour 
{
	public static MainScene instance { get; private set; }

	public Transform spawnPoint;

	// UI
	public GameObject objMenuXian;
	public GameObject objMenuBattlePrepare;
    public GameObject objMenuWalker;

	void Awake()
	{
		Debug.Assert (instance == null, "CHECK");
		instance = this;

		objMenuXian.SetActive (false);
		objMenuBattlePrepare.SetActive (false);
        objMenuWalker.SetActive(false);
	}

	void OnDestroy()
	{
		instance = null;
	}

	void Start()
	{
		// Init Walkers
		var walkerDatas = DataManager.instance.walkerDatas;
		for (int i = 0; i < walkerDatas.Count; ++i) 
		{
			var walkerObj = Instantiate (GameManager.instance.walkerPrefab);
			var walkerCtrl = walkerObj.GetComponent<Walker> ();
			walkerCtrl.Init (walkerDatas [i]);
			walkerCtrl.transform.position = GetSpwanPoint("Walker" + i.ToString ());
		}

		// Init Xians
		// var xianDatas = 
	}

	Vector3 GetSpwanPoint(string name)
	{
		var tf = spawnPoint.Find (name);
		if (tf != null)
			return tf.position;

		return Vector3.zero;
	}
		
    public MenuWalker SetMenuWalkerVisible(bool isVisible)
    {
        if (objMenuWalker.activeSelf == isVisible)
            return null;

        objMenuWalker.SetActive(isVisible);
        return objMenuWalker.GetComponent<MenuWalker>();
    }
}
