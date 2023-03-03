using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject BulletSpawn;
    public GameObject NeedlePrefab;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            fireGun();
    }

    void fireGun()
    {
        Debug.Log("Fired");
        Instantiate(NeedlePrefab, BulletSpawn.transform);
    }
}
