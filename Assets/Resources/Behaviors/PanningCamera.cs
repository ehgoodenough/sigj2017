﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanningCamera : MonoBehaviour {

	Vector3 velocity = Vector3.zero;
	private float smoothTime = 0.33f;

	void Awake() {
		Camera cam = GetComponent<Camera>();
		cam.farClipPlane = 2000;
	}

	void Update() {
		this.transform.position = Vector3.SmoothDamp(this.transform.position, Hero.instance.targetPos, ref this.velocity, smoothTime);
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -200);
		this.velocity.Set(this.velocity.x, this.velocity.y, 0);
	}
}
