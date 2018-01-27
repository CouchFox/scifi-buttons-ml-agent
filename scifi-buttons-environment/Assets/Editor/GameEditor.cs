using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


[CustomEditor(typeof(Game))]
public class GameEditor : Editor 
{
	Game game ;
	private bool expanded;

	private void OnEnable () {

		//boardPos = target as BoardPosition;
		game = target as Game;

	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginHorizontal ();
		game.expanded =	EditorGUILayout.Foldout(game.expanded, GetFoldoutLabel ());

		EditorGUILayout.EndHorizontal ();
		LevelGUI ();

	}

	protected string GetFoldoutLabel ()
	{
		return "Game: "  ;
	}

	private void LevelGUI(){
		EditorGUILayout.BeginHorizontal ();
		//level.boardPositions = EditorGUILayout.Foldout (level.boardPositions, "Show BoardPos List:");
		//game.levels = EditorGUILayout.Foldout (game.levels, "Show Level List:");
		game.expanded = EditorGUILayout.Foldout (game.expanded, "Show Level List:");

		if (GUILayout.Button ("+",GUILayout.Width(20))) {
			Level level = CreateInstance<Level> ();
			level.levelNumber = 0;
			level.boardPositions = new List<BoardPosition> ();
			game.levels.Add (level);
		}
		EditorGUILayout.EndHorizontal ();

		if (game.expanded) {
			for (int i = 0; i < game.levels.Count; i++) {
				EditorGUILayout.BeginVertical (GUI.skin.box);
				EditorGUI.indentLevel++;
				if (GUILayout.Button ("-",GUILayout.Width(20))) {
					game.levels.Remove (game.levels[i]);
					serializedObject.ApplyModifiedProperties ();
				}
				LevelEditor ed = CreateEditor (game.levels[i]) as LevelEditor;
				ed.OnInspectorGUI ();

				EditorGUI.indentLevel--;
				EditorGUILayout.EndVertical ();
			}
		}
	}


}