using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WidgetActorIcon: MonoBehaviour 
{
//	public enum Type
//	{
//		Selector,
//		Selected,
//		Display,
//	}

    public enum Status
    {
        
    }

	public Type type;
	[HideInInspector]public int actorId = Actor.INVALID_ID;

	public Image actorImage;
	public GameObject selectedMark;

	public Action<WidgetActorIcon> onClick;

	void Start()
	{
		selectedMark.SetActive (false);
	}

//	public void Init(Type tp, Sprite spr)
//	{
//		type = tp;
//		actorImage.sprite = spr;
//	}

	public void OnClick()
	{
		if (actorId == Actor.INVALID_ID)
			return;

        if (selectedMark.activeSelf)
            return;

//		if (type == Type.Selector) 
//		{
//          selectedMark.SetActive (!selectedMark.activeSelf);
//		}
//		else if (type == Type.Selected) 
//		{
//			actorImage.sprite = null;
//			actorId = Actor.INVALID_ID;
//		}
			
		if (onClick != null) 
		{
			onClick (this);
		}
	}

//	public void SetActorSprite(Sprite spr)
//	{
//		actorImage.sprite = spr;
//	}

//	public void Selected()
//	{
//		if (selectedMark != null)
//			selectedMark.SetActive (true);
//	}
//
//	public void UnSelected()
//	{
//		if (selectedMark != null)
//			selectedMark.SetActive (false);
//		actorId = Actor.INVALID_ID;
//	}
}
