using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement player;

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            GameManager.Instance.GameOver();
        }
    }
}