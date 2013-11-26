using UnityEngine;
using System.Collections;

public class BoxProjectedCubemapScript : MonoBehaviour {
	
	[SerializeField]
	private Vector3 boxSize, boxPosition;
	
	void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxPosition, boxSize);
    }
	
	void Awake () {
		gameObject.SetActive(false);
	}
	
	public void SetBoxParameters(Vector3 size, Vector3 position) {
		boxSize = size;
		boxPosition = position;
	}
}