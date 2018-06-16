using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour 
{		
	public enum Type
	{
		Walker,		// 0 - 999
		Xian,		// 1000 - 2999
		Monster,	// 3000 - 
	}

    // TODO:
    public enum State
    {
        Idle,
        Attack,
        Dead,
    }

	public static int INVALID_ID = -1;

	public System.Action onStartBattleEnd;
	public System.Action onFinishBattleEnd;
	public System.Action onAttackEnd;

    public Toast damageToast;
    public SpriteRenderer sprRender;

    public State state { get { return m_state;} set { m_state = value; }}
	public Actor attackTarget { get; set; }

    public int maxHP { get; protected set; }
    public int hp { get; protected set; }
    public int atk { get; protected set; }
    public int def { get; protected set; }

    protected State m_state = State.Idle;
    protected Animator m_animator = null;

    protected readonly int m_hashAttackPara = Animator.StringToHash("Attack");
    protected readonly int m_hashDeadPara = Animator.StringToHash("Dead");

	public static Type GetType(int actorId)
	{
		if (actorId >= 0 && actorId < 1000)
			return Type.Walker;
		else if (actorId >= 1000 && actorId < 3000)
			return Type.Xian;
		else
			return Type.Monster;
	}

    public virtual void Awake(){}
    public virtual void Start()
    {
        m_animator = GetComponent<Animator>();
    }
        
    public int IntAttr(string key) { return GetAttr<int>(key); }
    public float FloatAttr(string key) { return GetAttr<float>(key); }
    public string StringAttr(string key) { return GetAttr<string>(key); }
    public virtual T GetAttr<T>(string key) { return default(T); }
		
	public virtual bool CanAttack() 
    { 
        return state != State.Dead;
    }

	public virtual bool IsDead() 
    { 
        return state == State.Dead; 
    }

	public virtual void StartBattle()
    {
        state = State.Attack;

        onStartBattleEnd();
    }

	public virtual void FinishBattle(){}

	public virtual void Attack () 
    {
        Debug.Assert (attackTarget != null, "CHECK");
        attackTarget.OnAttacked (atk);

        m_animator.SetBool(m_hashAttackPara, true);
    }

	public virtual void OnAttacked(int damage)
    {
        hp -= damage;
        if (hp <= 0) 
        {
            Debug.Log("xx-- Walker.OnAttacked is dead");
            hp = 0;
            state = State.Dead;

            m_animator.SetBool(m_hashDeadPara, true);
        }

        if (damageToast != null)
        {
            // damageToast.AddValue (damage);
        }
    }

    public virtual void SetAniSpeed(float speed)
    {
        m_animator.speed = speed;
    }

	public virtual void OnAniAttackEnd ()
    {
        m_animator.SetBool(m_hashAttackPara, false);

        if (onAttackEnd != null)
            onAttackEnd ();
    }

    public virtual void OnAniDeadEnd (){}
}
