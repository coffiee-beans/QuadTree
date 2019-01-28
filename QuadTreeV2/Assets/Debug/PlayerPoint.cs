using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoint : MonoBehaviour
{
    public Color pointColor;
    public float pointRadius;

    private void OnDrawGizmos()
    {
        Gizmos.color = pointColor;
        Gizmos.DrawSphere(this.transform.position, pointRadius);
    }

}
