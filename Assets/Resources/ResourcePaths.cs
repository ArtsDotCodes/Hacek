using UnityEngine;
using System.Collections;

/**
 * This class is used to reference path names for resources in the resources folder (gameworld objects, art, etc.)
 * Basically for anything that needs to be dynamically loaded
 */
public class ResourcePaths {
    ////////
    //DATA//
    ////////
    private static readonly string DataPath = "Data/";
    public static readonly string FormattedDataPath = DataPath + "formattedData";

    ///////////
    //PREFABS//
    ///////////
    private static readonly string PrefabPath = "Prefabs/";
    private static readonly string TestPrefabPath = PrefabPath + "Tests/";
    private static readonly string TrailTestPrefabPath = TestPrefabPath + "TrailTest/";
    public static readonly string TrailMakerPrefabPath = TrailTestPrefabPath + "TrailMaker";
}
