using UnityEngine;
using System.Collections;

public class RailSpawnManager : MonoBehaviour {

    private int numObjects = 1062;
    private GameObject railMakerPrefab;

    void Start()
    {
        print(ResourcePaths.TrailMakerPrefabPath);
        railMakerPrefab = (GameObject)Resources.Load(ResourcePaths.TrailMakerPrefabPath);

        print(railMakerPrefab);

        Vector3 center = transform.position;
        for (int i = 0; i < numObjects; i++)
        {
            Vector3 pos = RandomCircle(center, 50.0f);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            Instantiate(railMakerPrefab, pos, rot);
        }
    }

    private Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}
