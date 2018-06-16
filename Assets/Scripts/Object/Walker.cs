using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class Walker : Actor
{
	public const int ID_TANGSENG = 0;
    public const int ID_WUKONG = 1;
    public const int ID_BAJIE = 2;
    public const int ID_SHAHESHANG = 3;

    private WalkerData m_walkerData = null;
    private WalkerConfig m_walkerConfig = null;

	public void Init(WalkerData data)
	{
		m_walkerData = data;
		m_walkerConfig = GameManager.instance.walkerConfigs [m_walkerData.id];

		sprRender.sprite = m_walkerConfig.spr;

        InitWithLv(m_walkerData.lv);
	}

    // TODO:
    // reflection too slow, cache?
    public override T GetAttr<T>(string key) 
    { 
        // first find in 
        var t = m_walkerConfig.GetType();
        FieldInfo fi = t.GetField(key);
        if (fi != null)
        {
            var val = fi.GetValue(m_walkerConfig);
            if (val is T)
                return (T)val;
            else
                Debug.LogError("GetAttr - walkerConfig > " + key + " type is " + val.GetType().ToString());
        }

        t = m_walkerData.GetType();
        fi = t.GetField(key);
        if (fi != null)
        {
            var val = fi.GetValue(m_walkerData);
            if (val is T)
                return (T)val;
            else
                Debug.LogError("GetAttr - walkerData > " + key + " type is " + val.GetType().ToString());
        }

        Debug.LogError("Walker.GetAttr > cant find > " + key);
        return default(T);
    }

    /// <summary>
    /// Upgrade cost coin
    /// </summary>
    /// <param name="toLv">default is 0, if you dont set, will return next level cost</param>
    public int CostCoinForUpgrade(int toLv = 0)
    {
        if (toLv == 0)
            toLv = m_walkerData.lv + 1;
        return (int)(m_walkerConfig.upCoinBase + toLv * m_walkerConfig.upCoinFactor);
    }

    /// <summary>
    /// Upgrade cost soul
    /// </summary>
    /// <param name="toLv">default is 0, if you dont set, will return next level cost</param>
    public int CostSoulForUpgrade(int toLv = 0)
    {
        if (toLv == 0)
            toLv = m_walkerData.lv + 1;
        return (int)(m_walkerConfig.upSoulBase + toLv * m_walkerConfig.upSoulFactor);
    }

    public bool CanUpgrade()
    {
        return (CostCoinForUpgrade() >= DataManager.instance.coin &&
                CostSoulForUpgrade() >= DataManager.instance.soul);
    }

    public void Upgrade()
    {
        if (!CanUpgrade())
        {
            Debug.LogWarning("Walker.Upgrade > cost not enough");
            return;
        }

        m_walkerData.lv += 1;
        InitWithLv(m_walkerData.lv);
    }

    private void InitWithLv(int lv)
    {
        maxHP = m_walkerConfig.MaxHP(lv);
        hp = maxHP;
        atk = m_walkerConfig.MaxAtk (lv);
    }

    private void OnMouseDown()
    {
        if (UIManager.Instance.isMenuOver)
            return;

        MenuWalker menuWalker = MainScene.instance.SetMenuWalkerVisible(true);
        if (menuWalker != null)
        {
            menuWalker.InitWithWalker(this);
        }
    }

    private void SetClickEnable(bool isEnable)
    {
        var comp = GetComponent<BoxCollider2D>();
        if (comp != null)
            comp.enabled = isEnable;
    }
}
