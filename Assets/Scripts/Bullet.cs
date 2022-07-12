using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Mirror;
using Mirror.Examples.AdditiveScenes;
using UniRx;
using UniRx.Triggers;

public class Bullet : NetworkBehaviour
{
    private NetworkIdentity _shooterIdentity;

    private readonly float _lifeTime = 10f; //球の寿命

    private Subject<BulletHitMessage> _bulletHit;

    public IObservable<BulletHitMessage> OnBulletHit
    {
        get
        {
            return _bulletHit;
        }
    }
    
    private readonly float _speed = 10f;

    public void Shoot(NetworkIdentity playerIdentity, Vector3 shootDirection)
    {
        //シューターの設定
        _shooterIdentity = playerIdentity;
        Debug.Log("shooter: " + _shooterIdentity.connectionToClient.connectionId);
        
        //寿命の設定
        Destroy(gameObject, _lifeTime);

        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                gameObject.transform.position += shootDirection * _speed * Time.deltaTime;
            });
    }

    [ServerCallback]
    public void OnTriggerEnter(Collider other)
    {
        MazePlayer mazePlayer = other.gameObject.GetComponent<MazePlayer>();
        if (mazePlayer != null)
        {
            NetworkIdentity shotIdentity = mazePlayer.netIdentity;
            if (shotIdentity == _shooterIdentity) return; //自爆はしない
            
            // Container.Instance.BulletHitPublisher.OnNext(new BulletHitMessage(
            //     _shooter,
            //     mazePlayer.connectionToClient.connectionId,
            //     10
            // ));
            // Debug.Log(mazePlayer.gameObject.name);
        }

        Container.Instance.BulletHitPublisher.OnNext(new BulletHitMessage(
            _shooterIdentity,
            _shooterIdentity,
            10
        ));
    }
}

public class BulletHitMessage
{
    public NetworkIdentity ShooterIdentity;
    public NetworkIdentity ShotIdentity;
    public float Damage;

    public BulletHitMessage(NetworkIdentity shooterIdentity, NetworkIdentity shotIdentity, int damage)
    {
        ShooterIdentity = shooterIdentity;
        ShotIdentity = shotIdentity;
        Damage = damage;
    }
}
