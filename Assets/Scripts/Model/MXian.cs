using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MXian : Model
{
    public static List<MXian> mxianList = new List<MXian>();

    public static bool IsValidId(int xianId)
    {
        // return (xianId >= 1000 && xianId < 3000);
        return xianId >= 0;
    }

    public static MXian Create(XianData datXian)
    {
        MXian mx = Get(datXian.id);
        if (mx != null)
            return mx;

        mx = new MXian(datXian.id);
        mxianList.Add(mx);
        return mx;
    }
        
    public static MXian Unlock(int xianId)
    {
        // Debug.Assert(IsUnlockXian(xianId) == false, "CHECK > " + xianId.ToString());

        XianData datXian = new XianData();
        datXian.id = xianId;
        datXian.lv = -1;
        DataManager.instance.xianDatas.Add (datXian);
        DataManager.instance.MarkSaveData();

        return MXian.Create(datXian);
    }     

    public static bool IsUnlock(int xianId)
    {
        return DataManager.instance.GetXianData (xianId) != null;
    }

    public static MXian Get(int xianId)
    {
        return mxianList.Find((x) => x.id == xianId);
    }

    //////////////////////////////////////////////////////////
    private int id { get { return m_cfgXian.id; } }
    private XianData m_datXian;
    private XianConfig m_cfgXian;

    private MXian(int xianId)
    {
        m_cfgXian = GameManager.instance.xianConfigs[xianId];
        m_datXian = DataManager.instance.GetXianData(xianId);
    }

    public bool TryKaiGuang(out string msg)
    {
        if (DataManager.instance.coin >= m_cfgXian.coinKaiGuang)
        {
            m_datXian.lv = 0;
            DataManager.instance.coin -= m_cfgXian.coinKaiGuang;

            DataManager.instance.MarkSaveData();
            msg = null;
            return true;
        }

        msg = string.Format("lack coin: {0}", m_cfgXian.coinKaiGuang - DataManager.instance.coin);
        return false;
    }

    public bool TryGongFeng(out string msg)
    {
        if (DataManager.instance.coin <= 0)
        {
            msg = string.Format("lack coin");
            return false;
        }

        DataManager.instance.coin -= 1;
        m_datXian.totalGongFengCoin += 1;
        m_datXian.curLvGongFengCoin += 1;
        if (m_datXian.curLvGongFengCoin >= m_cfgXian.LvUpCoin(m_datXian.lv + 1))
        {
            m_datXian.lv += 1;
            m_datXian.curLvGongFengCoin = 0;
            Debug.Log(string.Format("xian {0} level up", m_datXian.id));
        }
        
        DataManager.instance.MarkSaveData();

        msg = null;
        return true;
    }
}
