using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBattlePrepare : MonoBehaviour 
{
	public Transform selectorWidgetsParent;
	public Transform selectedWidgetsParent;
	public GameObject widgetActorIconPrefab;
	public Button battleButton;

	private WidgetActorIcon[] widgetActorIconSelecteds;
	private List<WidgetActorIcon> widgetActorIconSelectors = new List<WidgetActorIcon>();

	void Awake()
	{
		if (DataManager.instance == null)
			return;

		widgetActorIconSelecteds = selectedWidgetsParent.GetComponentsInChildren<WidgetActorIcon> ();
		for (int i = 0; i < widgetActorIconSelecteds.Length; ++i) 
		{
			var wgt = widgetActorIconSelecteds [i];
			wgt.onClick = OnClickWidgetActor;
		}

		var walkerDatas = DataManager.instance.walkerDatas;
		for (int i = 0; i < walkerDatas.Count; ++i) 
		{
			var walkerData = walkerDatas [i];

			var obj = Instantiate (widgetActorIconPrefab);
			obj.transform.SetParent (selectorWidgetsParent, false);

			var cfg = GameManager.instance.walkerConfigs [walkerData.id];

			var ctrl = obj.GetComponent<WidgetActorIcon> ();
			ctrl.Init(WidgetActorIcon.Type.Selector, cfg.spr);
			ctrl.onClick = OnClickWidgetActor;
			ctrl.actorId = walkerData.id;
	
			widgetActorIconSelectors.Add (ctrl);
		}
			
		var xianDatas = DataManager.instance.xianDatas;
		for (int i = 0; i < xianDatas.Count; ++i) 
		{
			var xianData = xianDatas [i];

			if (xianData.isInvited) 
			{
				var obj = Instantiate (widgetActorIconPrefab);
				obj.transform.SetParent (selectorWidgetsParent, false);

				var cfg = GameManager.instance.xianConfigs [xianData.id];

				var ctrl = obj.GetComponent<WidgetActorIcon> ();
				ctrl.Init (WidgetActorIcon.Type.Selector, cfg.spr);
				ctrl.onClick = OnClickWidgetActor;
				ctrl.actorId = xianData.id;

				widgetActorIconSelectors.Add (ctrl);
			}
		}

		//
		this.UpdateBattleButtonStatus();
	}

	public void OnClickBackButton()
	{
		gameObject.SetActive (false);
	}

	public void OnClickBattleButton()
	{
		// cache battle ids
		for (int i = 0; i < widgetActorIconSelecteds.Length; ++i)
		{
			var actorId = widgetActorIconSelecteds [i].actorId;
			if (actorId == Actor.INVALID_ID)
				continue;

            DataCacheManager.Instance.battleActorIds.Add (actorId);
		}

		GameManager.sceneType = SceneType.Battle;
		UnityEngine.SceneManagement.SceneManager.LoadScene ("BattleScene");
	}

	private void OnClickWidgetActor(WidgetActorIcon widget)
	{
		if (widget.type == WidgetActorIcon.Type.Selector) 
		{
			for (int i = 0; i < widgetActorIconSelecteds.Length; ++i) 
			{
				var selectedWidget = widgetActorIconSelecteds [i];
				if (selectedWidget.actorId != Actor.INVALID_ID)
					continue;

				selectedWidget.SetActorSprite (widget.actorImage.sprite);
				selectedWidget.actorId = widget.actorId;
				break;
			}	
		}
		else if (widget.type == WidgetActorIcon.Type.Selected) 
		{
			for (int i = 0; i < widgetActorIconSelectors.Count; ++i) 
			{
				var widgetSelector = widgetActorIconSelectors [i];
				if (widgetSelector.actorId == widget.actorId) 
				{
					widgetSelector.UnSelected ();
					break;
				}
			}
		}

		this.UpdateBattleButtonStatus ();
	}

	private void UpdateBattleButtonStatus()
	{
		// NOTE:
		// must include 4 walkers
		int countWalker = 0;
		for (int i = 0; i < widgetActorIconSelecteds.Length; ++i) 
		{
			var wgt = widgetActorIconSelecteds [i];
			if (wgt.actorId == Actor.INVALID_ID)
				continue;
			if (wgt.actorId == Walker.ID_TANGSENG ||
			    wgt.actorId == Walker.ID_WUKONG ||
			    wgt.actorId == Walker.ID_BAJIE ||
			    wgt.actorId == Walker.ID_SHAHESHANG)
				countWalker += 1;
		}

		if (countWalker >= 4)
			battleButton.interactable = true;
		else
			battleButton.interactable = false;
	}
}
