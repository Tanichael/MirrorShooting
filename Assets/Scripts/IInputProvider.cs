using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInputProvider
{
    Vector3 GetRotate();
    Vector3 GetMove();

    bool GetShoot();
}