using UnityEngine;
using System.Collections;
using UnityEditor;

public class LevelBorder : MonoBehaviour {
	public int height;
	public int width;
	// Use this for initialization
	void Start () {
		var pos = Vector3.zero;
		foreach(Transform child in transform)
		{
			pos += child.position;
		}
		pos /= transform.childCount;
		Camera.main.transform.position = pos - new Vector3 (0, 0, 10);


		float minWidth = width*12f * Screen.height / Screen.width * 0.5f;
		float minHeight = height * 12f * 0.5f;
		//float minHeight = 0;
		Camera.main.orthographicSize = Mathf.Max (minWidth, minHeight);
	}
	
	// Update is called once per frame
	void Update () {
	}
	[ContextMenu("Bake Size")]
	public void Bake() {
		if (height <= 0 || width <= 0)
			return;
		while (0 != transform.childCount) {
			DestroyImmediate(transform.GetChild(0).gameObject);
		}

		for(int i = 0; i < width; ++i) {
			GameObject obj = Instantiate(Resources.Load("prefabs/SpaceMetal")) as GameObject;
			obj.transform.position = new Vector3(10*i+2.5f, -2.5f, 0) + transform.position;
			obj.transform.parent = gameObject.transform;

			obj = Instantiate(Resources.Load("prefabs/SpaceMetal")) as GameObject;
			obj.transform.position = new Vector3(10*i+2.5f, 10*height-2.5f, 0) + transform.position;
			obj.transform.parent = gameObject.transform;
		}

		for(int i = 0; i < height; ++i) {
			GameObject obj = Instantiate(Resources.Load("prefabs/SpaceMetal")) as GameObject;
			obj.transform.position = new Vector3(-2.5f, 10*i+2.5f, 0) + transform.position;
			obj.transform.Rotate(new Vector3(0, 0, 90));
			obj.transform.parent = gameObject.transform;
			
			obj = Instantiate(Resources.Load("prefabs/SpaceMetal")) as GameObject;
			obj.transform.position = new Vector3(10*width-2.5f, 10*i+2.5f, 0) + transform.position;
			obj.transform.Rotate(new Vector3(0, 0, 90));
			obj.transform.parent = gameObject.transform;
		}

		//space metal corners
		GameObject cornerObj = Instantiate(Resources.Load ("prefabs/SpaceMetalCorner")) as GameObject;
		cornerObj.transform.position = new Vector3(10*width-1f + .34f, 10*height-1f + .075f, 0) + transform.position;
		cornerObj.transform.parent = gameObject.transform;

		cornerObj = Instantiate(Resources.Load ("prefabs/SpaceMetalCorner")) as GameObject;
		cornerObj.transform.position = new Vector3(-4.075f, 10*height-.66f, 0) + transform.position;
		cornerObj.transform.Rotate (0, 0, 90f);
		cornerObj.transform.parent = gameObject.transform;

		cornerObj = Instantiate(Resources.Load ("prefabs/SpaceMetalCorner")) as GameObject;
		cornerObj.transform.position = new Vector3(-4.342f, -4.076f, 0) + transform.position;
		cornerObj.transform.Rotate (0, 0, 180f);
		cornerObj.transform.parent = gameObject.transform;

		cornerObj = Instantiate(Resources.Load ("prefabs/SpaceMetalCorner")) as GameObject;
		cornerObj.transform.position = new Vector3(10*width-.926f, -4.34f, 0) + transform.position;
		cornerObj.transform.Rotate (0, 0, 270f);
		cornerObj.transform.parent = gameObject.transform;
	}
}

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
