using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuWalker : MonoBehaviour 
{
    public Text txtName;
    public Text txtAtk;
    public Text txtDef;
    public Text txtCoin;
    public Text txtSoul;
    public Button btnUpgrade;

    private Walker m_walker;

    void OnDisable()
    {
        m_walker = null;    
    }

    public void InitWithWalker(Walker wk)
    {
        DataManager.instance.soul = 1000;
        DataManager.instance.SavePlayerData();

        m_walker = wk;

        txtName.text = "Name: " + wk.StringAttr("name");
        txtAtk.text = "Atk: " + wk.atk.ToString();
        txtDef.text = "Def: " + wk.def.ToString();
        txtCoin.text = "Coin: " + wk.CostCoinForUpgrade().ToString();
        txtSoul.text = "Soul: " + wk.CostSoulForUpgrade().ToString();

        btnUpgrade.interactable = m_walker.CanUpgrade();
    }

    public void OnClickButtonUpgrade()
    {
        Debug.Log("MenuWalker.OnClickButtonUpgrade");
        m_walker.Upgrade();
    }

    public void OnClickButtonExit()
    {
        Debug.Log("MenuWalker.OnClickButtonExit");
        gameObject.SetActive(false);
    }
}
