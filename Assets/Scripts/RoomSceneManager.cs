using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class RoomSceneManager : MonoBehaviour
{
    [SerializeField] private NetworkManager _networkManager;
    [SerializeField] private Button _createButton;
    [SerializeField] private Button _joinButton;

    public void Start()
    {   
        _createButton.onClick.AddListener(() =>
        {
            _networkManager.StartHost();
        });
        
        _joinButton.onClick.AddListener(() =>
        {
            
        });
    }
}
