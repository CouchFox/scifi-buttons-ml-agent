using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level : ScriptableObject {

	public int levelNumber;
	public int boardNumber = 0;
	public bool expanded;
	public List<BoardPosition> boardPositions  = new List<BoardPosition>();
}


