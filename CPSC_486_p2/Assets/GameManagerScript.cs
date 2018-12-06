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
	private Vector2 boxDirection;

	// Use this for initialization
	void Start () {
		ballDirection = new Vector2 (2, -1); // start by moving object straight down
		boxDirection = new Vector2 (-2, -1); // start by moving object straight down
		radius = ball.GetComponent<SphereCollider>().radius;
		screenHeight = 2f * cam.orthographicSize;
		screenWidth = screenHeight * cam.aspect;
		Debug.Log (box.transform.localScale.y);
	}

	// Update is called once per frame
	void Update () {
		ball.transform.Translate( ballDirection*gravity*Time.deltaTime, Space.World);
		box.transform.Translate( boxDirection*gravity*Time.deltaTime, Space.World);

		//Movement for when ball hits edges of Screen
		if (ball.transform.position.y - radius < -screenHeight/2 || ball.transform.position.y + radius > screenHeight/2) {
			ballDirection = new Vector2 (ballDirection.x, -ballDirection.y);
		}
		if (ball.transform.position.x - radius < -screenWidth/2 || ball.transform.position.x + radius > screenWidth/2) {
			ballDirection = new Vector2 (-ballDirection.x, ballDirection.y);
		}

		//Movement for when box hits edges of Screen
		if (box.transform.position.y - box.transform.localScale.y/2 < -screenHeight/2 || box.transform.position.y + box.transform.localScale.y/2 > screenHeight/2) {
			boxDirection = new Vector2 (boxDirection.x, -boxDirection.y);
		}
		if (box.transform.position.x - box.transform.localScale.x/2 < -screenWidth/2 || box.transform.position.x + box.transform.localScale.x/2 > screenWidth/2) {
			boxDirection = new Vector2 (-boxDirection.x, boxDirection.y);
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
				boxDirection = new Vector2 (-boxDirection.x, boxDirection.y);
			}//Ball hit left side
			else if (ball.transform.position.x < box.transform.position.x - box.transform.localScale.x/2) {
				Debug.Log ("Left Side!");
				ballDirection = new Vector2 (-ballDirection.x, ballDirection.y);
				boxDirection = new Vector2 (-boxDirection.x, boxDirection.y);
			}//Ball hit top
			else if (ball.transform.position.y > box.transform.position.y + box.transform.localScale.y/2) {
				Debug.Log ("Top!");
				ballDirection = new Vector2 (ballDirection.x, -ballDirection.y);
				boxDirection = new Vector2 (boxDirection.x, -boxDirection.y);
			}
			else if (ball.transform.position.y < box.transform.position.y - box.transform.localScale.y/2) {
				Debug.Log ("Bottom!");
				ballDirection = new Vector2 (ballDirection.x, -ballDirection.y);
				boxDirection = new Vector2 (boxDirection.x, -boxDirection.y);
			}
		} //end ball collision with box


	}
		

}
