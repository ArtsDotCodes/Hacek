using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static string[,,] railData;
    public static float timeScaleFactor = 1.0f;

    public static void SetRailData(string[,,] railData)
    {
        GameManager.railData = railData;
    }
}
