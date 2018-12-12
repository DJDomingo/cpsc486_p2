using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript2 : MonoBehaviour {
	public Camera cam;
	float screenHeight;
	float screenWidth;

	//Force = mass*acceleration
	public float gravity = 0.12f; // m/s^2
	public GameObject ball;
	float radius;
	//public GameObject box, box2;
    public GameObject[] boxList; //List of N number of box(es)
    public Vector2[] boxDirList; //List of N number of directions. Amount in List must equal to boxList
    private Vector2 ballDirection;
	//private Vector2 boxDirection;
 //   private Vector2 box2Direction;
    private float distX; // variable float that obtain distance of right side of first box to left side of second box
    private float distX2;// variable float that obtain distance of left side of first box to right side of second box
    private float distY; // variable float that obtain distance of top of first box to box of second box
    private float distY2;// variable float that obtain distance of bottom of first box to top of second box

    // Use this for initialization
    void Start () {
		ballDirection = new Vector2 (2, -1); // start by moving object straight down
		//boxDirection = new Vector2 (-2, -1); // start by moving object straight down
  //      box2Direction = new Vector2(2, 1); // start by moving object straight down
        radius = ball.GetComponent<SphereCollider>().radius;
		screenHeight = 2f * cam.orthographicSize;
		screenWidth = screenHeight * cam.aspect;
	}

    // Update is called once per frame
    void Update()
    {

        ball.transform.Translate(ballDirection * gravity * Time.deltaTime, Space.World);

        for (int i = 0; i < boxList.Length; i++)
            boxList[i].transform.Translate(boxDirList[i] * gravity * Time.deltaTime, Space.World);

        //Movement for when ball hits edges of Screen
        if (ball.transform.position.y - radius < -screenHeight / 2 || ball.transform.position.y + radius > screenHeight / 2)
        {
            ballDirection = new Vector2(ballDirection.x, -ballDirection.y);
        }
        if (ball.transform.position.x - radius < -screenWidth / 2 || ball.transform.position.x + radius > screenWidth / 2)
        {
            ballDirection = new Vector2(-ballDirection.x, ballDirection.y);
        }

        //Movement for when box hits edges of Screen
        for (int i = 0; i < boxList.Length; i++)
        {
            if (boxList[i].transform.localPosition.y - boxList[i].transform.localScale.y / 2 < -screenHeight / 2 || boxList[i].transform.localPosition.y + boxList[i].transform.localScale.y / 2 > screenHeight / 2)
            {
                boxDirList[i] = new Vector2(boxDirList[i].x, -boxDirList[i].y);
            }
            if (boxList[i].transform.localPosition.x - boxList[i].transform.localScale.x / 2 < -screenWidth / 2 || boxList[i].transform.localPosition.x + boxList[i].transform.localScale.x / 2 > screenWidth / 2)
            {
                boxDirList[i] = new Vector2(-boxDirList[i].x, boxDirList[i].y);
            }

        }

        //Ball is colliding with box(es) (CURRENTLY MESSES UP ON CORNERS)
        for(int i = 0; i < boxList.Length; i++)
        {

            if (ball.transform.position.x + radius > boxList[i].transform.localPosition.x - boxList[i].transform.localScale.x / 2 &&
                ball.transform.position.x - radius < boxList[i].transform.localPosition.x + boxList[i].transform.localScale.x / 2 &&
                ball.transform.position.y - radius < boxList[i].transform.localPosition.y + boxList[i].transform.localScale.y / 2 &&
                ball.transform.position.y + radius > boxList[i].transform.localPosition.y - boxList[i].transform.localScale.y / 2)
            {

                //Ball hit right side
                if (ball.transform.position.x > boxList[i].transform.localPosition.x + boxList[i].transform.localScale.x / 2)
                {
                    Debug.Log("Right Side!");
                    ballDirection = new Vector2(-ballDirection.x, ballDirection.y);
                    boxDirList[i] = new Vector2(-boxDirList[i].x, boxDirList[i].y);
                }//Ball hit left side
                if (ball.transform.position.x < boxList[i].transform.localPosition.x - boxList[i].transform.localScale.x / 2)
                {
                    Debug.Log("Left Side!");
                    ballDirection = new Vector2(-ballDirection.x, ballDirection.y);
                    boxDirList[i] = new Vector2(-boxDirList[i].x, boxDirList[i].y);
                }//Ball hit top
                if (ball.transform.position.y > boxList[i].transform.localPosition.y + boxList[i].transform.localScale.y / 2)
                {
                    Debug.Log("Top!");
                    ballDirection = new Vector2(ballDirection.x, -ballDirection.y);
                    boxDirList[i] = new Vector2(boxDirList[i].x, -boxDirList[i].y);
                }
                if (ball.transform.position.y < boxList[i].transform.localPosition.y - boxList[i].transform.localScale.y / 2)
                {
                    Debug.Log("Bottom!");
                    ballDirection = new Vector2(ballDirection.x, -ballDirection.y);
                    boxDirList[i] = new Vector2(boxDirList[i].x, -boxDirList[i].y);
                }
            }
        }//end ball collision with box(es)

        for (int i = 0; i < boxList.Length; i++)
        {
            for(int j = i+1; j < boxList.Length; j++)
            {
                if (boxList[i].transform.localPosition.x + boxList[i].transform.localScale.x / 2 > boxList[j].transform.localPosition.x - boxList[j].transform.localScale.x / 2 &&
                boxList[i].transform.localPosition.x - boxList[i].transform.localScale.x / 2 < boxList[j].transform.localPosition.x + boxList[j].transform.localScale.x / 2 &&
                boxList[i].transform.localPosition.y - boxList[i].transform.localScale.y / 2 < boxList[j].transform.localPosition.y + boxList[j].transform.localScale.y / 2 &&
                boxList[i].transform.localPosition.y + boxList[i].transform.localScale.y / 2 > boxList[j].transform.localPosition.y - boxList[j].transform.localScale.y / 2)
                    {
                    /*
                     *This portion first gets the distance between two boxes' sides. distX and distX2 gets the distance of left/right sides
                     *while distY and distY2 gets the distance of top/bottom. Get the average speed between two boxes and add them to
                     * the variables to allow more leeway to avoid constants collision checks.
                     * the variables to allow more leeway to avoid constants collision checks.
                     */
                    distX = (boxDirList[i].x * boxDirList[j].x)/2 + Mathf.Sqrt(Mathf.Pow((boxList[i].transform.localPosition.x - boxList[i].transform.localScale.x / 2) - (boxList[j].transform.localPosition.x + boxList[j].transform.localScale.x / 2), 2));
                    distX2 = (boxDirList[i].x * boxDirList[j].x) / 2 + Mathf.Sqrt(Mathf.Pow((boxList[i].transform.localPosition.x + boxList[i].transform.localScale.x / 2) -(boxList[j].transform.localPosition.x - boxList[j].transform.localScale.x / 2),2));
                    distY = (boxDirList[i].y * boxDirList[j].y) / 2 + Mathf.Sqrt(Mathf.Pow((boxList[i].transform.localPosition.y + boxList[i].transform.localScale.y / 2) - (boxList[j].transform.localPosition.y - boxList[j].transform.localScale.y / 2),2));
                    distY2 = (boxDirList[i].y * boxDirList[j].y) / 2 + Mathf.Sqrt(Mathf.Pow((boxList[i].transform.localPosition.y - boxList[i].transform.localScale.y / 2) - (boxList[j].transform.localPosition.y + boxList[j].transform.localScale.y / 2),2));
                    
                    if((distX <= distY && distX <= distY2)||(distX2 <= distY && distX2 <= distY2))
                    {   //if either distance of left/right side of 2 boxes is smaller than the distance of the top/bottom of the two boxes
                        boxDirList[i] = new Vector2(-boxDirList[i].x, boxDirList[i].y);
                        boxDirList[j] = new Vector2(-boxDirList[j].x, boxDirList[j].y);
                    }

                    Debug.Log((distY2 <= distX && distX <= distX2));
                    if((distY <= distX && distY <= distX2)||(distY2 <= distX && distX <= distX2))
                    {   //if either distance of top/bottom side of 2 boxes is smaller than the distance of the left/right of the two boxes
                        boxDirList[i] = new Vector2(boxDirList[i].x, -boxDirList[i].y);
                        boxDirList[j] = new Vector2(boxDirList[j].x, -boxDirList[j].y);
                    }//box i's top hits box j's bottom

                }
            }
        }
        
    }

}
