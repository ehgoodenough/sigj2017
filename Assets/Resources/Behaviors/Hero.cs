﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : Walker {
	//private Vector3 _targetPosition = new Vector3();
	//public Vector2 targetPosition { get { return _targetPosition; } }
	//private float speed = 10;

	private float deathspin = 22;

	public int maxhealth = 3;
	public int health = 3;
	public int gold = 0;

	public bool isDead = false;
	public bool isDone = false;

	public static Hero instance;

	private const float Z_INDEX = 0.5f;

	protected override void Awake() {
		base.Awake();
		instance = this;
		_zIndex = Z_INDEX;
		//_attackSprite = getSpriteResource("Images/SkeletonAttack");
	}

	public float playCoinsEffect() {
		string path = "Prefabs/FX/Coins FX";
		GameObject prefab = Resources.Load(path) as GameObject;
		ParticleSystem fx = Instantiate(prefab).GetComponent<ParticleSystem>();
		fx.transform.position = targetPos;
		Destroy(fx.gameObject, fx.main.duration);
		return fx.main.duration;
	}

	void Update() {
		if(this.isDead) {
			this.transform.Rotate(Vector3.forward * this.deathspin);
			return;
		}

		if(this.isDone) {
			return;
		}

		// Poll the keyboard.
		if (_isStepping == false) {
			Vector3 stepDir = new Vector3();
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
				stepDir += Vector3.up;
			}
			if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
				stepDir += Vector3.down;
			}
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
				stepDir += Vector3.left;
			}
			if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
				stepDir += Vector3.right;
			}

			if (canMoveTo(targetPos + stepDir)) {
				targetPos += stepDir;
			}
		}

		if (Map.instance != null) {
			if(Map.instance.HasGold(targetPos)) {
				this.gold += Map.instance.GetGolds(targetPos).Count;
				Map.instance.RemoveGold(targetPos, 0.3f);
				playCoinsEffect();
			}
		}

	}

	private bool canMoveTo(Vector3 position) {
		if(Map.instance == null) {
			// Debug.Log ("Map.instance == null");
			return true;
		}

		Enemy enemy = Map.instance.getEnemy(position);
		if (enemy != null && enemy.isAlive) {
			attack(enemy.targetPos);
			enemy.takeDamage(1);
			return false;
		}

		// The "end position" is now the scammer's position.
		if(position.x == Map.instance.endPosition.x
		&& position.y == Map.instance.endPosition.y) {
			halfStep(position);
			Debug.Log("Talking to the scammer!");
			int REQUIRED_GOLD = 10; // GET THIS FROM THE GAME MANAGER
			if(this.gold >= REQUIRED_GOLD) {
				this.gold -= REQUIRED_GOLD;
				this.isDone = true;
			} else {
				// PULL UP THE DIALOGUE BOX FROM THE SCAMMER IN THE GAME MANAGER
			}
		}

		return Map.instance.canMoveTo(position);
	}

	public void takeDamage(int damage) {
		if (health <= 0)
			return;

		health -= damage;
		playHitEffect("Blood Splash", false);

		if (health <= 0) {
			health = 0;
			die();
		}
	}

	private void die() {
		if(this.isDead != true) {
			this.isDead = true;

			// TODO: play death sound.
		}
	}
}
