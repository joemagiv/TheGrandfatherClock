using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Status : MonoBehaviour {

	private Text textElement;

	// Use this for initialization
	void Start () {
		textElement = GetComponent<Text> ();
		textElement.text = ""; 
	}

	public void changeStatus(string t){
		textElement.text = t;
		Invoke ("ClearStatus", 2f);
	}

	public void ClearStatus(){
		textElement.text = ""; 

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
