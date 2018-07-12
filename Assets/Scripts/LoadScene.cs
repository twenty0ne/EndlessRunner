using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour 
{
	bool bJumpToNextScene = false;
    // TODO:
    // targetScene, loadType
    // 数据的加载和初始化最好也放在这里

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
