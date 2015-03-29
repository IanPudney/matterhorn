using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class LoadCustomLevel : MonoBehaviour {
	void StartLevel (string customLevelUrl) {
		CustomLevelSerialized customLevel = CustomLevelSerialized.Load (customLevelUrl);

		for (int i=0; i<customLevel.SerializedObjects.Length; ++i) {
			Debug.Log ((customLevel.SerializedObjects[i]).ToString());
			GameObject obj = customLevel.SerializedObjects[i].generateElement();
			Debug.Log (obj);
			obj.transform.parent = this.transform;
		}
	}
}
[XmlRoot("CustomLevel")]
public class CustomLevelSerialized {
	[XmlArray("SerializedObject"),XmlArrayItem("SerializedObjects")]
	public SerializedObject[] SerializedObjects;

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(CustomLevelSerialized));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static CustomLevelSerialized Load(string path)
	{
		var serializer = new XmlSerializer(typeof(CustomLevelSerialized));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as CustomLevelSerialized;
		}
	}
}

[XmlInclude(typeof(MapSerialized))]
[XmlInclude(typeof(PlayerSerialized))]
[XmlInclude(typeof(GoalTargetSerialized))]
[XmlInclude(typeof(WallSerialized))]
[XmlInclude(typeof(SlidingWallSerialized))]
[XmlInclude(typeof(SpinnerSerialized))]
public abstract class SerializedObject {
	public abstract GameObject generateElement ();
}

public class MapSerialized : SerializedObject {
	[XmlAttribute("width")]
	public float width;
	override public GameObject generateElement() {
		return null;
	}
}

public class PlayerSerialized : SerializedObject {
	[XmlAttribute("xpos")]
	public float xpos;
	[XmlAttribute("ypos")]
	public float ypos;

	[XmlAttribute("xvel")]
	public float xvel;
	[XmlAttribute("yvel")]
	public float yvel;

	override public GameObject generateElement(){
		Debug.Log ("PlayerSerialized called");
		GameObject obj = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/Character"));
		obj.transform.localPosition = new Vector3 (xpos, ypos, 0f);
		obj.GetComponent<CharacterPhysics> ().initialForce = new Vector3 (xvel, yvel, 0);
		return obj;
	}
}

public class GoalTargetSerialized : SerializedObject {
	[XmlAttribute("xpos")]
	public float xpos;
	[XmlAttribute("ypos")]
	public float ypos;
	
	[XmlAttribute("size")]
	public float size;
	
	[XmlAttribute("zrot")]
	public float zrot;

	override public GameObject generateElement(){
		GameObject obj = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/GoalTarget"));
		obj.transform.localPosition = new Vector3 (xpos, ypos, 0f);
		obj.transform.localEulerAngles = new Vector3(0f, 0f, zrot);
		//todo: make GoalTarget have a better resizer
		obj.transform.localScale = new Vector3 (1f, size, 1f);
		return obj;
	}
}

public class WallSerialized : SerializedObject {
	[XmlAttribute("xpos")]
	public float xpos;
	[XmlAttribute("ypos")]
	public float ypos;
	[XmlAttribute("zrot")]
	public float zrot;
	
	[XmlAttribute("yscale")]
	public float yscale;

	override public GameObject generateElement(){
		GameObject obj = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/Wall"));
		obj.transform.localPosition = new Vector3 (xpos, ypos, 0f);
		obj.transform.localEulerAngles = new Vector3(0f, 0f, zrot);
		obj.transform.localScale = new Vector3 (1f, yscale, 1f);

		return obj;
	}
	
}

public class SlidingWallSerialized : SerializedObject {
	[XmlAttribute("xpos")]
	public float xpos;
	[XmlAttribute("ypos")]
	public float ypos;
	[XmlAttribute("zrot")]
	public float zrot;
	
	[XmlAttribute("velocity")]
	public float velocity;
	[XmlAttribute("height")]
	public float height;
	[XmlAttribute("travel")]
	public float travel;
	[XmlAttribute("startpos")]
	public float startpos;

	override public GameObject generateElement(){
		GameObject obj = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/SlidingWallContainer"));
		obj.transform.localPosition = new Vector3 (xpos, ypos, 0f);
		obj.transform.localEulerAngles = new Vector3(0f, 0f, zrot);
		obj.GetComponent<SlidingWallMove> ().velocity = velocity;
		obj.GetComponent<SlidingWallMove> ().height = height;
		obj.GetComponent<SlidingWallMove> ().travel = travel;
		obj.GetComponent<SlidingWallMove> ().startPosition = startpos;
		
		return obj;
	}

}

public class SpinnerSerialized : SerializedObject {
	[XmlAttribute("xpos")]
	public float xpos;
	[XmlAttribute("ypos")]
	public float ypos;
	[XmlAttribute("zrot")]
	public float zrot;
	
	[XmlAttribute("rotspeed")]
	public float rotspeed;
	[XmlAttribute("sticksize")]
	public float sticksize;
	[XmlAttribute("beamwidth")]
	public float beamwidth;
	
	override public GameObject generateElement(){
		GameObject obj = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/Spinner"));
		obj.transform.localPosition = new Vector3 (xpos, ypos, 0f);
		obj.transform.localEulerAngles = new Vector3(0f, 0f, zrot);
		obj.GetComponent<Spinner> ().rotationSpeed = rotspeed;
		obj.GetComponent<Spinner> ().stickSize = sticksize;
		obj.GetComponent<Spinner> ().crossbeamWidth = beamwidth;
		
		return obj;
	}
	
}

