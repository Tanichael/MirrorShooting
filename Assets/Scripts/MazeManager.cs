using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MazeManager : NetworkBehaviour
{
    [SerializeField] private GameObject _wall;
    [SerializeField] private GameObject _walls;
    
    void Start()
    {
        MakeMaze();
    }

    void MakeMaze()
    {
        // for(int i = 0; i < 5; i++)
        // {
        //     GameObject wall = Instantiate(_wall, _walls.transform);
        //     wall.SetActive(true);
        //     wall.transform.localPosition = new Vector3(-13.5f, 0f, -14.5f) + new Vector3(0, 0, 1f * i);
        //     NetworkServer.Spawn(wall);
        // }
    }
    
}
