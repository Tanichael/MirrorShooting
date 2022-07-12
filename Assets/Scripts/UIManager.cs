using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UIManager : NetworkBehaviour
{
    [SerializeField] private Text _playerNameText;
    [SerializeField] private Text _hitPointText;

    public override void OnStartServer()
    {
        Container.Instance.OnChangeInfo.Subscribe(changeInfoMessage =>
        {
            RpcChangeUI(changeInfoMessage.PlayerName, changeInfoMessage.HitPoint.ToString());
            // TRpcChangeUI(changeInfoMessage.PlayerIdentity.connectionToClient, changeInfoMessage.PlayerName, changeInfoMessage.HitPoint.ToString());
        });
    }

    [ClientRpc]
    private void RpcChangeUI(string playerName, string hitPoint)
    {
        _playerNameText.text = playerName;
        _hitPointText.text = hitPoint;
    }
    
    [TargetRpc]
    private void TRpcChangeUI(NetworkConnection target, string playerName, string hitPoint)
    {
        _playerNameText.text = playerName;
        _hitPointText.text = hitPoint;
    }
}
