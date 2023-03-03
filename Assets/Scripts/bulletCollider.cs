using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollider : MonoBehaviour
{
    void OnCollisionEnter(Collision info)
    {
        if (info.gameObject.name == "Target")
        Destroy(this.gameObject);
    }
}
