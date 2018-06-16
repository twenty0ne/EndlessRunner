using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team 
{
	public enum Type
	{
		None,
		Justice,
		Evil,
	}

	// front
	// 
	public System.Action onTeamStartBattleEnd;
	public System.Action onTeamFinishBattleEnd;
	public System.Action onTeamAttackTurnEnd;

	private Type m_type;
	private List<Actor> m_actorGroup = new List<Actor>();
	// private List<Actor> _frontActorGroup = new List<Actor> ();
	// private List<Actor> _backActorGroup = new List<Actor> ();

	private Team m_oppTeam;
	private int m_attackTurn = -1;

	public Type type { get { return m_type; } set { m_type = value; }}
	public Team oppTeam { get { return m_oppTeam; } set { m_oppTeam = value; }}
	public List<Actor> actorGroup { get { return m_actorGroup; } }

	public Team(Type type)
	{
		m_type = type;
	}

	public void AddActor(Actor at)
	{
		at.onStartBattleEnd = OnActorStartBattleEnd;
		at.onFinishBattleEnd = OnActorFinishBattleEnd;
		at.onAttackEnd = OnActorAttackEnd;
		m_actorGroup.Add (at);
	}

//	public bool IsBattleReady()
//	{
//		for (int i = 0; i < m_actorGroup.Count; ++i) 
//		{
//			if (!m_actorGroup [i].IsBattleReady ())
//				return false;
//		}
//		return true;
//	}

	public bool IsAllDead()
	{
		for (int i = 0; i < m_actorGroup.Count; ++i) 
		{
			if (m_actorGroup [i].IsDead () == false)
				return false;
		}

		return true;
	}

	// TODO
	// 需要扩展地方，根据当前攻击角色特点以及对方角色信息
	// 需要考虑队伍的薄弱环节
	public Actor GetAttackTarget()
	{
		var oppActorsCount = m_oppTeam.actorGroup.Count;
		int idx = Random.Range (0, oppActorsCount);

		for (int i = 0; i < oppActorsCount; ++i) 
		{
			var nidx = (idx + i) % oppActorsCount;
            var at = m_oppTeam.actorGroup[nidx];
			if (at.CanAttack ())
				return m_oppTeam.actorGroup [nidx];
		}

		return null;
	}

	public void StartBattle()
	{
		for (int i = 0; i < m_actorGroup.Count; ++i) 
		{
			var at = m_actorGroup[i];
			at.StartBattle();
		}
	}

	public void FinishBattle()
	{
		
	}

	public void StartAttackTurn()
	{
        Debug.Log("xx-- Team.StartAttackTurn");
		m_attackTurn = 0;
		SwitchAttackActor ();
	}

	private void FinishAttackTurn()
    {
		Debug.Assert (onTeamAttackTurnEnd != null, "CHECK");
		onTeamAttackTurnEnd ();
	}

	private int m_actorStartBattleEndCount = 0;
	private void OnActorStartBattleEnd()
	{
		m_actorStartBattleEndCount += 1;
		if (m_actorStartBattleEndCount == m_actorGroup.Count)
			onTeamStartBattleEnd();
	}

	private void OnActorFinishBattleEnd()
	{
	}

	private void OnActorAttackEnd()
	{
		// Debug.Log ("xx-- Team.OnActorAttackEnd");
		m_attackTurn += 1;
		if (m_attackTurn >= m_actorGroup.Count) 
		{
			FinishAttackTurn ();
		}
		else 
		{
			SwitchAttackActor ();
		}
	}

	private void SwitchAttackActor()
	{
        if (m_attackTurn >= m_actorGroup.Count)
        {
            FinishAttackTurn();
            return;
        }

		var at = m_actorGroup [m_attackTurn];
        if (at.CanAttack() == false)
        {
            m_attackTurn += 1;
            SwitchAttackActor();
            return;
        }

        // TODO
        // check attack target exist?
        if (at.attackTarget == null)
        {
            var target = GetAttackTarget();
            if (target == null)
            {
                FinishAttackTurn();
                return;
            }

            at.attackTarget = target;
        }

		at.Attack ();
	}
}
