using UnityEngine;
using System.Collections;
using UnityEditor;

public class BoxProjectedCubemapWizard : ScriptableWizard {
	
	public string cubemapName = "Cubemap Zone 01";
	public int cubemapResolution = 256;
	public Transform startPoint = null, endPoint = null;
	public Renderer[] parallaxCorrectedMaterials;
	public Renderer[] renderersToExclude;
	
	[MenuItem ("GameObject/Create Box Projected Cubemap")]
    static void CreateWizard () {
        ScriptableWizard.DisplayWizard<BoxProjectedCubemapWizard>("Create Box Projected Cubemap", "Create");
    }
	
    void OnWizardCreate () {
    	if (startPoint != null && endPoint != null) {
        // Create a cubemap probe
		Vector3 center = startPoint.position + (endPoint.position - startPoint.position)/2;
		GameObject cubemapProbe = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		cubemapProbe.name = cubemapName;
		cubemapProbe.transform.position = center;
		cubemapProbe.transform.rotation = Quaternion.identity;
		GameObject cubemapProbes = GameObject.Find("Cubemap Probes");
		if (cubemapProbes == null) {
			cubemapProbes = new GameObject("Cubemap Probes");
		}
		cubemapProbe.transform.parent = cubemapProbes.transform;
		cubemapProbe.renderer.castShadows = false;
		CubemapProbeScript cubemapProbeScript = cubemapProbe.AddComponent("CubemapProbeScript") as CubemapProbeScript;
		Vector3 boxSize = new Vector3(
			Mathf.Abs(endPoint.position.x - startPoint.position.x),
			Mathf.Abs(endPoint.position.y - startPoint.position.y),
			Mathf.Abs(endPoint.position.z - startPoint.position.z));
		cubemapProbeScript.SetBoxParameters(boxSize, center);
		
		// Render cubemap
		foreach (Renderer rdr in renderersToExclude) {
			rdr.enabled = false;
		}
		cubemapProbe.renderer.enabled = false;
		GameObject cubemapCamera = new GameObject();
		cubemapCamera.AddComponent("Camera");
		cubemapCamera.transform.position = center;
		cubemapCamera.transform.rotation = Quaternion.identity;
		Cubemap cubemap = new Cubemap(cubemapResolution, TextureFormat.RGB24, true);
		cubemapCamera.camera.RenderToCubemap(cubemap);
		AssetDatabase.CreateAsset(cubemap, "Assets/" + cubemapName + ".cubemap");
		DestroyImmediate(cubemapCamera);
		cubemapProbe.renderer.enabled = true;
		foreach (Renderer rdr in renderersToExclude) {
			rdr.enabled = true;
		}
		
		// Create and assign reflective material with rendered cubemap
		Material reflectiveMat = new Material(Shader.Find("Reflective/Diffuse"));
		reflectiveMat.SetTexture("_Cube", cubemap);
		reflectiveMat.SetColor("_Color", Color.black);
		AssetDatabase.CreateAsset(reflectiveMat, "Assets/" + cubemapName + ".mat");
		cubemapProbe.renderer.material = reflectiveMat;
		
		// Assign cubemap and correct size to materials using the parallax-corrected reflective shader
		foreach (Renderer rdr in parallaxCorrectedMaterials) {
			rdr.sharedMaterial.SetTexture("_Cube", cubemap);
			rdr.sharedMaterial.SetVector("_BoxPosition", center);
			rdr.sharedMaterial.SetVector("_BoxSize", boxSize);
		}
    	} else {
    		Debug.Log("Please set the two points to define the bounding box.");
    	}
    }
	
    void OnWizardUpdate () {
        helpString = "Please set the starting point and the ending point\n" +
        	"of the box (they must form an xyz opposite diagonal).";
    }
}
