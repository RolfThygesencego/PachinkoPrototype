using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Barrel : MonoBehaviour
{
    public GameObject spawnCube;
    public GameObject target;
    void Update()
    {
        TransformExtensions.LookAt2D(transform, new Vector2(target.transform.position.x, target.transform.position.y));
    }
    public void LookAtTarget()
    {
        
    }
}
