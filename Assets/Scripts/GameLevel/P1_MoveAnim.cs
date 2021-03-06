﻿using UnityEngine;

public class P1_MoveAnim : MonoBehaviour {

	Animator animator;

	public float speed;

	private Rigidbody2D rb; 

	public Direction move;

	public int totHP;
	public int curHP;
	public bool dead = false;

	public Inventory inventory;
	public Item weapon;
	public float startAttack;

	public enum Direction
	{
		Still, Down, Up, Left, Right, UpRight, UpLeft, DownRight, DownLeft, Dead
	}

	/*int stillHash = Animator.StringToHash("Still");
	int upHash = Animator.StringToHash("Up");
	int downHash = Animator.StringToHash("Down");
	int leftHash = Animator.StringToHash("Left");
	int rightHash = Animator.StringToHash("Right");
	int upRightHash = Animator.StringToHash("UpRight");
	int downRightHash = Animator.StringToHash("DownRight");
	int upLeftHash = Animator.StringToHash("UpLeft");
	int downLeftHash = Animator.StringToHash("DownLeft");*/

	void Start() {
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator>();
		totHP = 20;
		curHP = totHP;
		weapon = new Item ("Sword", 3, 2, false);
	}

	void Update() {

		if (startAttack + 0.5 < Time.time) {
			startAttack = Time.time;
			GetComponent<SpriteRenderer> ().color = UnityEngine.Color.white;
		}

		float h;
		float v;

		if (!dead) {

			// GET ATTACK INPUT
			if (Input.GetMouseButtonDown(0)) {
				Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				pz.z = 0;
				if (weapon.longRange) {
					// for now do nothing
				}
			}

			// GET MOVEMENT INPUT
			h = Input.GetAxisRaw ("Horizontal");

			v = Input.GetAxisRaw ("Vertical");

			// MOVEMENT
			Vector2 movement = new Vector2 (h, v).normalized;
			rb.velocity = movement * speed;

			// RIGHT
			if (h > 0.1) {

				if (v > 0.1) {
					move = Direction.UpRight;
				} else if (v < -0.1) {
					move = Direction.DownRight;
				} else {
					move = Direction.Right;
				}
				// LEFT
			} else if (h < -0.1) {

				if (v > 0.1) {
					move = Direction.UpLeft;
				} else if (v < -0.1) {
					move = Direction.DownLeft;
				} else {
					move = Direction.Left;
				}
				// STILL
			} else {

				if (v > 0.1) {
					move = Direction.Up;
				} else if (v < -0.1) {
					move = Direction.Down;
				} else {
					move = Direction.Still;
				}

			}
		} else {
			move = Direction.Dead;
		}

		determineAnimation ();

	}

	void determineAnimation() {

		switch (move) {

		case Direction.Still:
			animator.Play ("P1_Still");
			break;
		case Direction.Down:
			animator.Play ("P1_Down");
			break;
		case Direction.Up:
			animator.Play ("P1_Up");
			break;
		case Direction.Left:
			animator.Play ("P1_Left");
			break;
		case Direction.Right:
			animator.Play ("P1_Right");
			break;
		case Direction.UpRight:
			animator.Play ("P1_UpRight");
			break;
		case Direction.UpLeft:
			animator.Play ("P1_UpLeft");
			break;
		case Direction.DownRight:
			animator.Play ("P1_DownRight");
			break;
		case Direction.DownLeft:
			animator.Play ("P1_DownLeft");
			break;
		case Direction.Dead:
			animator.Play ("P1_Dead");
			break;
		}

	}

	public void Damaged() {
		GetComponent<SpriteRenderer> ().color = UnityEngine.Color.red;
		startAttack = Time.time;
	}
		
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			Enemy enemy = coll.gameObject.GetComponent<Enemy> ();
			if (Input.GetMouseButtonDown(0)) {
				RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

				if(hit.collider != null) {
					Debug.Log ("Target Position: " + hit.collider.gameObject.transform.position);
				}
			}
		}
	}

}