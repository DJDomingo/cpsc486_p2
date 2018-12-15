Program Name: BlockFall Project#2
By:     Daniel Domingo (djdomingo@csu.fullertin.edu) cwid: 891717852, 
	Jordan Harvey  (jpharvey@csu.fullerton.edu)  cwid: 891213803, 
	Patrick Myers  (pmyers@csu.fullerton.edu)    cwid: 890354012,
	Kim-Lan Hoang  (khoangl@csu.fullerton.edu)   cwid: 895005478
	 
Team Name: ZZZ
Class #: 486

Files:  - GameManagerScript.cs [script that Unity runs to control the movements]
	- 486_p2.mp4 [Video showing the script running inside Unity]
	- README.txt [this file]

Intro: The task of this project is to simulate collision and movement between objects and
	the edges of the screen.

External Req: Unity was used to create the application.  The file provided is the script used.
	The video shows the script in-action inside Unity. While Unity was used, tools such as
	the built-in rigidbodies and collider functions were not used.

Build: Used Unity and coded with C#.  
Usage:  Currently project has:
	- Ball and Box object which move
	- Gravity affects the objects, but no friction so objects move indefinitely
	- Collision between objects and edges of the screen
	- Collision between objects themselves
	Still needs:
	- Collision could be improved and more accurate
	- More objects
	- Mass to each objects affecting the force

Extra Features: none
Bugs: Collision algorithm used causes objects to sometimes not collide when visibly they should
	as well as colliding too early other times.
