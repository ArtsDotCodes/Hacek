using UnityEngine;
using System.Collections;

public class RailSpawnManager : MonoBehaviour {

    private GameObject railMakerPrefab;

    public void BeginSpawning()
    {
        railMakerPrefab = (GameObject)Resources.Load(ResourcePaths.TrailMakerPrefabPath);

        Vector3 center = transform.position;
        for (int i = 0; i < GameManager.railData.GetLength(0); i++)
        {
            if(GameManager.railData[i, 0, 0] != null)
            {
                Vector3 pos = RandomCircle(center, 50.0f);
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
                RailHandler rh = ((GameObject)Instantiate(railMakerPrefab, pos, rot)).GetComponent<RailHandler>();
                rh.SetRailIndex(i);
            }
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
