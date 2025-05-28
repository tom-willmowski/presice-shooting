using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody physics;

    public void Fire(float speed)
    {
        physics.velocity = transform.forward * speed;
        Destroy(gameObject, 5);
    }
}