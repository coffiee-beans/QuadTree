using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeDebug : MonoBehaviour
{
    public Gradient rectGradiant;
    public Color rectColor;
    public colorType debugType;

    private void OnDrawGizmos()
    {
        QuadTree qt = GetComponent<QuadTree>();

        if (debugType == colorType.Gradiant)
        {
            Gizmos.color = rectGradiant.Evaluate(GetComponent<QuadTree>().depth * 1f / GetComponent<QuadTree>().maxDepth);
        }
        if (debugType == colorType.SolidColor)
        {
            Gizmos.color = rectColor;
        }
        Gizmos.DrawWireCube(new Vector3(qt.transform.position.x, 0, qt.transform.position.z), new Vector3(qt.length, 0, qt.width));
    }
}

public enum colorType
{ SolidColor, Gradiant }