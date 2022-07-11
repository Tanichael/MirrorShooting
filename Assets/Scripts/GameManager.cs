using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UniRx;

public class GameManager : NetworkBehaviour
{
    public override void OnStartServer()
    {
        Container.Instance.OnBulletHit.Subscribe(bulletHitMessage =>
        {
            Debug.Log("Shooter: " + bulletHitMessage.Shooter);
            Debug.Log("Shot: " + bulletHitMessage.Shot);
            Debug.Log("Damage: " + bulletHitMessage.Damage);
        });
    }
}
