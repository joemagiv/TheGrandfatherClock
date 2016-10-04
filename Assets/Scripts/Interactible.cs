using UnityEngine;
using System.Collections;

public class Interactible : MonoBehaviour {

	public string objectName;

	public string statusText;

	public GameObject closeup;

	// Use this for initialization
	void Start () {
		closeup.SetActive (false);
	}

	public void DisplayCloseUp(){
		closeup.SetActive (true);
		Invoke ("DismissCloseUp", 2f);
	}

	void DismissCloseUp(){
		closeup.SetActive (false);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
