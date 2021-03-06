﻿using UnityEngine;
using System.Collections;

public class RailHandler : MonoBehaviour {

    private bool markedForDeletion;
    private bool isPlayerRail;
    private int railIndex;
    private float startDelay;
    private float slopeFactor;
    private float speed = 10.0f;
    private float[] durations = new float[5];
    private Vector3[] destinations = new Vector3[5];
    private GameObject waypointMarker;

    private int destinationIndex;

    void Awake()
    {
        waypointMarker = (GameObject)Resources.Load(ResourcePaths.WaypointMarkerPrefabPath);
        slopeFactor = 1.0f;
    }

    void FixedUpdate()
    {
        if(startDelay > 0.0f)
        {
            startDelay -= Time.fixedDeltaTime * GameManager.timeScaleFactor;
        }
        else
        {
            if (!markedForDeletion)
            {
                transform.LookAt(destinations[destinationIndex]);

                Vector3 nextMove = transform.position + ((transform.forward * Time.fixedDeltaTime * speed)) / (slopeFactor * 2.0f);
                if (Vector3.Distance(transform.position, destinations[destinationIndex]) < Vector3.Distance(nextMove, destinations[destinationIndex])
                    && Vector3.Distance(transform.position, destinations[destinationIndex]) < 3.0f)
                {
                    transform.position = destinations[destinationIndex];
                    if (++destinationIndex >= destinations.Length || durations[destinationIndex] == 0.0f)
                    {
                        if (GameManager.debugMode)
                        {
                            startDelay = 9999999.9f;
                        }    
                        else
                        {
                            if(destinationIndex == destinations.Length && isPlayerRail)
                            {
                                PlayerManager.SetReachedEnd(true);
                            }
                            GetComponent<DestroyByTrailTime>().markForDeletion();
                            GetComponent<Rotator>().setSpeed(10.0f);
                            GetComponent<Rotator>().setAcceleration(100.0f);
                            markedForDeletion = true;
                        }
                    }
                    else
                    {
                        slopeFactor = destinations[destinationIndex].y - destinations[destinationIndex-1].y;

                        if (slopeFactor <= 1.0f)
                            slopeFactor = 2.0f;
                    }
                }
                else
                {
                    transform.position = nextMove;
                }
            } 
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
                break;
            else
                durations[int.Parse(GameManager.railData[railIndex, i, 2])] += float.Parse(GameManager.railData[railIndex, i, 1]);
        }

        startDelay = GameManager.startTimes[railIndex];

        SetDestinations();
    }

    private void SetDestinations()
    {
        for(int i=0; i<5; i++)
        {
            destinations[i] = transform.position + (transform.forward * (i+1) * (GameManager.radiusFromCenter/5.0f));
            if (i != 4)
            {
                if (durations[i] < 30.0f)
                    destinations[i].y = durations[i];
                else if (durations[i] < 130.0f)
                    destinations[i].y = durations[i] / 6.0f;
                else if (durations[i] < 250.0f)
                    destinations[i].y = durations[i] / 20.0f;
            }
            else
            {
                destinations[i].y = 0.0f;
            }

            ++destinations[i].y;

            if (GameManager.debugMode)
            {
                GameObject waypoint = (GameObject)Instantiate(waypointMarker, destinations[i], transform.rotation);
                waypoint.transform.parent = GameObject.Find("Waypoints").transform;
            }
        }
    }

    public void SetIsPlayerRail(bool isPlayerRail)
    {
        this.isPlayerRail = isPlayerRail;
    }

    public bool IsPlayerRail()
    {
        return isPlayerRail;
    }

    public bool IsMarkedForDeletion()
    {
        return markedForDeletion;
    }
}
