using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public PlayerController player;

	private Vector3 _lastPlayerPostion;

	private void Start()
	{
		player = FindObjectOfType<PlayerController>();
		_lastPlayerPostion = player.transform.position;
	}

	private void Update()
	{
		float moveDistance = player.transform.position.x - _lastPlayerPostion.x;

		transform.position = new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z);

		_lastPlayerPostion = player.transform.position;
	}
}
