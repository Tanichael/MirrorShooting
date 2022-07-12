using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シーンを超えて共有したいプレイヤー情報
public class PlayerInfo : Singleton<PlayerInfo>
{
    public string PlayerName { get; private set; }

    public void SetName(string name)
    {
        PlayerName = name;
    }
}
