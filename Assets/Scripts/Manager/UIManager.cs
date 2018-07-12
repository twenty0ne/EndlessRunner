using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO:
// 界面出现的方式可以参考 ojbect-c storyboard
// push, model, popover, replace, custom
public class UIManager : MonoSingleton<UIManager> 
{
    private UIRoot m_uiRoot = null;
    private GameObject m_curMenu = null;

    public GameObject OpenMenu(string menuName)
    {
        if (m_curMenu != null && m_curMenu.name == menuName)
        {
            m_curMenu.SetActive(true);
            return m_curMenu;
        }

        VerifyUIRoot();
        Debug.Assert(m_uiRoot != null, "CHECK");

        // find in scene
        var uipanel = m_uiRoot.GetUIPanelByName(menuName);
        if (uipanel != null)
        {
            m_curMenu = uipanel;

            uipanel.SetActive(true);
            return uipanel;
        }
        return null;
    }

    private void VerifyUIRoot()
    {
        if (m_uiRoot == null)
        {
            var uiRoot = GameObject.FindObjectOfType<UIRoot>();
            m_uiRoot = uiRoot;
        }
    }

    void Update()
    {
        if (m_curMenu != null && m_curMenu.activeSelf == false)
            m_curMenu = null;
    }

    public bool isMenuOver 
    {
        get { return m_curMenu != null && m_curMenu.activeSelf; }
    }
}
