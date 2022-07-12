using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : Singleton<PlayerInfo>
{
    public string PlayerName { get; private set; }

    public void SetName(string name)
    {
        PlayerName = name;
    }
}
