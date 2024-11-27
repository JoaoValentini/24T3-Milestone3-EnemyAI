using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUILookAtCamera : MonoBehaviour
{
    Transform camTransform;
    // Start is called before the first frame update
    void Start()
    {
        camTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camTransform);
        transform.rotation = Quaternion.Euler(0,transform.eulerAngles.y,0);
    }
}
