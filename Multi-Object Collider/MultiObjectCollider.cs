using UnityEngine;
using System.Collections;

public enum ColliderType { Box, Sphere, Capsule, Mesh, None };

public class MultiObjectCollider : MonoBehaviour {
	
	public static void CreateCollider (GameObject[] gameObjects, ColliderType colliderType, bool removeExistingColliders = true) {
		foreach (GameObject selectedObject in gameObjects) {
			if (removeExistingColliders) {
				Collider[] colliders = selectedObject.GetComponentsInChildren<Collider>();
				
				foreach (Collider currentCollider in colliders)
					DestroyImmediate(currentCollider);
			}
			
			if (colliderType == ColliderType.None)
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
					tempMeshFilter = selectedObject.AddComponent<MeshFilter>() as MeshFilter;
				}
				tempMeshFilter.sharedMesh = new Mesh();
				tempMeshFilter.sharedMesh.CombineMeshes(combine);
				
				AddCollider(selectedObject, colliderType);
				
				DestroyImmediate(tempMeshFilter);
				if (intialMesh != null) {
					MeshFilter meshFilter = selectedObject.AddComponent<MeshFilter>() as MeshFilter;
					meshFilter.mesh = intialMesh;
				}
				
				objectTransform.position = initialPosition;
				objectTransform.rotation = initialRotation;
			} else {
				AddCollider(selectedObject, colliderType);
			}
		}
	}
	
	private static void AddCollider(GameObject selectedObject, ColliderType colliderType)
	{
		switch (colliderType)
		{
		case ColliderType.Box:
			selectedObject.AddComponent<BoxCollider>();
			break;
		case ColliderType.Sphere:
			selectedObject.AddComponent<SphereCollider>();
			break;
		case ColliderType.Capsule:
			selectedObject.AddComponent<CapsuleCollider>();
			break;
		case ColliderType.Mesh:
			selectedObject.AddComponent<MeshCollider>();
			break;
		default:
			break;
		}
	}
	
}
