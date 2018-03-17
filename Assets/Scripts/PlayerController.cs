using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float moveSpeed;
	public float jumpForce;
	public LayerMask whatIsGround;

	private Rigidbody2D myRigidBody;
	private BoxCollider2D myCollider;

	private bool grounded = false;
	private Animator myAnimator;

	private void Start()
	{
		myRigidBody = GetComponent<Rigidbody2D>();
		myCollider = GetComponent<BoxCollider2D>();
		myAnimator = GetComponent<Animator> ();
	}

	void Update()
	{
		grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

		myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (grounded)
			{
				myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
			}
		}

		myAnimator.SetFloat ("Speed", myRigidBody.velocity.x);
		myAnimator.SetBool ("Grounded", grounded);
	}
}
