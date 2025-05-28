using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;

    [SerializeField] private float speed = 1;

    [SerializeField] private Camera playerCamera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = playerCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, playerCamera.nearClipPlane));
            Fire(ray.origin, ray.direction);
        }
    }
    
    private void Fire(Vector3 position, Vector3 forward)
    {
        var bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
        bullet.transform.forward = forward;
        bullet.Fire(speed);
    }
}