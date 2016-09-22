using UnityEngine;
using System.Collections;

public class PSTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.color = TestProjectSettings.SomeColor;
        GUILayout.Label("Baboulinet");
    }

}
