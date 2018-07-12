using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WidgetXian : MonoBehaviour 
{
	private enum ButtonStatus
	{
		Lock,
		WaitKaiGuang,
		WaitGongFeng,
	}

	public Button handleButton;
	public Text handleButtonLabel;
	public Image headImage;
	public Text nameLabel;

    private int m_xianId;
    private ButtonStatus m_btnStatus;
    private XianConfig m_xianConfig;

    public void Init(int xianId)
	{
		m_xianId = xianId;

        var xianConfig = GameManager.instance.xianConfigs.GetById(xianId);
		m_xianConfig = xianConfig;

		headImage.sprite = xianConfig.spr;
		nameLabel.text = xianConfig.name;

		this.UpdateButtonStatus ();
	}

	public void OnClickHandleButton()
	{
		if (m_btnStatus == ButtonStatus.Lock) 
		{
		}
        else if (m_btnStatus == ButtonStatus.WaitKaiGuang) 
		{
            MXian mx = MXian.Get(m_xianId);
            Debug.Assert(mx != null, "CHECK");

            string msg;
            if (mx.TryKaiGuang(out msg))
            {    
                this.UpdateButtonStatus();
            }
            else
            {
                Debug.Log("kaiguang fail > " + msg);
            }
		} 
        else if (m_btnStatus == ButtonStatus.WaitGongFeng) 
		{
            MXian mx = MXian.Get(m_xianId);
            Debug.Assert(mx != null, "CHECK");

            string msg;
            if (mx.TryGongFeng(out msg))
            {
                this.UpdateButtonStatus();
            }
            else
            {
                Debug.Log("gongfeng fail > " + msg);
            }
		}
	}

	private void UpdateButtonStatus()
	{
		var xianData = DataManager.instance.GetXianData (m_xianId);

		string btnLabel = "";
		if (xianData == null)
		{
			m_btnStatus = ButtonStatus.Lock;
			btnLabel = "未解锁";
		}
		else
		{
			if (xianData.lv == -1) 
			{
                m_btnStatus = ButtonStatus.WaitKaiGuang;
				btnLabel = "开光";

                if (DataManager.instance.coin >= m_xianConfig.coinKaiGuang)
                    btnLabel += "coin:" + m_xianConfig.coinKaiGuang.ToString ();
				else
                    btnLabel += "coin:<color=#FF0000FF>" + m_xianConfig.coinKaiGuang.ToString() + "</color>";
			}
			else
			{
                m_btnStatus = ButtonStatus.WaitGongFeng;
				btnLabel = "供奉";
			}
		}

		handleButtonLabel.text = btnLabel;
	}
}
