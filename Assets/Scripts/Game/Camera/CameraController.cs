using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float yOffset;
    public GameObject followObject;
    void Start()
    {
        transform.position = new Vector3(followObject.transform.position.x, followObject.transform.position.y + yOffset, -10);
    }

    void Update()
    {
        transform.position = new Vector3(followObject.transform.position.x, followObject.transform.position.y + yOffset, -10);
    }
}
