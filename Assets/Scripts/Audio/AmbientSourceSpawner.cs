using UnityEngine;
using System.Collections;

public class AmbientSourceSpawner : MonoBehaviour {

    [SerializeField] private GameObject source, ambienceContainer;
    [SerializeField] private AudioClip[] scarseSources;
    [SerializeField] private AudioClip[] mediumSources;
    [SerializeField] private AudioClip[] denseSources;

    void Start () {
        ambienceContainer = GameObject.Find("Ambience");

        GameObject go = null;
        for (int i=0; i<42; i++)
        {
            go = (GameObject)Instantiate(source, DistributeCircle(Vector3.zero, 78, i), transform.rotation);
            go.transform.position += Vector3.up;
            go.transform.parent = ambienceContainer.transform;
            go.GetComponent<AudioSource>().clip = scarseSources[Random.Range(0, scarseSources.Length)];
            go.GetComponent<AudioSource>().Play();

            go = (GameObject)Instantiate(source, DistributeCircle(Vector3.zero, 62, i), transform.rotation);
            go.transform.position += Vector3.up * 5.0f;
            go.transform.parent = ambienceContainer.transform;
            go.GetComponent<AudioSource>().clip = mediumSources[Random.Range(0, mediumSources.Length)];
            go.GetComponent<AudioSource>().Play();

            go = (GameObject)Instantiate(source, DistributeCircle(Vector3.zero, 50, i), transform.rotation);
            go.transform.position += Vector3.up * 15.0f;
            go.transform.parent = ambienceContainer.transform;
            go.GetComponent<AudioSource>().clip = denseSources[Random.Range(0, denseSources.Length)];
            go.GetComponent<AudioSource>().Play();
        }
	}

    private Vector3 DistributeCircle(Vector3 center, float radius, int index)
    {
        float ang = index / 42.0f * 360.0f;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}
