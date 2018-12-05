using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
	public Camera cam;
	float screenHeight;
	float screenWidth;

	//Force = mass*acceleration
	public float gravity = 0.12f; // m/s^2
	public GameObject ball;
	float radius;
	public GameObject box;

	private Vector2 ballDirection;

	// Use this for initialization
	void Start () {
		ballDirection = new Vector2 (0, -1); // start by moving object straight down
		radius = ball.GetComponent<SphereCollider>().radius;
		screenHeight = 2f * cam.orthographicSize;
		screenWidth = screenHeight * cam.aspect;
		Debug.Log (box.transform.localScale.y);
	}

	// Update is called once per frame
	void Update () {
		ball.transform.Translate( ballDirection*gravity*Time.deltaTime, Space.World);
		//box.transform.Translate( Vector2.down*gravity*Time.deltaTime, Space.World);

		//Movement for when ball hits edge of Screen
		if (ball.transform.position.y - radius < -screenHeight/2 || ball.transform.position.y + radius > screenHeight/2) {
			ballDirection = new Vector2 (ballDirection.x, -ballDirection.y);
		}
		if (ball.transform.position.x - radius < -screenWidth/2 || ball.transform.position.x + radius > screenWidth/2) {
			ballDirection = new Vector2 (-ballDirection.x, ballDirection.y);
		}

		//Movement for ball hitting other objects
		if (box.transform.position.x < ball.transform.position.x + radius &&
			box.transform.position.x + 3 > ball.transform.position.x &&
			box.transform.position.y < ball.transform.position.y + radius &&
			box.transform.position.y + 0.5 > ball.transform.position.y) {

			Debug.Log ("Collision!");
		}
	}
		

}
