using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform targit;
    public Vector3 offset = new Vector3(0f, 3f, -10f);
    [Range(0.0f, 1f)] public float smoth = 0.5f;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
transform.position = Vector3.SmoothDamp(transform.position, targit.position + offset, ref velocity, smoth);
    }
   
}
