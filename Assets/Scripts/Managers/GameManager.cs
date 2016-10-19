using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static string[,,] railData;
    public static float[] startTimes;
    public static float timeScaleFactor = 10.0f;

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
