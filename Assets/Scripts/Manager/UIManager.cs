using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager> 
{
    public bool isMenuOver = false;
    // public GameObject[] objMenus

    public void OpenMenu(string menuName)
    {
        // find in scene
        // Resources.FindObjectsOfTypeAll
        isMenuOver = true;
    }

    public void OpenDialog()
    {
        
    }
}
