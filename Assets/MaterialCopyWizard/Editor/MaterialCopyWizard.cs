using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class MaterialCopyWizard : ScriptableWizard {

	public Material sourceMaterial;
	public Material targetMaterial;

	[MenuItem ("CustomTools/Copy Material")]
	static void CreateWizard () {
		ScriptableWizard.DisplayWizard<MaterialCopyWizard>("Copy Material", "Copy");
	}
	
	void OnWizardCreate () {
		CreateWizard();
		targetMaterial.shader = sourceMaterial.shader;
		targetMaterial.CopyPropertiesFromMaterial(sourceMaterial);
	}
	
	void OnWizardUpdate () {
		helpString = "Copy a material to another";
	}
}
