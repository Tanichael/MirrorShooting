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
    private int _shooter;

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

    public int Shooter
    {
        get => _shooter;
        set => _shooter = value;
    }
    
    public void Shoot(int connId, Vector3 shootDirection)
    {
        //シューターの設定
        _shooter = connId;
        
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
            int shot = mazePlayer.connectionToClient.connectionId;
            if (shot == _shooter) return; //自爆はしない
            
            Container.Instance.BulletHitPublisher.OnNext(new BulletHitMessage(
                _shooter,
                mazePlayer.connectionToClient.connectionId,
                10
            ));
            Debug.Log(mazePlayer.gameObject.name);
        }
    }
}

public class BulletHitMessage
{
    public int Shooter;
    public int Shot;
    public float Damage;

    public BulletHitMessage(int shooter, int shot, int damage)
    {
        Shooter = shooter;
        Shot = shot;
        Damage = damage;
    }
}
