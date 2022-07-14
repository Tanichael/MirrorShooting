using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeathenEngineering.SteamworksIntegration;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Steamworks;
using Zenject.Asteroids;

public class RoomSceneManager : MonoBehaviour
{
    [SerializeField] private Button _createButton;
    [SerializeField] private Button _joinButton;

    public void Start()
    {
        DontDestroyOnLoad(this);
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

        Debug.Log(SteamSettings.Server.autoInitialize);
        SteamSettings.Server.LogOn();
        Debug.Log(SteamSettings.Server.LoggedOn);
        Debug.Log(SteamSettings.Server.serverId.m_SteamID);

    }

    public void Update()
    {
        if (SteamSettings.Server.LoggedOn)
        {
            Debug.Log(SteamSettings.Server.serverId.m_SteamID);
        }
    }
}
