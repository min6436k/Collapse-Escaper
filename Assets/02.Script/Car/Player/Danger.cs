using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Danger : MonoBehaviour
{
    public Transform Target;

    void FixedUpdate()
    {
        transform.LookAt(Target);
    }
}
