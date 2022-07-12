using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Zenject;
using UniRx;
using UniRx.Triggers;

public class MazePlayer : NetworkBehaviour
{
    [SerializeField] private HitManager _hitManager; //余裕あればZenject
    [SerializeField] private GameObject _launchPosition;
    [SerializeField] private GameObject _bullet;
    
    //サーバーとクライアントで共有しておきたい変数
    [SyncVar] private NetworkIdentity _netIdentity;

    public NetworkIdentity NetIdentity
    {
        get => _netIdentity;
    }

    [SyncVar] private string _playerName;
    public string PlayerName
    {
        get => _playerName;
    }

    [SyncVar] private float _hitPoint = 100f;

    public float HitPoint
    {
        get => _hitPoint;
        set => _hitPoint = value;
    }
   
    //ローカルプレイヤー用の変数 入力受付の際に使う
    //Weaponクラスを用意してもいい
    private IInputProvider _inputProvider;
    private bool _isShooting;
    private float _lapseTime = 0f;
    private readonly float _coolTime = 1f;
    
    public override void OnStartLocalPlayer()
    {
        //InputProviderの設定
        _inputProvider = new UnityInputProvider();

        //Cameraの設定
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 0, 0);
        transform.LookAt(new Vector3(0f, 1f, 0f));
        
        CmdSetUpPlayer();
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        transform.Rotate(_inputProvider.GetRotate());
        
        if (!_hitManager.IsHit)
        {
            transform.Translate(_inputProvider.GetMove());
        }

        if (_inputProvider.GetShoot())
        {
            if (!_isShooting)
            {
                _isShooting = true;
                _lapseTime = 0f;
                CmdShoot();
            }
        }
        
        //クールタイム処理
        if (_isShooting)
        {
            _lapseTime += Time.deltaTime;
            if (_lapseTime >= _coolTime)
            {
                _isShooting = false;
                _lapseTime = 0f;
            }
        }
    }

    [Command]
    void CmdSetUpPlayer()
    {
        //コネクションを記録
        _netIdentity = netIdentity;
        _playerName = PlayerInfo.Instance.PlayerName;
        _hitPoint = 100f;
        
        Container.Instance.ChangeInfoPublisher.OnNext(new ChangeInfoMessage(
                _netIdentity,
                _playerName,
                _hitPoint
            ));

        Debug.Log("NetId: " + netId + ", PlayerName: " + _playerName + ", HitPoint: " + _hitPoint);
    }
    
    [Command]
    void CmdShoot()
    {
        Vector3 shootPosition = _launchPosition.transform.position;
        Vector3 shootDirection = Vector3.Normalize(shootPosition - gameObject.transform.position);
        GameObject bulletObject = Instantiate(_bullet);
        bulletObject.transform.position = shootPosition;
        
        NetworkServer.Spawn(bulletObject);
        Container.Instance.BulletShootPublisher.OnNext(new BulletShootMessage(
                _netIdentity,
                shootPosition,
                shootDirection
            ));
        // RpcShoot();
    }
    
}
