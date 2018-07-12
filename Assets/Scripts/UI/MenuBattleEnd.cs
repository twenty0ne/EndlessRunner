using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBattleEnd : MonoBehaviour 
{
	public Text labelCoin;
	public Text labelExp;
	public Text labelUnlockXian;

	public void OnClickButtonBack()
	{
		GameManager.sceneType = SceneType.Main;
		SceneManager.LoadScene ("MainScene");
	}

	public void FinishLevel(int levelIndex)
	{
		var levelConfig = GameManager.instance.levelConfigs [levelIndex];

		labelCoin.text = "Coin: " + levelConfig.coin.ToString ();
		labelExp.text = "Exp: " + levelConfig.exp.ToString ();

		if (levelConfig.unlockXianId >= 0) 
		{
			labelUnlockXian.gameObject.SetActive (true);

            var xianConfig = GameManager.instance.xianConfigs.GetById(levelConfig.unlockXianId); 
			labelUnlockXian.text = "Unlock Xian: " + xianConfig.name;
		} 
		else 
		{
			labelUnlockXian.gameObject.SetActive (false);	
		}
	}
}
