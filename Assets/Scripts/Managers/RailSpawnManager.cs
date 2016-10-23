using UnityEngine;
using System.Collections;

public class RailSpawnManager : MonoBehaviour {

    private GameObject railMakerPrefab;

    public void BeginSpawning()
    {
        railMakerPrefab = (GameObject)Resources.Load(ResourcePaths.TrailMakerPrefabPath);

        Vector3 center = transform.position;
        for (int i = 165; i < GameManager.railData.GetLength(0)-165; i++)
        {
            if(GameManager.railData[i, 0, 0] != null)
            {
                Vector3 pos = DistributeCircle(center, GameManager.radiusFromCenter, i);
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
                RailHandler rh = ((GameObject)Instantiate(railMakerPrefab, pos, rot)).GetComponent<RailHandler>();
                rh.SetRailIndex(i);
            }
        }
    }

    private Vector3 DistributeCircle(Vector3 center, float radius, int index)
    {
        float ang = index/(1062.0f-320.0f) * 370.0f;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}
