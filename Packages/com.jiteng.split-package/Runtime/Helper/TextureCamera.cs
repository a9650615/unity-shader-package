using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TextureCamera : MonoBehaviour
{
    public Texture2D texture;
    private Camera camera;
    RenderTexture source;
    private int screenWidth = 0;
    private int screenHeight = 0;
    public Material mMaterial;
    private Shader splitShader;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        gameObject.SetActive(true);
        //mMaterial = Resources.Load("Scripts/Camera/CameraMaterial.mat", typeof(Material)) as Material;
        if (mMaterial)
        {
            Debug.Log("Start");
        }

        if (GraphicsSettings.renderPipelineAsset is UniversalRenderPipelineAsset && mMaterial == null)
        {
            Debug.Log("URP");
            // splitShader = Shader.Find("Custom/Texture Blend");
            SplitData.SetMaterial((new Material(Shader.Find("Custom/Texture Blend"))));
            // SplitData.material = CoreUtils.CreateEngineMaterial(Shader.Find("Custom/Texture Blend"));
            mMaterial = SplitData.material;
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        // RTImage(camera);
        if (screenWidth != Screen.width || screenHeight != Screen.height)
        {
            Object.Destroy(texture);
            Destroy(source);
            source = new RenderTexture(Screen.width, Screen.height, 24);
            texture = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false);
            texture.hideFlags = HideFlags.HideAndDontSave;
            screenWidth = Screen.width;
            screenHeight = Screen.height;
            camera.targetTexture = source;
            SplitData.material.SetTexture("_BlendTex", source);
        }

        camera.Render();
        RenderTexture.active = source;
        texture.Apply(false);
        texture.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
        texture.Apply();
        // camera.targetTexture = null;
        Graphics.CopyTexture(source, texture);
        RenderTexture.active = null;

    }

    // SRP usage
    Texture2D RTImage(Camera camera)
    {
        if (screenWidth != Screen.width || screenHeight != Screen.height)
        {
            Object.Destroy(texture);
            Destroy(source);
            source = new RenderTexture(Screen.width, Screen.height, 24);
            texture = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false);
            texture.hideFlags = HideFlags.HideAndDontSave;
            screenWidth = Screen.width;
            screenHeight = Screen.height;
        }

        camera.targetTexture = source;
        camera.Render();
        RenderTexture.active = source;
        texture.Apply(false);
        texture.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
        texture.Apply();
        // camera.targetTexture = null;
        Graphics.CopyTexture(source, texture);
        RenderTexture.active = null;
        return texture;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (screenWidth != Screen.width || screenHeight != Screen.height)
        {
            Object.Destroy(texture);
            texture = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false);
            texture.hideFlags = HideFlags.HideAndDontSave;
            screenWidth = Screen.width;
            screenHeight = Screen.height;
        }
        Graphics.CopyTexture(source, texture);
        Graphics.Blit(source, destination);
    }

    // private void OnRenderImage(RenderTexture source, RenderTexture destination)
    // {
    //     Object.Destroy(texture);
    //     texture = new Texture2D(source.width, source.height, TextureFormat.RGBA32, true);
    //     texture.hideFlags = HideFlags.HideAndDontSave;
    //     RenderTexture.active = source;
    //     texture.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
    //     texture.Apply();
    //     // texture = tex;
    //      byte[] _bytes = texture.EncodeToPNG();
    //              System.IO.File.WriteAllBytes(Application.dataPath + "./screen.png", _bytes);
    //     Graphics.Blit(source, destination);
    // }
}
