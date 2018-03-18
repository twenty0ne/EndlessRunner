using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour 
{
	public GameObject[] thePlatforms;
	public Transform generationPoint;

	public float distanceBetweenMin;
	public float distanceBetweenMax;

	public ObjectPooler theObjectPooler;

	private float[] platformWidths;

	void Start()
	{
		platformWidths = new float[thePlatforms.Length];
		for (int i = 0; i < thePlatforms.Length; ++i) 
		{
			platformWidths [i] = thePlatforms [i].GetComponent<BoxCollider2D> ().size.x;
		}

		// platformWidth = thePlatform.GetComponent<BoxCollider2D> ().size.x;
	}

	void Update()
	{
		if (transform.position.x < generationPoint.position.x) 
		{
			float distanceBetween = Random.Range (distanceBetweenMin, distanceBetweenMax);
			int platformSelector = Random.Range (0, thePlatforms.Length);

			transform.position = new Vector3 (transform.position.x + distanceBetween, 
				transform.position.y, transform.position.z);

			Instantiate (thePlatforms[platformSelector], transform.position, transform.rotation);

			transform.position = new Vector3 (transform.position.x + platformWidths [platformSelector], 
				transform.position.y, transform.position.z);

			/*
			var obj = theObjectPooler.GetPooledObject();
			obj.transform.position = transform.position;
			// obj.transform.rotation = transform.rotation;
			obj.SetActive (true);*/
		}
	}
}
