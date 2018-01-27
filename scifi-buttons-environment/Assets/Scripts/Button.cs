using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

	public bool isStatic = false;
	public bool isTarget = false;
	public int accessNumber = 0;
	public GameManager gameManager;
	public int scale;
	public int negScale;
	public float activeButtonHeight;
	public float inactiveButtonHeight;

	public Vector3[] relPositions;


	public void Awake(){
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		scale = gameManager.unitSize;
		negScale = -1 * scale;
		activeButtonHeight = 1f;
		inactiveButtonHeight = 0.1f;
	
		relPositions  = new [] { 
			new Vector3 (negScale, 0, scale), 
			new Vector3 (0, 0, scale),
			new Vector3 (scale, 0, scale),
			new Vector3 (scale, 0, 0),
			new Vector3 (scale, 0, negScale),
			new Vector3 (0, 0, negScale),
			new Vector3 (negScale, 0, negScale),
			new Vector3 (negScale, 0, 0)
		};

	}

	public virtual void Action (){
		
	}

	public virtual bool isPressed(){
		return transform.localScale.y < activeButtonHeight && !(isStatic && isTarget);
	}

	public int GetRelPosNumber(Vector3 relVec){
		
		for (int i = 0; i< relPositions.Length; i++){
			if (relVec.Equals (relPositions[i])) {
				return i;
			}
		}
		Debug.LogError ("relpos for vector " + relVec + " not found");
		return 0;

	}

	public Vector3 GetVecForRelPosNumber(int pos){
		return relPositions [pos];
	}
}
