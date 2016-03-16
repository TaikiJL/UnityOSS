using UnityEngine;
using System.Collections;

public class PrefabCustomer : MonoBehaviour
{

    public PrefabPreOrder order;

    public GameObject reference;

	void Start () {
        order.Instantiate();

        reference = order.instance as GameObject;
	}

}
