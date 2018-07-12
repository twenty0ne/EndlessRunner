using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuXian : MonoBehaviour 
{
	public GameObject widgetXianPrefab;
	public Transform widgetXianParent;

	// private List<WidgetXian> _widgets = new List<WidgetXian>();

	private void Awake()
	{
		gameObject.SetActive (false);
	}

	private void Start()
	{
		var xianConfigs = GameManager.instance.xianConfigs;
		for (int i = 0; i < xianConfigs.Count; ++i) 
		{
			var obj = Instantiate (widgetXianPrefab);
			obj.transform.SetParent (widgetXianParent, false);

			var wid = obj.GetComponent<WidgetXian> ();
            wid.Init (xianConfigs [i].id);
		}
	}

	public void OnClickBackButton()
	{
		gameObject.SetActive (false);
	}
}
