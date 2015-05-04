using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(LevelBorder))]
public class LevelBorderEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		LevelBorder myScript = (LevelBorder)target;
		if (GUILayout.Button ("Bake")) {
			myScript.Bake ();
		}
		if (GUI.changed) {
			myScript.Bake();
		}
	}
}
