using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Script : MonoBehaviour {
	public Camera cam;
	float screenHeight;
	float screenWidth;

	//Force = mass*acceleration
	public float accel = 0.12f; // m/s^2
	public float grav = 0.1f;
	public float turnSpeed = 5;
	public GameObject ball;
	float radius;
	public GameObject box;

	private Vector2 ballDirection;
	private Vector2 boxDirection;
	private Vector3 turnDir;

	private Vector2 topL;
	private Vector2 topR;
	private Vector2 botL;
	private Vector2 botR;

	// Use this for initialization
	void Start () {
		box = this.gameObject;
		boxDirection = new Vector2 (-2, 1); // start by moving object straight down
		turnDir = Vector3.forward;
		radius = ball.GetComponent<SphereCollider>().radius;
		screenHeight = 2f * cam.orthographicSize;
		screenWidth = screenHeight * cam.aspect;
		Debug.Log (box.transform.localScale.y);
	}

	// Update is called once per frame
	void Update () {
		//calculate vertice locations
		topL = new Vector2( ( (box.transform.position.x - box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Cos(box.transform.rotation.z) - 
			( (box.transform.position.y + box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Sin(box.transform.rotation.z) + box.transform.position.x,
			( (box.transform.position.x - box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Sin(box.transform.rotation.z) - 
			( (box.transform.position.y + box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Cos(box.transform.rotation.z) + box.transform.position.y
		);
		topR = new Vector2( ( (box.transform.position.x + box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Cos(box.transform.rotation.z) - 
			( (box.transform.position.y + box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Sin(box.transform.rotation.z) + box.transform.position.x,
			( (box.transform.position.x + box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Sin(box.transform.rotation.z) - 
			( (box.transform.position.y + box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Cos(box.transform.rotation.z) + box.transform.position.y
		);
		botL = new Vector2( ( (box.transform.position.x - box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Cos(box.transform.rotation.z) - 
			( (box.transform.position.y - box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Sin(box.transform.rotation.z) + box.transform.position.x,
			( (box.transform.position.x - box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Sin(box.transform.rotation.z) - 
			( (box.transform.position.y - box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Cos(box.transform.rotation.z) + box.transform.position.y
		);
		botR = new Vector2( ( (box.transform.position.x + box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Cos(box.transform.rotation.z) - 
			( (box.transform.position.y - box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Sin(box.transform.rotation.z) + box.transform.position.x,
			( (box.transform.position.x + box.transform.localScale.x/2) - box.transform.position.x)*Mathf.Sin(box.transform.rotation.z) - 
			( (box.transform.position.y - box.transform.localScale.y/2) - box.transform.position.y)*Mathf.Cos(box.transform.rotation.z) + box.transform.position.y
		);

		box.transform.Translate( boxDirection*accel*Time.deltaTime, Space.World);
		//box.transform.Translate( Vector2.down*grav*Time.deltaTime, Space.World);
		box.transform.Rotate( turnDir, turnSpeed*Time.deltaTime, Space.World);

		//Movement for when box hits edges of Screen
		if (box.transform.position.y - box.transform.localScale.y/2 < -screenHeight/2 || box.transform.position.y + box.transform.localScale.y/2 > screenHeight/2) {
			boxDirection = new Vector2 (boxDirection.x, -boxDirection.y);
			turnDir = -turnDir;
		}
		if (box.transform.position.x - box.transform.localScale.x/2 < -screenWidth/2 || box.transform.position.x + box.transform.localScale.x/2 > screenWidth/2) {
			boxDirection = new Vector2 (-boxDirection.x, boxDirection.y);
			turnDir = -turnDir;
		}

		//Ball is colliding with box (CURRENTLY MESSES UP ON CORNERS)
		if (ball.transform.position.x + radius > box.transform.position.x - box.transform.localScale.x/2 &&
			ball.transform.position.x - radius < box.transform.position.x + box.transform.localScale.x/2 &&
			ball.transform.position.y - radius < box.transform.position.y + box.transform.localScale.y/2 &&
			ball.transform.position.y + radius > box.transform.position.y - box.transform.localScale.y/2) {

			//Ball hit right side
			if (ball.transform.position.x > box.transform.position.x + box.transform.localScale.x/2) {
				Debug.Log ("Right Side!");
				boxDirection = new Vector2 (-boxDirection.x, boxDirection.y);
			}//Ball hit left side
			else if (ball.transform.position.x < box.transform.position.x - box.transform.localScale.x/2) {
				Debug.Log ("Left Side!");
				boxDirection = new Vector2 (-boxDirection.x, boxDirection.y);
			}//Ball hit top
			else if (ball.transform.position.y > box.transform.position.y + box.transform.localScale.y/2) {
				Debug.Log ("Top!");
				boxDirection = new Vector2 (boxDirection.x, -boxDirection.y);
			}
			else if (ball.transform.position.y < box.transform.position.y - box.transform.localScale.y/2) {
				Debug.Log ("Bottom!");
				boxDirection = new Vector2 (boxDirection.x, -boxDirection.y);
			}
		} //end ball collision with box


	}


}
