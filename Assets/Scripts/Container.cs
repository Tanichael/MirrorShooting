using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UniRx;
using Unity.Collections.LowLevel.Unsafe;

//イベントの定義 Singletonとして動く
public class Container : Singleton<Container>
{
    private Subject<BulletShootMessage> _bulletShootMessageSubject;
    private Subject<BulletHitMessage> _bulletHitMessageSubject;
    
    public IObserver<BulletShootMessage> BulletShootPublisher
    {
        get
        {
            return _bulletShootMessageSubject;
        }
    }

    public IObservable<BulletShootMessage> OnBulletShoot
    {
        get
        {
            return _bulletShootMessageSubject;
        }
    }


    public IObserver<BulletHitMessage> BulletHitPublisher
    {
        get
        {
            return _bulletHitMessageSubject;
        }
    }

    public IObservable<BulletHitMessage> OnBulletHit
    {
        get
        {
            return _bulletHitMessageSubject;
        }
    }

    public Container()
    {
        //コンストラクターで各Subjectのインスタンス作っておく
        _bulletHitMessageSubject = new Subject<BulletHitMessage>();
        _bulletShootMessageSubject = new Subject<BulletShootMessage>();

    }
}
