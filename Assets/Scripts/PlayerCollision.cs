using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement;

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(collision.collider.tag == "Obstacle")
        {
            movement.enabled = false;
        }
    }
}
