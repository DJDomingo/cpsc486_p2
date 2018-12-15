using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Script : MonoBehaviour {
	public Camera cam;
	float screenHeight;
	float screenWidth;

	public float accel = 0.12f; // m/s^2
	public float grav = 0.1f;
	public GameObject ball;
	public GameObject box;
	float radius;

	private Vector2 ballDirection;

	// Use this for initialization
	void Start () {
		ball = this.gameObject;
		ballDirection = new Vector2 (2, -1); // start by moving object straight down
		radius = ball.GetComponent<SphereCollider>().radius;
		screenHeight = 2f * cam.orthographicSize;
		screenWidth = screenHeight * cam.aspect;
	}

	// Update is called once per frame
	void Update () {
		ball.transform.Translate( ballDirection*accel*Time.deltaTime, Space.World);
		//ball.transform.Translate( Vector2.down*grav*Time.deltaTime, Space.World);

		//Movement for when ball hits edges of Screen
		if (ball.transform.position.y - radius < -screenHeight/2 || ball.transform.position.y + radius > screenHeight/2) {
			ballDirection = new Vector2 (ballDirection.x, -ballDirection.y);
		}
		if (ball.transform.position.x - radius < -screenWidth/2 || ball.transform.position.x + radius > screenWidth/2) {
			ballDirection = new Vector2 (-ballDirection.x, ballDirection.y);
		}

		//Ball is colliding with box (CURRENTLY MESSES UP ON CORNERS)
		if (ball.transform.position.x + radius > box.transform.position.x - box.transform.localScale.x/2 &&
			ball.transform.position.x - radius < box.transform.position.x + box.transform.localScale.x/2 &&
			ball.transform.position.y - radius < box.transform.position.y + box.transform.localScale.y/2 &&
			ball.transform.position.y + radius > box.transform.position.y - box.transform.localScale.y/2) {

			//Ball hit right side
			if (ball.transform.position.x > box.transform.position.x + box.transform.localScale.x/2) {
				Debug.Log ("Right Side!");
				ballDirection = new Vector2 (-ballDirection.x, ballDirection.y);
			}//Ball hit left side
			else if (ball.transform.position.x < box.transform.position.x - box.transform.localScale.x/2) {
				Debug.Log ("Left Side!");
				ballDirection = new Vector2 (-ballDirection.x, ballDirection.y);
			}//Ball hit top
			else if (ball.transform.position.y > box.transform.position.y + box.transform.localScale.y/2) {
				Debug.Log ("Top!");
				ballDirection = new Vector2 (ballDirection.x, -ballDirection.y);
			}
			else if (ball.transform.position.y < box.transform.position.y - box.transform.localScale.y/2) {
				Debug.Log ("Bottom!");
				ballDirection = new Vector2 (ballDirection.x, -ballDirection.y);
			}
		} //end ball collision with box


	}


}
