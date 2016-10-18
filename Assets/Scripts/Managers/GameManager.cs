using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static string[,,] railData;

    public static void SetRailData(string[,,] railData)
    {
        GameManager.railData = railData;
    }
}
