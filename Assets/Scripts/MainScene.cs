using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour 
{
	public static MainScene instance { get; private set; }

    public static bool isLoadModel = false;

	public Transform spawnPoint;

	void Awake()
	{
		Debug.Assert (instance == null, "CHECK");
		instance = this;
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

        // TODO
        // Create Model
        if (MainScene.isLoadModel == false)
        {
            MainScene.isLoadModel = true;

            for (int i = 0; i < DataManager.instance.xianDatas.Count; i++)
            {
                XianData datXian = DataManager.instance.xianDatas[i];
                MXian.Create(datXian);
            }
        }
	}

	Vector3 GetSpwanPoint(string name)
	{
		var tf = spawnPoint.Find (name);
		if (tf != null)
			return tf.position;

		return Vector3.zero;
	}
}
