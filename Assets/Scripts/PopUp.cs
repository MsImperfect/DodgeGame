using UnityEngine;

public class PopUp : MonoBehaviour
{
    public float riseHeight = 3f;       // How far up the spike rises
    public float riseSpeed = 2f;          // How fast it rises
    public float delayBeforeRise = 3f;    // Optional delay

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool rising = false;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.up * riseHeight;

        // Start rising after delay
        Invoke("StartRising", delayBeforeRise);
    }

    void StartRising()
    {
        rising = true;
    }

    void Update()
    {
        if (rising)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, riseSpeed * Time.deltaTime);
        }
    }
}
