using UnityEngine;
using UnityEditor;
using System.Collections;

public class ColliderWizard : ScriptableWizard {

	public enum ColliderType { box, sphere, capsule, mesh };
	public ColliderType colliderType = ColliderType.box;

	private string selectedColliderType;

	[MenuItem ("GameObject/Create Collider")]
	static void CreateWizard () {
		ScriptableWizard.DisplayWizard<ColliderWizard>("Create Collider", "Create");
	}

	void OnWizardCreate () {
		switch (colliderType) {
			case ColliderType.box:
				selectedColliderType = "BoxCollider";
				break;
			case ColliderType.sphere:
				selectedColliderType = "SphereCollider";
				break;
			case ColliderType.capsule:
				selectedColliderType = "CapsuleCollider";
				break;
			case ColliderType.mesh:
				selectedColliderType = "MeshCollider";
				break;
			default:
				break;
		}

		foreach (GameObject selectedObject in Selection.gameObjects) {
			// Checks if the object has any child
			if (selectedObject.GetComponentsInChildren<Transform>().Length > 1) {
				// Gets and saves the object's initial Transform and initializes it.
				// This is done to have a correct Transform for the combined mesh.
				Transform objectTransform = selectedObject.transform;
				Vector3 initialPosition = objectTransform.position;
				Quaternion initialRotation = objectTransform.rotation;
				objectTransform.position = Vector3.zero;
				objectTransform.rotation = Quaternion.identity;

				// Combine the meshes
				MeshFilter[] meshFilters = selectedObject.GetComponentsInChildren<MeshFilter>();
				CombineInstance[] combine = new CombineInstance[meshFilters.Length];
				for (int i = 0; i < meshFilters.Length; i++) {
					combine[i].mesh = meshFilters[i].sharedMesh;
					combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
				}

				// If the root object has already a mesh filter, stores it to give it back later
				MeshFilter tempMeshFilter;
				Mesh intialMesh;
				if (tempMeshFilter = selectedObject.GetComponent<MeshFilter>()) {
					intialMesh = tempMeshFilter.sharedMesh;
				} else {
					intialMesh = null;
					tempMeshFilter = selectedObject.AddComponent("MeshFilter") as MeshFilter;
				}
				tempMeshFilter.sharedMesh = new Mesh();
				tempMeshFilter.sharedMesh.CombineMeshes(combine);

				selectedObject.AddComponent(selectedColliderType);

				DestroyImmediate(tempMeshFilter);
				if (intialMesh != null) {
					MeshFilter meshFilter = selectedObject.AddComponent("MeshFilter") as MeshFilter;
					meshFilter.mesh = intialMesh;
				}

				objectTransform.position = initialPosition;
				objectTransform.rotation = initialRotation;
			} else {
				selectedObject.AddComponent(selectedColliderType);
			}
		}
	}
	
	void OnWizardUpdate () {
		helpString = "Add a collider to your object." +
			"If your object is composed of multiple GameObject children," +
			"the collider parameters will take into account their meshes" +
			"to fit around them correctly.";
	}
}
