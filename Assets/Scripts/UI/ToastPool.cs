using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastPool : MonoBehaviour 
{
	public Color textColor;
	public float accTime;
	public int defaultInit;
	public GameObject toastPrefab = null;
	public Transform toastParent = null;

	private float tick = 0;
	private bool isTick = false;
	private List<GameObject> toastPool = new List<GameObject>();
	private int totalValue = 0;

	void Awake()
	{
		for (int i = 0; i < defaultInit; ++i) 
		{
			var obj = Instantiate (toastPrefab);
			obj.SetActive (false);
			obj.transform.SetParent(toastParent, false);
			toastPool.Add (obj);
		}
	}

	void Update()
	{
		if (isTick) 
		{
			tick += Time.deltaTime;
			if (tick >= accTime) 
			{
				// display
				var obj = GetPooledObject();
				if (obj != null)
				{
					var compToast = obj.GetComponent<Toast> ();
					if (compToast != null)
					{
						compToast.label = totalValue.ToString ();
						compToast.color = textColor;
					}
					obj.SetActive (true);
				}

				isTick = false;
				tick = 0;
				totalValue = 0;
			}	
		}
	}

	public void AddValue(int value)
	{
		isTick = true;
		totalValue += value;
	}

	private GameObject GetPooledObject()
	{
		for (int i = 0; i < toastPool.Count; ++i) 
		{
			if (toastPool [i].activeSelf == false)
				return toastPool [i];
		}

		var obj = Instantiate (toastPrefab);
		obj.SetActive (false);
		obj.transform.SetParent(toastParent, false);
		toastPool.Add (obj);
		return obj;
	}
}
