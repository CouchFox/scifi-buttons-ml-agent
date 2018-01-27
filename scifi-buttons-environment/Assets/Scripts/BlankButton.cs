using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankButton : Button
{
	


	public override void Action ()
	{
		if (!isPressed ()) {
			//Debug.Log (gameObject.name + " Action");
			foreach (Button button in GameObject.FindObjectsOfType<Button>()) {
				if (!button.isStatic && !button.Equals (GetComponent<Button> ())) {
					
					if ((button.transform.position - transform.position).sqrMagnitude <= 2 * (Mathf.Pow(gameManager.unitSize,2))) {
						button.transform.localScale = new Vector3 (1, activeButtonHeight, 1);
					}

				}
			}
			transform.localScale = new Vector3 (1, inactiveButtonHeight, 1);
			//Debug.Log("blank button nr: "+accessNumber+" pressed!");
		}

	}
}
