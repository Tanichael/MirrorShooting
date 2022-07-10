using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityInputProvider : IInputProvider
{
    public Vector3 GetRotate()
    {
        return new Vector3(0, Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f, 0);
    }
    
    public Vector3 GetMove()
    {
        return new Vector3(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * 4f);
    }

    public bool GetShoot()
    {
        return Input.GetMouseButton(0);
    }
}
