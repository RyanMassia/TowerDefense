using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class WayPointManager : MonoBehaviour
{
    //1 
    public static WayPointManager Instance; // makes an instance of this script
    //2   
    public List<Path> Paths = new List<Path>();  // story list of all the paths 
    void Awake()
    {
        //3     make ths script the Instance of Waypoint Manger
        Instance = this;
    }
    //4  returns postion of enemy spawn depends on what path they will take 
    public Vector3 GetSpawnPosition(int pathIndex)
    {
        return Paths[pathIndex].WayPoints[0].position;
    }
}
//5 stores list of waypoints that can be edited in Unity 
[System.Serializable]
public class Path
{
    public List<Transform> WayPoints = new List<Transform>();
}