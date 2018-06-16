using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour 
{
	bool bJumpToNextScene = false;

	void Start()
	{
		bJumpToNextScene = true;
	}

	private void Update()
	{
		if (bJumpToNextScene) 
		{
			bJumpToNextScene = false;

			GameManager.sceneType = SceneType.Main;
			SceneManager.LoadScene ("MainScene");
		}
	}
}
