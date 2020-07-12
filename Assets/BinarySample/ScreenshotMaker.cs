using System.IO;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenshotMaker : MonoBehaviour
{
    private Camera _cam;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    public byte[] MakePhoto()
    {
        var width = Screenshot.Width;
        var height = Screenshot.Height;
        var photo = new Texture2D(width, height, TextureFormat.RGBA32, 
            false);
        RenderTexture rt = new RenderTexture(width, height, 24,
            RenderTextureFormat.ARGB32);
        _cam.targetTexture = rt;
        _cam.Render();
        RenderTexture.active = _cam.targetTexture;
        photo.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        _cam.targetTexture = null;
        RenderTexture.active = null;
        DestroyImmediate(rt);
        photo.Apply();
        var bytes = photo.EncodeToJPG(100);
        Destroy(photo);
        return bytes;
    }
}
