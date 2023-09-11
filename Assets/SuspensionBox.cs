using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspensionBox : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (collision != GetComponentInParent<BallHolder>().BallList[GetComponentInParent<BallHolder>().BallIndex])
            {
                collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        }
    }

}
