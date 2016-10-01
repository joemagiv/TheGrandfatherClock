using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public bool enemyActive;
	private GameObject playerObject;

	public float enemyMovespeed;

	private bool touchingDoor;
	private Door currentDoor;

	public float timeAtDoor;

	private Status status;
	private PlayerMovement playerMovement;


	// Use this for initialization
	void Start () {
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		status = FindObjectOfType<Status> ().GetComponent<Status> ();
		playerMovement = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Door> ()) {
			touchingDoor = true;
			currentDoor = other.GetComponent<Door> ();
		}
		if (other.GetComponent<PlayerMovement> ()) {
			if (!playerMovement.isDashing) {
				status.changeStatus ("Player would have died");
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.GetComponent<Door> ()) {
			touchingDoor = false;
			timeAtDoor = 0f;
		}
	}

	void MoveTowardsPlayer(){
		transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), new Vector2 (playerObject.transform.position.x, transform.position.y), enemyMovespeed * Time.deltaTime);
		if (playerObject.transform.position.x < transform.position.x) {
			transform.localScale = new Vector3 (1, 1, 1);
		} else {
			transform.localScale = new Vector3 (-1, 1, 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyActive) {
			if (touchingDoor) {
				if (currentDoor.closed) {
					if (currentDoor.isBraced) {
						//Player is bracing door, do a timer to see if guy gets kept out
						timeAtDoor += Time.deltaTime;
						if (timeAtDoor >= 3f) {
							status.changeStatus ("Enemy was blocked at door");
							EnemySpawner enemySpawner = GetComponentInParent<EnemySpawner> ();
							enemySpawner.alexanderActive = false;
							Destroy (this.gameObject);
						}
					} else {
						//enemy kicks in door
						currentDoor.closed = false;
					}
				} else {
					MoveTowardsPlayer ();
				}
			} else {
				MoveTowardsPlayer ();
			}
		}
	}
}
