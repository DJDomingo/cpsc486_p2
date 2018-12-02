using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

	public float gravity = 0.12f; // m/s^2
	public GameObject ball;
	public GameObject box;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		ball.transform.Translate( Vector2.down*gravity*Time.deltaTime, Space.World);
		box.transform.Translate( Vector2.down*gravity*Time.deltaTime, Space.World);
	}



}
