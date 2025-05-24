using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bobbing : MonoBehaviour
{
    public float intensity;
    public float offset;
    public bool useInitialOffset;
    // Start is called before the first frame update
    void Start()
    {
        if (useInitialOffset)
        {
            offset = transform.position.y;

        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, ((Mathf.PerlinNoise1D(Time.time) * intensity) + offset), this.transform.position.z);
    }
}
