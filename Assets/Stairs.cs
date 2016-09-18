using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour {

	public bool bottomOfStairs;

	public Vector2 topLocation;
	public Vector2 bottomLocation;

	public GameObject topObject;
	public GameObject bottomObject;

	// Use this for initialization
	void Start () {
		topLocation = topObject.transform.position;
		bottomLocation = bottomObject.transform.position;

//		if (bottomOfStairs) {
//			bottomLocation = transform.position;
//			topLocation = new Vector2 (transform.position.x, transform.position.y + 1.4f);
//		} else {
//			topLocation = transform.position;
//			bottomLocation = new Vector2 (transform.position.x, transform.position.y - 1.4f);
//		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
