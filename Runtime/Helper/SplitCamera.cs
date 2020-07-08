using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SplitCamera: MonoBehaviour
{
    protected Shader m_Shader;
    protected Material m_Material;
    public TextureCamera sec_Camera;
    // Start is called before the first frame update
    void Start()
    {
        GameObject DeviceInfo = GameObject.Find("DeviceInfo");
        if (DeviceInfo)
        {
            // DeviceInfo.GetComponent<Text>().text = "Device:" + SystemInfo.deviceName + "/" + SystemInfo.deviceModel;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Shader != null && m_Material == null)
        {
            m_Material = new Material(m_Shader);
            m_Material.hideFlags = HideFlags.DontSave;
        }
        else if (m_Shader == null)
        {
           //AssetDatabase.ImportAsset("Packages/com.jiteng.split-package/Runtime/Helper/SplitBlend.shader");
           m_Shader = Shader.Find("Custom/Texture Blend");
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (m_Material != null)
        {

            // m_Material.SetFloat("_SampleDist", m_Dist);
            // m_Material.SetFloat("_SampleStrength", m_Strength);
            // m_Material.SetVector("_Center", new Vector4(m_Center.x, m_Center.y, 0.0f, 0.0f));
            // Texture2D tex = new Texture2D(source.width, source.height, TextureFormat.RGB24, false);
            // RenderTexture.active = source;
            // tex.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
            // tex.Apply();
            // byte[] _bytes = tex.EncodeToPNG();
            //  System.IO.File.WriteAllBytes(Application.dataPath + "./screen.png", _bytes);
            Debug.Log("start");
            m_Material.SetTexture("_BlendTex", sec_Camera.texture);
            Graphics.Blit(source, destination, m_Material);
            // Graphics.Blit(source, destination);
        }
        else
        {
            Debug.Log("fail");
            Graphics.Blit(source, destination);
        }
    }
}
