using UnityEngine;
using System.Collections;

public class RailHandler : MonoBehaviour {

    private int railIndex;
    private float speed = 10.0f;
    private float[] durations = new float[5];
    private Vector3[] destinations = new Vector3[5];

    private int destinationIndex;

    void FixedUpdate()
    {
        transform.LookAt(destinations[destinationIndex]);
        Vector3 nextMove = transform.position + (transform.forward * Time.fixedDeltaTime * speed);
        if (Vector3.Distance(transform.position, destinations[destinationIndex]) < Vector3.Distance(nextMove, destinations[destinationIndex]))
        {
            transform.position = destinations[destinationIndex];
            if (++destinationIndex >= destinations.Length || durations[destinationIndex] == 0.0f)
                Destroy(gameObject);
        }
        else
        {
            transform.position = nextMove;
        }
    }

    public void SetRailIndex(int railIndex)
    {
        this.railIndex = railIndex;
        CalculateStateDurations();
    }

    private void CalculateStateDurations()
    {
        for(int i=0; i<GameManager.railData.GetLength(1); i++)
        {
            if(GameManager.railData[railIndex, i, 0] == null)
            {
                break;
            }
            else
            {
                durations[int.Parse(GameManager.railData[railIndex, i, 3])] += float.Parse(GameManager.railData[railIndex, i, 2]);
            }
        }

        SetDestinations();
    }

    private void SetDestinations()
    {
        for(int i=0; i<5; i++)
        {
            destinations[i] = transform.position + (transform.forward * i * 10);
            destinations[i].y = durations[i]/10.0f;
        }
    }
}
