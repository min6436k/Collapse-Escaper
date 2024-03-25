using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayGizmo : MonoBehaviour
{
    public Transform WayPoints;
    private void OnDrawGizmos()
    {
        foreach (Transform t in WayPoints)
        {
        Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(t.position,10);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(t.position,20);
        }
    }
}









