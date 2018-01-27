using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnButton : Button {



	public override void Action(){
		
		if (!isPressed()){
			//Debug.Log (gameObject.name + " Action");
			foreach (Button button in GameObject.FindObjectsOfType<Button>()) {
				if (!button.isStatic && !button.Equals (GetComponent<Button>())) {
					Rotate (button);

				}
			}
			transform.localScale = new Vector3 (1, inactiveButtonHeight, 1);
		}

	}



	public void Rotate(Button button){
		//Debug.Log(button.gameObject.name + " " + (button.transform.position - transform.position).sqrMagnitude);
		if (!button.isPressed () && (button.transform.position - transform.position).sqrMagnitude <= 2 * Mathf.Pow(gameManager.unitSize,2)) {

			button.transform.position = transform.position + GetVecForRelPosNumber ((GetRelPosNumber (button.transform.position-transform.position) + 9) % 8);

			Button nextButton = gameManager.GetButtonByPosition (button, button.transform.position);
			if (nextButton != null && !nextButton.Equals(button) && nextButton.isPressed ()) {
				Rotate (button);
			}


		}


	}
		


}
