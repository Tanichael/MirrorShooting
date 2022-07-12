using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BulletShootMessage
{
    public NetworkIdentity ShooterIdentity { get; }
    public Vector3 ShootPosition { get; }
    public Vector3 ShootDirection { get; }

    public BulletShootMessage(NetworkIdentity shooterIdentity, Vector3 shootPosition,Vector3 shootDirection)
    {
        ShooterIdentity = shooterIdentity;
        ShootPosition = shootPosition;
        ShootDirection = shootDirection;
    }
}

public class BulletHitMessage
{
    public NetworkIdentity ShooterIdentity { get; }
    public NetworkIdentity ShotIdentity { get;  }
    public float Damage { get; }

    public BulletHitMessage(NetworkIdentity shooterIdentity, NetworkIdentity shotIdentity, int damage)
    {
        ShooterIdentity = shooterIdentity;
        ShotIdentity = shotIdentity;
        Damage = damage;
    }
}
