using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float moveSpeed;
	public float jumpForce;
	public LayerMask whatIsGround;

	private Rigidbody2D _rigidbody;
	private BoxCollider2D _collider;

	[SerializeField] private bool _isGround = false;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_collider = GetComponent<BoxCollider2D>();
	}

	void Update()
	{
		_isGround = Physics2D.IsTouchingLayers(_collider, whatIsGround);

		_rigidbody.velocity = new Vector2(moveSpeed, _rigidbody.velocity.y);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (_isGround)
			{
				_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
			}
		}
	}
}
