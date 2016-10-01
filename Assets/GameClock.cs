using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameClock : MonoBehaviour {

	public int currentNight;

	public float timer;

	public float secondsInANight;

	public bool timerStarted;

	public Text clockText;
	public Text statusText;

	float hour1;
	float hour2;
	float hour3;
	float hour4;
	float hour5;


	// Use this for initialization
	void Start () {
		hour1 = 0f;
		hour2 = secondsInANight * 0.2f;
		hour3 = secondsInANight * 0.4f;
		hour4 = secondsInANight * 0.6f;
		hour5 = secondsInANight * 0.8f;
	}

	void UpdateClockText(float currentTime){
		if (currentTime > hour5) {
			clockText.text = "4 am";
		} else {
			if (currentTime > hour4) {
				clockText.text = "3 am";
			} else {
				if (currentTime > hour3) {
					clockText.text = "2 am";
				} else {
					if (currentTime > hour2) {
						clockText.text = "1 am";
					} else {
						if (currentTime > hour1) {
							clockText.text = "12 am";
						}
					}
				}
			}
		}
	}

	void NightComplete(){
		timerStarted = false;
		timer = 0;

		clockText.text = "5am";
		statusText.text = "Night Complete";
	}
	
	// Update is called once per frame
	void Update () {
		if (timerStarted) {
			timer += Time.deltaTime;
			UpdateClockText (timer);
			if (timer > secondsInANight) {
				NightComplete ();
			}
		}
	}
}
