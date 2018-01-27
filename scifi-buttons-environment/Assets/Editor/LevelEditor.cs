using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(Level))]
public class LevelEditor : Editor {

	Level level ;
	private bool levelListExpanded;

	private void OnEnable () {

		level = target as Level;

	}

	public override void OnInspectorGUI()
	{
		//level.expanded =	EditorGUILayout.Foldout(level.expanded, GetFoldoutLabel ());
		BoardPosGUI ();


	}

	protected string GetFoldoutLabel ()
	{
		return "Level: " + level.levelNumber ;
	}

	private void BoardPosGUI(){
		EditorGUILayout.BeginHorizontal ();

		EditorGUILayout.BeginVertical ();
		level.levelNumber = EditorGUILayout.IntField("Level-Nr.:", level.levelNumber);
		level.boardNumber = EditorGUILayout.IntField("Board-Nr.:", level.boardNumber);
		level.expanded = EditorGUILayout.Foldout (level.expanded, "Show BoardPos List:");

		EditorGUILayout.EndVertical ();

		if (GUILayout.Button ("+",GUILayout.Width(20))) {
			BoardPosition boardPos = CreateInstance<BoardPosition> ();
			boardPos.boardPositionNumber = 0;
			boardPos.activePos = Vector3.zero;
			boardPos.helperPos = Vector3.zero;
			boardPos.targetPos = Vector3.zero;
			level.boardPositions.Add (boardPos);
		}

		EditorGUILayout.EndHorizontal ();

		if (level.expanded) {
			for (int i = 0; i < level.boardPositions.Count; i++) {
				EditorGUILayout.BeginVertical (GUI.skin.box);
				EditorGUI.indentLevel++;
				if (GUILayout.Button ("-",GUILayout.Width(20))) {
					level.boardPositions.Remove (level.boardPositions [i]);
					serializedObject.ApplyModifiedProperties ();
				}
				BoardPositionEditor ed = CreateEditor (level.boardPositions[i]) as BoardPositionEditor;
				ed.OnInspectorGUI ();

				EditorGUI.indentLevel--;
				EditorGUILayout.EndVertical ();
			}
		}
	}
}
