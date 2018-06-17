using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoot : MonoBehaviour 
{
    public List<GameObject> uiObjs = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            uiObjs.Add(transform.GetChild(i).gameObject);
        }
    }

    public GameObject GetUIPanelByName(string name)
    {
        for (int i = 0; i < uiObjs.Count; ++i)
        {
            var obj = uiObjs[i];
            if (obj.name == name)
                return obj;
        }
        Debug.LogWarning("UIRoot.GetUIPanelByName > cant find " + name);
        return null;
    }
}
