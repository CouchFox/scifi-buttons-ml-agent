using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;   

public class GameManager : MonoBehaviour {

	public int unitSize = 2;



	void Start(){
		/*PersistentBoard board = new PersistentBoard ();
		PersistentLevel[] levels = new PersistentLevel[3];
		board.levels [0] = new PersistentLevel ();
		PositionSet posSet = new PositionSet ();*/



	}

	// Update is called once per frame
	void Update () {
		
	}

	public Button GetButtonById(int number){
		foreach (Button button in GameObject.FindObjectsOfType<Button>()) {
			//Debug.Log (button.gameObject.name + " " + button.accessNumber);
			if (button.accessNumber == number) {
				return button;
			}
		}
		return null;
	}

	public bool CheckForWin(){
		return GameObject.FindObjectOfType<Target> ().IsGameWon ();

	}

	public Button GetButtonByPosition(Button fromButton, Vector3 position){
		foreach (Button button in GameObject.FindObjectsOfType<Button>()) {
			if (!fromButton.Equals(button) && button.transform.position.Equals (position) && !button.isStatic ) {
				return button;
			}
		}
		return null;
	}
		



}
