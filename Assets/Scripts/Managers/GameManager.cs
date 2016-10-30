using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static readonly bool debugMode = false;

    public static LinkedList<GameObject> railList;
    public static string[,,] railData;
    public static float[] startTimes;
    public static float timeScaleFactor = 50.0f;
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

    public static LinkedList<GameObject> GetRailList()
    {
        return railList;
    }

    public static void SetRailList(LinkedList<GameObject> railList)
    {
        GameManager.railList = railList;
    }
}
