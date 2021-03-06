﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed;
	public bool canMoveToRight = true;
	public bool canMoveToLeft = true;

	public bool isDashing = false;
	public bool canDash = true;
	public float timeSinceDash;
	public float dashingTime = 1f;
	public float timeAbleToDash = 10f;

	public bool touchingDoor = false;
	private Door currentDoor;

	public float timeAtDoor;
	public bool isBracing = true;

	public bool touchingInteractible = false;
	public Interactible currentInteractible;

	public bool touchingStairs = false;
	private Stairs currentStairs;
	public bool walkingOnStairs = false;

	private Animator anim;

	private Status status;

	// Use this for initialization
	void Start () {
		status = FindObjectOfType<Status> ().GetComponent<Status> ();
		anim = GetComponent<Animator> ();
	}

	private void moveToRight(){
		if (canMoveToRight) {
			if (!isDashing) {
				Vector3 newScale = new Vector3 (1, 1, 1);
				transform.localScale = newScale;
				Vector3 newPosition = new Vector3 (transform.position.x + movementSpeed, transform.position.y, transform.position.z);
				transform.position = newPosition;
			} else {
				Vector3 newScale = new Vector3 (1, 1, 1);
				transform.localScale = newScale;
				Vector3 newPosition = new Vector3 (transform.position.x + (movementSpeed * 2), transform.position.y, transform.position.z);
				transform.position = newPosition;
			}
			anim.SetBool ("IsWalking", true);
			anim.SetBool ("IsBracing", false);

		} else {
			anim.SetBool ("IsWalking", false);
		}
	}

	private void moveToLeft(){
		if (canMoveToLeft) {
			if (!isDashing) {
				Vector3 newScale = new Vector3 (-1, 1, 1);
				transform.localScale = newScale;
				Vector3 newPosition = new Vector3 (transform.position.x - movementSpeed, transform.position.y, transform.position.z);
				transform.position = newPosition;
			} else {
				Vector3 newScale = new Vector3 (-1, 1, 1);
				transform.localScale = newScale;
				Vector3 newPosition = new Vector3 (transform.position.x - (movementSpeed*2), transform.position.y, transform.position.z);
				transform.position = newPosition;
			} 
			anim.SetBool ("IsWalking", true);
			anim.SetBool ("IsBracing", false);

		} else {
			anim.SetBool ("IsWalking", false);
		}
	}


	void OnTriggerEnter2D(Collider2D other) {

		//Colliders for Doors
		if (other.GetComponent<Door> ()) {
			touchingDoor = true;
			currentDoor = other.GetComponent<Door> ();
			if (currentDoor.closed) {
				if (other.transform.position.x > transform.position.x) {
					canMoveToRight = false;
				} else {
					canMoveToLeft = false;
				}
			}
		}

		//Colliders for Interactibles
		if (other.GetComponent<Interactible> ()) {
			Debug.Log ("I'm touching: " + other.name);
			touchingInteractible = true;
			currentInteractible = other.GetComponent<Interactible> ();
		}

		//Colliders for barriers
		if (other.GetComponent<Collider2D>().tag == "Barrier") {
			if (other.transform.position.x > transform.position.x) {
				canMoveToRight = false;
			} else {
				canMoveToLeft = false;
			}
		}

		//Colliders for stairs
		if (other.GetComponent<Stairs> ()) {
			touchingStairs = true;
			currentStairs = other.GetComponent<Stairs> ();
		}
	}


	void OnTriggerExit2D(Collider2D other) {
		if (other.GetComponent<Door> ()) {
			touchingDoor = false;
			if (!canMoveToRight) {
				canMoveToRight = true;
			} 
			if (!canMoveToLeft) {
				canMoveToLeft = true;
			}
			timeAtDoor = 0f;
			isBracing = false;
			anim.SetBool ("IsBracing", false);
			currentDoor.isBraced = false;
		}
		if (other.GetComponent<Interactible> ()) {
			touchingInteractible = false;
//			currentInteractible = other.GetComponent<Interactible> ();
		}
		if (other.GetComponent<Collider2D>().tag == "Barrier") {
			if (!canMoveToRight) {
				canMoveToRight = true;
			} 
			if (!canMoveToLeft) {
				canMoveToLeft = true;
			}
		}

		if (other.GetComponent<Stairs> ()) {
			touchingStairs = false;
		}
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Horizontal") > 0) {
			moveToRight ();
		}
		if (Input.GetAxis ("Horizontal") < 0) {
			moveToLeft ();
		} 

		if (Input.GetAxis ("Horizontal") == 0) {
			anim.SetBool ("IsWalking", false);
			anim.SetBool ("IsBracing", false);
		}

		if (Input.GetButton ("Interact")) {
			if (touchingDoor) {
				Debug.Log ("attempting to open door");
				OpenDoor ();
			}
		}

		if (Input.GetButton ("Examine")) {
			if (touchingInteractible) {
				Debug.Log ("examiningObject");
				status.changeStatus (currentInteractible.statusText);
				currentInteractible.DisplayCloseUp ();
			}
		}

		if (Input.GetButton("Dash")){
			if (canDash){
				isDashing = true;
				canDash = false;
			}
		}
		if (isDashing) {
			timeSinceDash += Time.deltaTime;
			if (timeSinceDash > dashingTime) {
				isDashing = false;
			}
		}
		if (!canDash) {
			timeSinceDash += Time.deltaTime;
			if (timeSinceDash > timeAbleToDash) {
				canDash = true;
				timeSinceDash = 0f;
			}
		}

		if (touchingStairs) {
			if (Input.GetAxis ("Vertical") > 0) {
				GoUpstairs ();
				if (walkingOnStairs) {
					canMoveToLeft = false;
					canMoveToRight = false;
				}
			}
			if (Input.GetAxis ("Vertical") < 0) {
				GoDownstairs ();
				if (walkingOnStairs) {
					canMoveToLeft = false;
					canMoveToRight = false;
				}
			}
		}

		//checking for Bracing
		if (touchingDoor) {
			if (currentDoor.closed) {
				timeAtDoor += Time.deltaTime;
				if (timeAtDoor >= 0.5f) {
					isBracing = true;
					currentDoor.isBraced = true;
					anim.SetBool ("IsBracing", true);
				} else {
					anim.SetBool ("IsBracing", false);
				}
			}
		}

//		if (!Input.anyKey) {
//			//no input, go back to idle animation
//			anim.SetBool ("IsWalking", false);
//			anim.SetBool ("IsBracing", false);
//		}

	}
		


	void GoUpstairs(){
		walkingOnStairs = true;

		Debug.Log (Vector2.Distance (transform.position, currentStairs.topLocation));
		transform.position = Vector2.MoveTowards (transform.position, currentStairs.topLocation, movementSpeed);
//		if (transform.position.y >= currentStairs.topLocation.y) {
		if (Vector2.Distance(transform.position, currentStairs.topLocation) == 0f){
			walkingOnStairs = false;
			canMoveToLeft = true;
			canMoveToRight = true;
		}
	}

	void GoDownstairs(){
		walkingOnStairs = true;
		transform.position = Vector2.MoveTowards (transform.position, currentStairs.bottomLocation, movementSpeed);
		if (Vector2.Distance(transform.position, currentStairs.bottomLocation) == 0f) {
			walkingOnStairs = false;
			canMoveToLeft = true;
			canMoveToRight = true;
		}
	}

	void OpenDoor ()
	{
		if (currentDoor.doorTimerPassed) {
			currentDoor.doorTimerStarted = true;
			if (currentDoor.closed) {
				currentDoor.closed = false;
				if (currentDoor.transform.position.x > transform.position.x) {
					canMoveToRight = true;
				} else {
					canMoveToLeft = true;
				}
			} else {
				CloseDoor ();
			}
		}
	}

	void CloseDoor(){
		if (currentDoor.doorTimerPassed) {
			currentDoor.doorTimerStarted = true;
			currentDoor.closed = true;
			if (currentDoor.transform.position.x > transform.position.x) {
				canMoveToRight = false;
			} else {
				canMoveToLeft = false;
			}
		}
	}
}
