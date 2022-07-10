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
    
    private IInputProvider _inputProvider;
    
    //Weaponクラスを用意してもいい
    private bool _isShooting;
    private float _lapseTime = 0f;
    private readonly float _coolTime = 1f;
    private readonly float _hitPoint = 100f;
    
    [SyncVar]
    private int _connId;
    
    public override void OnStartLocalPlayer()
    {
        //InputProviderの設定
        _inputProvider = new UnityInputProvider();

        //Cameraの設定
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 0, 0);
        transform.LookAt(new Vector3(0f, 1f, 0f));
        
        CmdSetUpPlayer();

        Container.Instance.OnBulletHit.Subscribe(bulletHitMessage =>
        {
            Debug.Log("Shooter: " + bulletHitMessage.Shooter);
            Debug.Log("Shot: " + bulletHitMessage.Shot);
            Debug.Log("Damage: " + bulletHitMessage.Damage);
        });
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
        _connId = connectionToClient.connectionId;
        Debug.Log("ID: " + _connId);
    }
    
    [Command]
    void CmdShoot()
    {
        RpcShoot();
    }

    [ClientRpc]
    void RpcShoot()
    {
        GameObject bulletObject = Instantiate(_bullet, _launchPosition.transform);
        bulletObject.transform.localPosition = new Vector3(0f, 0f, 0f);
        NetworkServer.Spawn(bulletObject);
        bulletObject.transform.SetParent(null);
        
        Vector3 shootDirection = Vector3.Normalize(_launchPosition.transform.position - gameObject.transform.position);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.Shoot(_connId, shootDirection);
    }
    
    
}
