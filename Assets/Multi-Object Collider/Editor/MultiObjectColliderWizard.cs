using UnityEngine;
using UnityEditor;

public class MultiObjectColliderWizard : ScriptableWizard
{
	
	[SerializeField]
    private ColliderType m_ColliderType = ColliderType.Box;
	[SerializeField]
    private bool m_RemoveExistingColliders = true;
	
	[MenuItem ("CustomTools/Multi-object Collider &c")]
	static void CreateWizard()
    {
		DisplayWizard<MultiObjectColliderWizard>("Create a Collider", "Create");
	}
	
	void OnWizardCreate()
    {
        foreach (var gameObject in Selection.gameObjects)
        {
            MultiObjectCollider.CreateCollider(gameObject, m_ColliderType, m_RemoveExistingColliders);
        }
		
		CreateWizard();
	}
	
	void OnWizardUpdate()
    {
        helpString = "Adds a collider to the selected GameObjects. The collider will wrap the GameObjects' children.";
	}

}
