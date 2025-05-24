using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    public Transform centerPoint;
    public float speed = 50f;

    void Update()
    {
        transform.RotateAround(centerPoint.position, Vector3.up, speed * Time.deltaTime);
    }
}
