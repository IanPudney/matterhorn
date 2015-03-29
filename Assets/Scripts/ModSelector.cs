using UnityEngine;
using System.Collections;
using System.IO; 
using UnityEngine.UI;

public class ModSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var info = new DirectoryInfo("mods/");
		var fileInfo = info.GetFiles();
		//GameObject dummyButton = (GameObject)GameObject.Find ("DummyButton");
		GameObject prev = null;
		for (int i=0; i<fileInfo.Length; ++i) {
			GameObject obj = (GameObject) Instantiate(Resources.Load ("Prefabs/CustomLevelLoaderButton"));
			obj.transform.SetParent (this.transform);
			obj.GetComponent<CustomLevelLoaderButton>().path = fileInfo[i].FullName;
			obj.GetComponentInChildren<Text>().text = fileInfo[i].Name;
			obj.transform.SetAsLastSibling();

			if(prev) {
				obj.GetComponent<RectTransform>().pivot = prev.GetComponent<RectTransform>().pivot + new Vector2(0f, 1f);
			}

			prev = obj;
		}
		//GameObject.Destroy (dummyButton);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
