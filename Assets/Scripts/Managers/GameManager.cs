using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static string[,,] railData;
    public static float[] startTimes;
    public static float timeScaleFactor = 1000.0f;
    public static float radiusFromCenter = 100.0f;

    public static void SetRailData(string[,,] railData)
    {
        GameManager.railData = railData;
    }

    public static void SetStartTimes(float[] startTimes)
    {
        GameManager.startTimes = startTimes;
        FindObjectOfType<RailSpawnManager>().BeginSpawning();
    }
}
