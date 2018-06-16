using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour 
{
	void Awake()
	{
		for (int i = 0; i < transform.childCount; ++i) 
		{
			Transform tf = transform.GetChild (i);
			if (tf != null) 
			{
				var comp = tf.gameObject.GetComponent<Renderer> ();
				if (comp != null) 
				{
					comp.enabled = false;
				}
			}
		}
	}
}
