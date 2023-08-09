using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Obstacle : ScriptableObject
{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Transform transform;
}
