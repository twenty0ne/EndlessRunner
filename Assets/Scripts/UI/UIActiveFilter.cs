using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE:
// 确定在 Awake 的时候，哪些可以显示
// 只是子节点，还是子子节点都可以？
public class UIActiveFilter : MonoBehaviour 
{
    public List<GameObject> hideObjects = new List<GameObject>();
    public List<GameObject> showObjects = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < hideObjects.Count; ++i)
        {
            if (hideObjects[i] != null)
                hideObjects[i].SetActive(false);
        }

        for (int i = 0; i < showObjects.Count; ++i)
        {
            if (showObjects[i] != null)
                showObjects[i].SetActive(true);
        }
    }
}
