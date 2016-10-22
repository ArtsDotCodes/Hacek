using UnityEngine;
using System.Collections.Generic;

public class DataReader : MonoBehaviour {

    private TextAsset data;
    private string[] lines;
    private string[,,] railData = new string[1057,5,4]; //size is hard-coded due to time limitations
    private float[] startTimes = new float[1069];

    void Start()
    {
        /////////////
        //RAIL DATA//
        /////////////
        //Load the data file
        data = (TextAsset)Resources.Load(ResourcePaths.SortedDataPath);

        //Separate all the lines by the newline character
        lines = data.ToString().Split('\n');

        //Parse the data file and store all info into an array

        int k = 0;
        foreach (string line in lines)
        {
            string[] splitLine = line.Split(',');
            if (railData[int.Parse(splitLine[0]), 0, 0] == null)
            {
                k = 0;
                for (int i = 0; i < splitLine.Length; i++)
                {
                    railData[int.Parse(splitLine[0]), 0, i] = splitLine[i];
                }
            }
            else if (railData[int.Parse(splitLine[0]), 0, 0] == splitLine[0])
            {
                ++k;
                for (int j = 0; j < splitLine.Length; j++)
                {
                    railData[int.Parse(splitLine[0]), k, j] = splitLine[j];
                }
            }
        }

        //Pass all the data to the Game Manager
        GameManager.SetRailData(railData);

        ///////////////
        //START TIMES//
        ///////////////
        /*for (int i=0; i<startTimes.Length; i++)
        {
            startTimes[i] = float.Parse(railData[i, 0, 3]);
        }*/

        //Load the data file
        data = (TextAsset)Resources.Load(ResourcePaths.T0DataPath);

        //Separate all the lines by the newline character
        lines = data.ToString().Split('\n');

        //Parse the data file and store all info into an array
        foreach (string line in lines)
        {
            string[] splitLine = line.Split(',');
            if (startTimes[int.Parse(splitLine[0])] == 0.0f)
                startTimes[int.Parse(splitLine[0])] = float.Parse(splitLine[1].Split(';')[0]);
        }

        //Pass all the data to the Game Manager
        GameManager.SetStartTimes(startTimes);
    }

    //uses old data format, held in case of emergency
    void OldStart()
    {
        /*
        private TextAsset data;
        private string[] lines;
        private string[,,] railData = new string[1062,1220,5]; //size is hard-coded due to time limitations
        private float[] startTimes = new float[1062];
        */

        /////////////
        //RAIL DATA//
        /////////////
        //Load the data file
        data = (TextAsset)Resources.Load(ResourcePaths.FormattedDataPath);

        //Separate all the lines by the newline character
        lines = data.ToString().Split('\n');

        //Parse the data file and store all info into an array

        int k = 0;
        foreach (string line in lines)
        {
            string[] splitLine = line.Split(',');
            if (railData[int.Parse(splitLine[0]), 0, 0] == null)
            {
                k = 0;
                for (int i = 0; i < splitLine.Length; i++)
                {
                    railData[int.Parse(splitLine[0]), 0, i] = splitLine[i];
                }
            }
            else if (railData[int.Parse(splitLine[0]), 0, 0] == splitLine[0])
            {
                ++k;
                for (int j = 0; j < splitLine.Length; j++)
                {
                    railData[int.Parse(splitLine[0]), k, j] = splitLine[j];
                }
            }
        }

        //Pass all the data to the Game Manager
        GameManager.SetRailData(railData);

        ///////////////
        //START TIMES//
        ///////////////
        //Load the data file
        data = (TextAsset)Resources.Load(ResourcePaths.T0DataPath);

        //Separate all the lines by the newline character
        lines = data.ToString().Split('\n');

        //Parse the data file and store all info into an array
        foreach (string line in lines)
        {
            string[] splitLine = line.Split(',');
            if (startTimes[int.Parse(splitLine[0])] == 0.0f)
                startTimes[int.Parse(splitLine[0])] = float.Parse(splitLine[1].Split(';')[0]);
        }

        //Pass all the data to the Game Manager
        GameManager.SetStartTimes(startTimes);
    }
}
