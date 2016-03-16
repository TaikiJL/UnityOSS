using UnityEngine;

[System.Serializable]
public class PrefabPreOrder
{

    [SerializeField]
    private Object m_Prefab;

    public Object instance { get; private set; }

    public void Instantiate()
    {
        instance = Object.Instantiate(m_Prefab);
    }

    public void Instantiate(Vector3 position, Quaternion rotation)
    {
        instance = Object.Instantiate(m_Prefab, position, rotation);
    }

}
