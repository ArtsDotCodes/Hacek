using UnityEngine;
using System.Collections.Generic;

public class PlayerHitboxHandler : MonoBehaviour {

    private List<GameObject> rails = new List<GameObject>();

    public List<GameObject> GetRails()
    {
        return rails;
    }

	void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rail") && !rails.Contains(other.gameObject))
        {
            rails.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rail"))
        {
            rails.Remove(other.gameObject);
        }
    }
}
