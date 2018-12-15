using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
	public Camera cam;
	float screenHeight;
	float screenWidth;

	//Force = mass*acceleration
	public float accel = 2f; // m/s^2
	public float grav = 0.5f;
	public float turnSpeed = 60;
	public GameObject ball;
	float radius;
	public GameObject box;

	private Vector2 ballDirection;
	private Vector2 boxDirection;
	private Vector3 turnDir;

	//4 Vertices for the Box
	private Vector2 topL;
	private Vector2 topR;
	private Vector2 botL;
	private Vector2 botR;

	// Use this for initialization
	void Start () {
		ballDirection = new Vector2 (2, -1); // start by moving object straight down
		boxDirection = new Vector2 (-2, -1); // start by moving object straight down
		turnDir = Vector3.forward; //start turning in forward direction
		radius = ball.GetComponent<SphereCollider>().radius;
		screenHeight = 2f * cam.orthographicSize; //Get the full Height of the Screen
		screenWidth = screenHeight * cam.aspect; //Get the full Width of the Screen
	}

	// Update is called once per frame
	void Update () {
		ball.transform.Translate( ballDirection*accel*Time.deltaTime, Space.World); //start movement of ball
		box.transform.Translate( boxDirection*accel*Time.deltaTime, Space.World); //start movement of box
		ball.transform.Translate( Vector2.down*grav*Time.deltaTime, Space.World); //simulate gravity
		box.transform.Translate( Vector2.down*grav*Time.deltaTime, Space.World);  //simulate gravity
		box.transform.Rotate( turnDir, turnSpeed*Time.deltaTime, Space.World);  //start rotation of box

		#region Movement for when ball hits edges of Screen
			//bot of screen
		if (ball.transform.position.y - radius < -screenHeight/2) {
			ballDirection = new Vector2 (ballDirection.x, Mathf.Abs( ballDirection.y));
		}
			//top
		if (ball.transform.position.y + radius > screenHeight/2) {
			ballDirection = new Vector2 (ballDirection.x, -Mathf.Abs( ballDirection.y));
		}
			//left
		if (ball.transform.position.x - radius < -screenWidth/2) {
			ballDirection = new Vector2 (Mathf.Abs( ballDirection.x), ballDirection.y);
		}
			//right
		if (ball.transform.position.x + radius > screenWidth/2) {
			ballDirection = new Vector2 (-Mathf.Abs( ballDirection.x), ballDirection.y);
		}
		#endregion

		#region calculate vertex locations
		topL = new Vector2( ( (box.transform.position.x - box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Cos(box.transform.rotation.z) - 
			( (box.transform.position.y + box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Sin(box.transform.rotation.z) + box.transform.position.x ,
			( (box.transform.position.x - box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Sin(box.transform.rotation.z) + 
			( (box.transform.position.y + box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Cos(box.transform.rotation.z) + box.transform.position.y
		);
		topR = new Vector2( ( (box.transform.position.x + box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Cos(box.transform.rotation.z) - 
			( (box.transform.position.y + box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Sin(box.transform.rotation.z) + box.transform.position.x ,
			( (box.transform.position.x + box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Sin(box.transform.rotation.z) + 
			( (box.transform.position.y + box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Cos(box.transform.rotation.z) + box.transform.position.y
		);
		botL = new Vector2( ( (box.transform.position.x - box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Cos(box.transform.rotation.z) - 
			( (box.transform.position.y - box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Sin(box.transform.rotation.z) + box.transform.position.x ,
			( (box.transform.position.x - box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Sin(box.transform.rotation.z) + 
			( (box.transform.position.y - box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Cos(box.transform.rotation.z) + box.transform.position.y
		);
		botR = new Vector2( ( (box.transform.position.x + box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Cos(box.transform.rotation.z) - 
			( (box.transform.position.y - box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Sin(box.transform.rotation.z) + box.transform.position.x ,
			( (box.transform.position.x + box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Sin(box.transform.rotation.z) + 
			( (box.transform.position.y - box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Cos(box.transform.rotation.z) + box.transform.position.y
		);
		Debug.Log ("TopL: " + topL);
		Debug.Log ("TopR: " + topR);
		Debug.Log ("BotL: " + botL);
		Debug.Log ("botR: " + botR);
		Debug.Log ("ScreenHeight: " + screenHeight);
		Debug.Log ("ScreenWidth: " + screenWidth);
		#endregion

		#region Movement for when box hits edges of Screen
			//bot of screen
		if (botL.y < -screenHeight/2 || botR.y < -screenHeight/2 || topL.y < -screenHeight/2 || topR.y < -screenHeight/2) {
			boxDirection = new Vector2 (boxDirection.x, Mathf.Abs(boxDirection.y));
			turnDir = -turnDir;
		}
			//top
		if (botL.y > screenHeight/2 || botR.y > screenHeight/2 || topL.y > screenHeight/2 || topR.y > screenHeight/2) {
			boxDirection = new Vector2 (boxDirection.x, -Mathf.Abs(boxDirection.y));
			turnDir = -turnDir;
		}
			//left
		if (botL.x < -screenWidth/2 || botR.x < -screenWidth/2 || topL.x < -screenWidth/2 || topR.x < -screenWidth/2) {
			boxDirection = new Vector2 (Mathf.Abs(boxDirection.x), boxDirection.y);
			turnDir = -turnDir;
		}
			//right
		if (botL.x > screenWidth/2 || botR.x > screenWidth/2 || topL.x > screenWidth/2 || topR.x > screenWidth/2) {
			boxDirection = new Vector2 (-Mathf.Abs(boxDirection.x), boxDirection.y);
			turnDir = -turnDir;
		}
		#endregion

		#region Ball is colliding with box (CURRENTLY MESSES UP ON CORNERS)
		if (ball.transform.position.x + radius > box.transform.position.x - box.transform.localScale.x/2 &&
			ball.transform.position.x - radius < box.transform.position.x + box.transform.localScale.x/2 &&
			ball.transform.position.y - radius < box.transform.position.y + box.transform.localScale.y/2 &&
			ball.transform.position.y + radius > box.transform.position.y - box.transform.localScale.y/2) {
			Debug.Log("Ball Box Collision!");
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
			turnDir = -turnDir;
		} //end ball collision with box
		#endregion

	}
		

}
