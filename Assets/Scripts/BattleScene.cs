using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : MonoBehaviour 
{
	public enum State
	{
		None,
		Start,
		Battle,
		End,
		Pause,
	}

	public Transform spawnPoint;

	// UI
	public GameObject menuInBattle;
	public GameObject menuBattleEnd;

	public State state { get; set;}

	private Team m_justiceTeam = new Team (Team.Type.Justice);
	private Team m_evilTeam = new Team (Team.Type.Evil);
    private int m_curLevelIndex = 0;

	private Team m_attackTeam = null;
	private Team m_hitTeam = null;

	private State m_lastState = State.None;

	void Awake()
	{
		menuBattleEnd.SetActive (false);
	}

	void Start()
	{
		m_curLevelIndex = DataManager.instance.level;

		// Init Walkers
		var walkerDatas = DataManager.instance.walkerDatas;
		for (int i = 0; i < walkerDatas.Count; ++i) 
		{
			var walkerObj = Instantiate (GameManager.instance.walkerPrefab);
			var walkerCtrl = walkerObj.GetComponent<Walker> ();
			walkerCtrl.Init (walkerDatas [i]);
			walkerObj.transform.position = GetSpwanPoint("Walker" + i.ToString ());

			m_justiceTeam.AddActor (walkerCtrl);
		}

		// Init Xian
		int xianIndex = 0;
		var xianDatas = DataManager.instance.xianDatas;
		for (int i = 0; i < xianDatas.Count; ++i) 
		{
			var xianData = xianDatas [i];
			if (xianData.isInvited) 
			{
				var objXian = Instantiate (GameManager.instance.xianPrefab);
				var xian = objXian.GetComponent<Xian> ();
				xian.Init (xianData);
				xian.transform.position = GetSpwanPoint ("Xian" + xianIndex.ToString ());
				xianIndex += 1;
			}
		}

		// Init Monsters
		var level = DataManager.instance.level;
		var levelConfig = GameManager.instance.levelConfigs [level];
		for (int i = 0; i < levelConfig.monsters.Length; ++i) 
		{
			var mcfg = levelConfig.monsters [i];

			var monsterObj = Instantiate (GameManager.instance.monsterPrefab);
			var monsterCtrl = monsterObj.GetComponent<Monster> ();
			monsterCtrl.Init (mcfg.id, mcfg.lv);
			monsterCtrl.transform.position = GetSpwanPoint ("Monster" + i.ToString ());

			m_evilTeam.AddActor(monsterCtrl);
		}

		m_justiceTeam.oppTeam = m_evilTeam;
		m_justiceTeam.onTeamStartBattleEnd = OnTeamStartBattleEnd;
		m_justiceTeam.onTeamAttackTurnEnd = OnTeamAttackTurnEnd;

		m_evilTeam.oppTeam = m_justiceTeam;
		m_evilTeam.onTeamStartBattleEnd = OnTeamStartBattleEnd;
		m_evilTeam.onTeamAttackTurnEnd = OnTeamAttackTurnEnd;

		state = State.Start;
	}

	Vector3 GetSpwanPoint(string name)
	{
		var tf = spawnPoint.Find (name);
		if (tf != null)
			return tf.position;

		return Vector3.zero;
	}

	void Update()
	{
		// state changed
		if (m_lastState != state) 
		{
			m_lastState = state;

            if (state == State.Start)
            {
                m_justiceTeam.StartBattle();
                m_evilTeam.StartBattle();
            }
            else if (state == State.Battle)
            {
                m_attackTeam.StartAttackTurn();
            }
            else if (state == State.End)
            {
                // get reward
                var levelConfig = GameManager.instance.levelConfigs [m_curLevelIndex];
                DataManager.instance.coin += levelConfig.coin;
                DataManager.instance.exp += levelConfig.exp;
                if (levelConfig.unlockXianId >= 0)
                    DataManager.instance.UnlockXian (levelConfig.unlockXianId);

                menuBattleEnd.SetActive (true);
                var menuCtrl = menuBattleEnd.GetComponent<MenuBattleEnd> ();
                if (menuCtrl != null)
                    menuCtrl.FinishLevel (m_curLevelIndex);
            }
		}
	}

	private int m_teamStartBattleEndCount = 0;
	private void OnTeamStartBattleEnd()
	{
		m_teamStartBattleEndCount += 1;

		if (m_teamStartBattleEndCount == 2)
		{
			state = State.Battle;

			// TODO:
			// calc first attack team
			m_attackTeam = m_justiceTeam;
			m_hitTeam = m_evilTeam;
		}
	}

	private void OnTeamAttackTurnEnd()
	{
        if (CheckBattleEnd())
        {
            state = State.End;
            return;
        }

		SwitchAttackTurn ();
	}

	private void SwitchAttackTurn()
	{
		var temp = m_attackTeam;
		m_attackTeam = m_hitTeam;
		m_hitTeam = temp;

		m_attackTeam.StartAttackTurn();
	}

    private bool CheckBattleEnd()
    {
        if (m_hitTeam != null && m_hitTeam.IsAllDead())
            return true;
        return false;
    }
}


