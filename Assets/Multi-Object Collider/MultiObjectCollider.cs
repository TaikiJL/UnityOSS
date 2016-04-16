using UnityEngine;

public enum ColliderType { Box, Sphere, Capsule, Mesh, None };

public sealed class MultiObjectCollider
{

    private MultiObjectCollider() { }

    /// <summary>
    /// Creates a single collider that wraps around the <param name="rootGameObject">GameObject</param> and its children.
    /// </summary>
    /// <param name="rootGameObject">Root GameObject containing the children to wrap.</param>
    /// <param name="colliderType">Type of the collider to wrap the GameObjects.</param>
    /// <param name="removeExistingColliders">Remove or keep the existing colliders?</param>
    public static void CreateCollider(GameObject rootGameObject, ColliderType colliderType, bool removeExistingColliders = true)
    {
        if (removeExistingColliders)
        {
            Collider[] colliders = rootGameObject.GetComponentsInChildren<Collider>();

            foreach (Collider currentCollider in colliders)
                Object.DestroyImmediate(currentCollider);
        }

        if (colliderType == ColliderType.None)
            return;

        // Checks if the object has any child
        if (rootGameObject.GetComponentsInChildren<Transform>().Length > 1)
        {
            // Gets and saves the object's initial Transform and initializes it.
            // This is done to have a correct Transform for the combined mesh.
            Transform objectTransform = rootGameObject.transform;
            Vector3 initialPosition = objectTransform.position;
            Quaternion initialRotation = objectTransform.rotation;
            objectTransform.position = Vector3.zero;
            objectTransform.rotation = Quaternion.identity;

            // Combine the meshes
            MeshFilter[] meshFilters = rootGameObject.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];
            for (int i = 0; i < meshFilters.Length; i++)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            }

            // If the root object has already a mesh filter, stores it to give it back later
            MeshFilter tempMeshFilter;
            Mesh intialMesh;
            if (tempMeshFilter = rootGameObject.GetComponent<MeshFilter>())
            {
                intialMesh = tempMeshFilter.sharedMesh;
            }
            else
            {
                intialMesh = null;
                tempMeshFilter = rootGameObject.AddComponent<MeshFilter>() as MeshFilter;
            }
            tempMeshFilter.sharedMesh = new Mesh();
            tempMeshFilter.sharedMesh.CombineMeshes(combine);

            AddCollider(rootGameObject, colliderType);

            Object.DestroyImmediate(tempMeshFilter);
            if (intialMesh != null)
            {
                MeshFilter meshFilter = rootGameObject.AddComponent<MeshFilter>() as MeshFilter;
                meshFilter.mesh = intialMesh;
            }

            objectTransform.position = initialPosition;
            objectTransform.rotation = initialRotation;
        }
        else
        {
            AddCollider(rootGameObject, colliderType);
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
