using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class NewBehaviourScript : MonoBehaviour
{
    public GameObject prefab;
    private GameObject pro;
    [Button]
    public void Instac()
    {
        Vector3 position = transform.position + transform.forward;
        print(transform.rotation);
        Quaternion ro = prefab.transform.rotation;
        ro.y = transform.rotation.y;
        print(ro);
        GameObject projectile = GameObject.Instantiate(prefab, position,ro);

        pro = projectile;
    }
    [Button]
    public void Clean()
    {
        DestroyImmediate(pro);
    }
}
