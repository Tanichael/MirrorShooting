using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UniRx;

//基本サーバーで動かしたい処理をまとめておく
//共通のイベント処理など
public class GameManager : NetworkBehaviour
{
    //サーバーで動かしたい処理を登録しておく
    public override void OnStartServer()
    {
        //衝突時の処理
        Container.Instance.OnBulletHit.Subscribe(bulletHitMessage =>
        {
            //HP処理
            
            //プレーヤー取得処理
            //MazePlayerを取得してHitPointを減らす処理が必要
            GameObject playerObject = bulletHitMessage.ShotIdentity.gameObject;
            MazePlayer mazePlayer = playerObject.GetComponent<MazePlayer>();
            if (mazePlayer != null)
            {
                mazePlayer.HitPoint -= bulletHitMessage.Damage;
                Debug.Log("Id: " + bulletHitMessage.ShotIdentity.connectionToClient.connectionId +  ", HitPoint: " + mazePlayer.HitPoint);
            }
            
            //UI表示処理 誰が誰を倒した、みたいなの
            
            Debug.Log("Shooter: " + bulletHitMessage.ShooterIdentity);
            Debug.Log("Shot: " + bulletHitMessage.ShotIdentity);
            Debug.Log("Damage: " + bulletHitMessage.Damage);
        });
    }
}
