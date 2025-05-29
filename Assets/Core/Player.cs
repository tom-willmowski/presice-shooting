using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float speed = 1;
    
    [SerializeField] private Camera playerCamera;
    [SerializeField] private RenderTexture bulletTexture;
    
    [SerializeField] private RawImage outputImage;

    [SerializeField] private GameObject redPlane, blackPlane;
    
    private Texture2D output;

    private void Start()
    {
        output = new Texture2D(bulletTexture.width, bulletTexture.height, TextureFormat.RGBA32, false, true);
        RenderPipelineManager.endCameraRendering += ReadColor;
    }

    private void OnDestroy()
    {
        RenderPipelineManager.endCameraRendering -= ReadColor;
    }

    private void ReadColor(ScriptableRenderContext arg1, Camera camera)
    {
        if (camera == playerCamera)
        {
            return;
        }
        output.ReadPixels(new Rect(0, 0, 1, 1), 0, 0);
        var color = output.GetPixel(0, 0);
        outputImage.color = color;
        Debug.Log(color);
        if (color == Color.black)
        {
            Destroy(blackPlane);
        }else if (color == Color.red)
        {
            Destroy(redPlane);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = playerCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, playerCamera.nearClipPlane));
            Fire(ray.origin, ray.direction);
        }
        ProcessTexture();
    }

    private void Fire(Vector3 position, Vector3 forward)
    {
        var bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
        bullet.transform.forward = forward;
        bullet.Fire(speed);
    }

    private void ProcessTexture()
    {
        Graphics.CopyTexture(bulletTexture, output);
    }
}