using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UniRx;

public class AudioManager : NetworkBehaviour
{
    [SerializeField] private AudioSource _shootBulletAudio;
    [SerializeField] private AudioSource _hitBulletAudio;

    public override void OnStartServer()
    {
        Container.Instance.OnBulletShoot.Subscribe(bulletShootMessage =>
        {
            _shootBulletAudio.Play();
        });
        
        Container.Instance.OnBulletHit.Subscribe(bulletHitMessage =>
        {
            _hitBulletAudio.Play();
        });
    }
}
