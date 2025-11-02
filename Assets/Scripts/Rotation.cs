using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;

    void Update()
    {
        Debug.DrawRay(transform.position, transform.up * 2, Color.green);

        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }
}
