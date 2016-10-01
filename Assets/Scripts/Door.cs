using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public bool closed = true;
	public float doorTimer;
	public bool doorTimerPassed = true;
	public bool doorTimerStarted = false;
	public bool isBraced;
	private float timeToPass = 0.5f;

	private SpriteRenderer spriteRenderer;

	public Sprite closedSprite;
	public Sprite openSprite;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
	}


	
	// Update is called once per frame
	void Update () {
		if (doorTimerStarted) {
			doorTimerPassed = false;
			doorTimer += Time.deltaTime;
			if (doorTimer > timeToPass) {
				doorTimerPassed = true;
				doorTimerStarted = false;
				doorTimer = 0;
			}
		}

		if (closed) {
			spriteRenderer.sprite = closedSprite;
		} else {
			spriteRenderer.sprite = openSprite;

		}
	
	}
}
