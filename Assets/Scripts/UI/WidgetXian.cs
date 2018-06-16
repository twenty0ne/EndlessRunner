using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WidgetXian : MonoBehaviour 
{
	private enum ButtonStatus
	{
		Lock,
		Unlock,
		Normal,
	}

	public Button handleButton;
	public Text handleButtonLabel;
	public Image headImage;
	public Text nameLabel;

	private int _xianId;
	private ButtonStatus _btnStatus;
	private XianConfig _xianConfig;

	public void Init(int xianId)
	{
		_xianId = xianId;
		var xianConfig = GameManager.instance.xianConfigs [xianId];
		_xianConfig = xianConfig;

		headImage.sprite = xianConfig.spr;
		nameLabel.text = xianConfig.name;

		this.UpdateButtonStatus ();
	}

	public void OnClickHandleButton()
	{
		if (_btnStatus == ButtonStatus.Lock) 
		{
		}
		else if (_btnStatus == ButtonStatus.Unlock) 
		{
			if (DataManager.instance.coin >= _xianConfig.inviteCoin) 
			{
				DataManager.instance.InviteXian (_xianId);

				this.UpdateButtonStatus ();
			}
		} 
		else if (_btnStatus == ButtonStatus.Normal) 
		{
			
		}
	}

	private void UpdateButtonStatus()
	{
		var xianData = DataManager.instance.GetXianData (_xianId);

		string btnLabel = "";
		if (xianData == null) 
		{
			_btnStatus = ButtonStatus.Lock;
			btnLabel = "未解锁";
		}
		else
		{
			if (xianData.lv == -1) 
			{
				_btnStatus = ButtonStatus.Unlock;
				btnLabel = "请神";

				if (DataManager.instance.coin >= _xianConfig.inviteCoin)
					btnLabel += ">" + _xianConfig.inviteCoin.ToString ();
				else
					btnLabel += "><color=#FF0000FF>" + _xianConfig.inviteCoin.ToString() + "</color>";
			}
			else 
			{
				_btnStatus = ButtonStatus.Normal;
				btnLabel = "供奉";
			}
		}

		handleButtonLabel.text = btnLabel;
	}
}
