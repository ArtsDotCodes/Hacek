using UnityEngine;
using System.Collections.Generic;

public class DataReader : MonoBehaviour {

    private TextAsset data;
    private string[] lines;
    private string[,,] railData = new string[1062,1220,5]; //size is hard-coded due to time limitations

	void Start () {
        //Load the data file
        data = (TextAsset)Resources.Load(ResourcePaths.FormattedDataPath);

        //Separate all the lines by the newline character
        lines = data.ToString().Split('\n');

        //Parse the data file and store all info into an array
        foreach (string line in lines)
        {
            string[] splitLine = line.Split(',');
            if (railData[int.Parse(splitLine[0]),0,0] == null)
            {
                for (int i = 0; i < splitLine.Length; i++)
                {
                    railData[int.Parse(splitLine[0]), 0, i] = splitLine[i];
                }
            }
            else if (railData[int.Parse(splitLine[0]), 0, 0] == splitLine[0])
            {
                //inefficient
                int i = 1;
                while (railData[int.Parse(splitLine[0]), i, 0] != null)
                {
                    ++i;
                }

                for (int j = 0; j < splitLine.Length; j++)
                {
                    railData[int.Parse(splitLine[0]), i, j] = splitLine[j];
                }
            }
        }

        //Pass all the data to the Game Manager
        GameManager.SetRailData(railData);
    }
}
