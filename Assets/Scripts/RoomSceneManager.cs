using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Steamworks;

public class RoomSceneManager : MonoBehaviour
{
    [SerializeField] private Button _createButton;
    [SerializeField] private Button _joinButton;

    public void Start()
    {   
        _createButton.onClick.AddListener(() =>
        {
            NetworkManager.singleton.StartHost();
        });
        
        _joinButton.onClick.AddListener(() =>
        {
            
        });

        //app_id.txtのidを入れておく
        try
        {
            bool flag = SteamAPI.RestartAppIfNecessary(new AppId_t(480));
            if (flag)
            {
                Application.Quit();
                return;
               
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
        
        bool isInitialized = SteamAPI.Init();
        if (isInitialized)
        {
            Debug.Log("SteamAPI initialized");
        }
        
    }
}
