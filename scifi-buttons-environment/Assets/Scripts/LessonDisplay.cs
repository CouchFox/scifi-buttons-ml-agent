using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonDisplay : MonoBehaviour {

	ScifiButtonsAcademy academy;

	// Use this for initialization
	void Start () {
		academy = GameObject.FindObjectOfType<ScifiButtonsAcademy> ();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<TextMesh> ().text = academy.lessonNr.ToString ();
	}
}
