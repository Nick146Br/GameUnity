using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject Plane_1;
    void OnTriggerEnter(Collider col) {
        Debug.Log("Pressed");
        Plane_1.transform.position += new Vector3(0,4,0);
    }
}
