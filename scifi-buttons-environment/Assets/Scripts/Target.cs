using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Button {

	public bool IsGameWon(){ //todo adjust for multi-targets
		bool foundNonTargetButton = false;
		bool foundTargetButton = false;
		foreach (Button button in GameObject.FindObjectsOfType<Button>()) {
			if (transform.position.Equals(button.transform.position)) {
				if (!button.Equals (this)) {
					if (  button.isTarget) {
						//Debug.Log (button.transform.position);
						//Debug.Log (transform.position);
						foundTargetButton = true;

					} else  {
						foundNonTargetButton = true;
					}
				}

			}
		} 
		return foundTargetButton && !foundNonTargetButton;
	}
}
