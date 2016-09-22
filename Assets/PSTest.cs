using UnityEngine;
using System.Collections;

public class PSTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var mat = GetComponent<Renderer>().material;
        mat.color = TestProjectSettings.SomeColor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
