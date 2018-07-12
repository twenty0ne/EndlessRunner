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
            wgt.onClick = OnClickWidgetActorPlaceHolder;
		}

		var walkerDatas = DataManager.instance.walkerDatas;
		for (int i = 0; i < walkerDatas.Count; ++i) 
		{
			var walkerData = walkerDatas [i];

			var obj = Instantiate (widgetActorIconPrefab);
			obj.transform.SetParent (selectorWidgetsParent, false);

			var cfg = GameManager.instance.walkerConfigs [walkerData.id];

			var ctrl = obj.GetComponent<WidgetActorIcon> ();
			// ctrl.Init(WidgetActorIcon.Type.Selector, cfg.spr);
            ctrl.actorImage.sprite = cfg.spr;
			ctrl.onClick = OnClickWidgetActor;
			ctrl.actorId = walkerData.id;
	
			widgetActorIconSelectors.Add (ctrl);
		}
			
		var xianDatas = DataManager.instance.xianDatas;
		for (int i = 0; i < xianDatas.Count; ++i) 
		{
			var xianData = xianDatas [i];

            if (xianData.isKaiGuang) 
			{
				var obj = Instantiate (widgetActorIconPrefab);
				obj.transform.SetParent (selectorWidgetsParent, false);

				var cfg = GameManager.instance.xianConfigs [xianData.id];

				var ctrl = obj.GetComponent<WidgetActorIcon> ();
				// ctrl.Init (WidgetActorIcon.Type.Selector, cfg.spr);
                ctrl.actorImage.sprite = cfg.spr;
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

            CacheManager.Instance.battleActorIds.Add (actorId);
		}

		GameManager.sceneType = SceneType.Battle;
		UnityEngine.SceneManagement.SceneManager.LoadScene ("BattleScene");
	}

	private void OnClickWidgetActor(WidgetActorIcon widget)
	{
		for (int i = 0; i < widgetActorIconSelecteds.Length; ++i) 
		{
			var selected = widgetActorIconSelecteds [i];
            if (selected.actorId != Actor.INVALID_ID)
				continue;

            selected.actorImage.sprite = widget.actorImage.sprite;
            selected.actorId = widget.actorId;

            widget.selectedMark.SetActive(true);
			break;
		}	

		this.UpdateBattleButtonStatus ();
	}

    private void OnClickWidgetActorPlaceHolder(WidgetActorIcon widget)
    {
        if (widget.actorId == Actor.INVALID_ID)
            return;

        for (int i = 0; i < widgetActorIconSelectors.Count; i++)
        {
            var selector = widgetActorIconSelectors[i];
            if (selector.actorId != widget.actorId)
                continue;

            selector.selectedMark.SetActive(false);
            break;
        }

        widget.actorImage.sprite = null;
        widget.actorId = Actor.INVALID_ID;

        this.UpdateBattleButtonStatus();
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
