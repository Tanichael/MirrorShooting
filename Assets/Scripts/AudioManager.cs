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
            RpcShootAudio();
        });

        Container.Instance.OnBulletHit.Subscribe(bulletHitMessage =>
        {
            RpcHitAudio(bulletHitMessage.ShooterIdentity.connectionToClient);
        });
    }

    [ClientRpc]
    public void RpcShootAudio()
    {
        _shootBulletAudio.Play();
    }

    [TargetRpc]
    public void RpcHitAudio(NetworkConnection target)
    {
        _hitBulletAudio.Play();
    }
}
