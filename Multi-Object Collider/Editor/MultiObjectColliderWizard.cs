using UnityEngine;
using UnityEditor;
using System.Collections;

public class MultiObjectColliderWizard : ScriptableWizard {
	
	[SerializeField] public ColliderType _colliderType = ColliderType.Box;
	[SerializeField] public bool _removeExistingColliders = true;
	
	[MenuItem ("CustomTools/Multi-object Collider &c")]
	static void CreateWizard () {
		ScriptableWizard.DisplayWizard<MultiObjectColliderWizard>("Create a Collider", "Create");
	}
	
	void OnWizardCreate () {
		MultiObjectCollider.CreateCollider(Selection.gameObjects, _colliderType, _removeExistingColliders);
		
		CreateWizard();
	}
	
	void OnWizardUpdate () {
		helpString = "Add a collider to your object." +
			"If your object is composed of multiple GameObject children," +
				"the collider parameters will take into account their meshes.";
	}
}
