using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

//イベントの定義 Singletonとして動く
public class Container : Singleton<Container>
{
    private Subject<BulletHitMessage> _bulletHitMessageSubject;

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
        _bulletHitMessageSubject = new Subject<BulletHitMessage>();
        
    }
}
