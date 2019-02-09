using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSectionBounds : MonoBehaviour {
	
	private Vector3 sec1BoundsMin, sec1BoundsMax;
	public Vector3 sec2BoundsMin = new Vector3 (-20f, 1.5f, -10f);
	public Vector3 sec2BoundsMax = new Vector3 (-9f, 5f, 10f);
	private int section = 1;

	private new CameraControls camera;
	public GameObject player;

	void Start () {
		camera = gameObject.GetComponent<CameraControls> ();
		player = GameObject.FindGameObjectWithTag ("Player");

		sec1BoundsMin = camera.minCameraPos;
		sec1BoundsMax = camera.maxCameraPos;
	}

	void FixedUpdate () {

		Vector3 screenPos = Camera.main.WorldToViewportPoint (player.transform.position);

		if (screenPos.x < 0.0 && section == 1) { //Section 1 > Section 2
			camera.minCameraPos = sec2BoundsMin;
			camera.maxCameraPos = sec2BoundsMax;
			section = 2;
		}
		if (1.0 < screenPos.x && section == 2) { //Section 2 > Section 1
			camera.minCameraPos = sec1BoundsMin;
			camera.maxCameraPos = sec1BoundsMax;
			section = 1;
		}
	}
}
