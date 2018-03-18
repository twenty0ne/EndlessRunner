using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public GameObject pooledObject;
	public int pooledAmount;

	private List<GameObject> pooledObjects;

	void Start()
	{
		pooledObjects = new List<GameObject>();

		for (int i = 0; i < pooledAmount; ++i) 
		{
			NewPooledObject ();
		}
	}

	public GameObject GetPooledObject()
	{
		for (int i = 0; i < pooledObjects.Count; ++i) 
		{
			var obj = pooledObjects [i];
			if (obj.activeSelf == false) 
			{
				return obj;
			}
		}

		var nobj = NewPooledObject ();
		return nobj;
	}

	private GameObject NewPooledObject()
	{
		var obj = Instantiate (pooledObject);
		obj.SetActive (false);
		pooledObjects.Add (obj);
		return obj;
	}
}
