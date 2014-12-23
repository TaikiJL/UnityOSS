using UnityEngine;
using System.Collections;

public enum ColliderType { box, sphere, capsule, mesh, none };

public class MultiObjectCollider : MonoBehaviour {

	public static void CreateCollider (GameObject[] gameObjects, ColliderType colliderType, bool removeExistingColliders = true) {
		string selectedColliderType = "";
		
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
		case ColliderType.none:
			selectedColliderType = "None";
			break;
		default:
			break;
		}
		
		foreach (GameObject selectedObject in gameObjects) {
			if (removeExistingColliders) {
				Collider[] colliders = selectedObject.GetComponentsInChildren<Collider>();
				
				foreach (Collider currentCollider in colliders)
					DestroyImmediate(currentCollider);
			}
			
			if (selectedColliderType == "None")
				return;
			
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
}
