using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMain : MonoBehaviour 
{
	public Text coinText;
	public Text stepText;
	public Text lvText;

	void Start()
	{
		coinText.text = "Coin:" + DataManager.instance.coin.ToString ();
		stepText.text = "Step:" + DataManager.instance.step.ToString ();
		lvText.text = "Lv:" + DataManager.instance.lv.ToString ();
	}

	public void OnClickViewMatesButton()
	{
        UIManager.Instance.OpenMenu("MenuXian");
	}

	public void OnClickToBattleButton()
	{
        UIManager.Instance.OpenMenu("MenuBattlePrepare");
    }

	public void OnClickToJumpButton()
	{
		GameManager.sceneType = SceneType.Jump;
		SceneManager.LoadScene ("JumpScene");
	}
}
