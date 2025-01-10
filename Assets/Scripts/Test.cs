using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform test;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //transform.forward = new Vector3(0, 0, 1);
        //transform.right = new Vector3(1, 0, 0);
        //transform.up = new Vector3(0, -1, 0);
        //transform.forward = new Vector3(0, 0, 1);
    }

    private void Update()
    {

        //transform.rotation = Quaternion.LookRotation(test.forward, test.up);
        //Vector3 newDirection = test.forward + test.up + test.right;
        //newDirection.Normalize();

        transform.right = test.right;
    }
}
