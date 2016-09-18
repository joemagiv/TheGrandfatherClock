using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public bool enemyActive;
	private GameObject playerObject;

	public float enemyMovespeed;

	private bool touchingDoor;
	private Door currentDoor;

	// Use this for initialization
	void Start () {
		playerObject = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Door> ()) {
			touchingDoor = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyActive) {
			if (!touchingDoor) {
				transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), playerObject.transform.position, enemyMovespeed * Time.deltaTime);
			}
		}
	}
}
