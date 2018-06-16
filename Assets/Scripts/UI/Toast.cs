using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour 
{
	public Text text;

	public string label { set { text.text = value; } }
	public Color color { set { text.color = value; }}

//	public void OnAnimationEnd()
//	{
//		gameObject.SetActive (false);
//	}
}
