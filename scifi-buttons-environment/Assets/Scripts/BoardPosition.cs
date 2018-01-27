using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardPosition : ScriptableObject{

	public int boardPositionNumber;
	public Vector3 helperPos;
	public Vector3 activePos;
	public Vector3 targetPos;
	public bool expanded;
}