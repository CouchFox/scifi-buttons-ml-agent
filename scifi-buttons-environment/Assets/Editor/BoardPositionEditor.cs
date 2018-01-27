using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BoardPosition))]
public class BoardPositionEditor : Editor 
{
	
	private bool expanded;
	BoardPosition boardPos ;

	private void OnEnable () {

		boardPos = (BoardPosition)target ;

	}

	public override void OnInspectorGUI () {


		// Pull all the information from the target into the serializedObject.
		serializedObject.Update ();
		EditorGUILayout.BeginHorizontal ();
		boardPos.expanded =	EditorGUILayout.Foldout(boardPos.expanded, GetFoldoutLabel ());
		if (GUILayout.Button ("copy from board",GUILayout.Width(100))) {
			CopyPositionsFromBoard ();
		}
		if (GUILayout.Button ("copy to board",GUILayout.Width(100))) {
			CopyPositionsToBoard ();
		}
		EditorGUILayout.EndHorizontal ();
		if (boardPos.expanded) {
			boardPos.boardPositionNumber = EditorGUILayout.IntField("BoardPositionNumber", boardPos.boardPositionNumber);
			boardPos.activePos = EditorGUILayout.Vector3Field ("Active Position:", boardPos.activePos);
			boardPos.helperPos = EditorGUILayout.Vector3Field ("Helpoer Position:", boardPos.helperPos);
			boardPos.targetPos = EditorGUILayout.Vector3Field ("Target Position:", boardPos.targetPos);

		}

		// Push all the information from the serializedObject back into the target.
		serializedObject.ApplyModifiedProperties ();
	}

	protected string GetFoldoutLabel ()
	{
		return "Boardpos: " + boardPos.boardPositionNumber ;
	}

	protected void CopyPositionsFromBoard(){
		TidyUpButtons ();
		GameObject target = GameObject.Find ("Buttons/Target");
		GameObject active = GameObject.Find ("Buttons/TargetButton");
		GameObject helper = GameObject.Find ("Buttons/BlankButton");
		boardPos.activePos = active.transform.position;
		boardPos.helperPos = helper.transform.position;
		boardPos.targetPos = target.transform.position;
		CopyPositionsToBoard ();
	}

	protected void CopyPositionsToBoard(){
		GameObject target = GameObject.Find ("Buttons/Target");
		GameObject active = GameObject.Find ("Buttons/TargetButton");
		GameObject helper = GameObject.Find ("Buttons/BlankButton");
		active.transform.position = boardPos.activePos;
		helper.transform.position = boardPos.helperPos;
		target.transform.position= boardPos.targetPos;
	}

	public void TidyUpButtons (){
		GameObject target = GameObject.Find ("Buttons/Target");
		GameObject active = GameObject.Find ("Buttons/TargetButton");
		GameObject helper = GameObject.Find ("Buttons/BlankButton");

		target.transform.position = GetIntVector (target.transform.position);
		active.transform.position = GetIntVector (active.transform.position);
		helper.transform.position = GetIntVector (helper.transform.position);

	}

	public Vector3 GetIntVector(Vector3 vec){
		Vector3 tmpVec = Vector3.zero;
		tmpVec.x = Mathf.RoundToInt (vec.x);
		tmpVec.y = Mathf.RoundToInt (vec.y);
		tmpVec.z = Mathf.RoundToInt (vec.z);
		return tmpVec;

	}



}