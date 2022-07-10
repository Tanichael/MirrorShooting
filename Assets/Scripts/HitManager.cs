using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

//衝突の管理をしようとしたが、現状RigidBody任せ
public class HitManager : MonoBehaviour
{
    private bool _isHit = false;

    public bool IsHit
    {
        get
        {
            return _isHit;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // if (Physics.SphereCast(ray, 0.5f, out hit, 0.5f))
        // {
        //     if (hit.collider != null)
        //     {
        //         Debug.Log(hit.collider.gameObject.name);
        //         _isHit = true;
        //     }
        // }
        // else
        // {
        //     _isHit = false;
        // }
    }
}
