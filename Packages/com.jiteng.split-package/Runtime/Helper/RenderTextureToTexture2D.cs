using UnityEngine;

namespace SplitPackage.Helper
{
    public class RenderTextureToTexture2D
    {

        public static TextureFormat Format(RenderTextureFormat rt)
        {
            TextureFormat format;

            switch (rt)
            {
                case RenderTextureFormat.ARGBFloat:
                    format = TextureFormat.RGBAFloat;
                    break;
                case RenderTextureFormat.ARGBHalf:
                    format = TextureFormat.RGBAHalf;
                    break;
                case RenderTextureFormat.ARGBInt:
                    format = TextureFormat.RGBA32;
                    break;
                case RenderTextureFormat.ARGB32:
                    format = TextureFormat.ARGB32;
                    break;
                default:
                    format = TextureFormat.ARGB32;
                    Debug.LogWarning("Unsuported RenderTextureFormat.");
                    break;
            }

            return format;
        }


        public Texture2D Convert(RenderTexture rt)
        {
            return Convert(rt, Format(rt.format));
        }

        static Texture2D Convert(RenderTexture rt, TextureFormat format)
        {
            var tex2d = new Texture2D(rt.width, rt.height, format, false);
            var rect = Rect.MinMaxRect(0f, 0f, tex2d.width, tex2d.height);
            RenderTexture.active = rt;
            tex2d.ReadPixels(rect, 0, 0);
            RenderTexture.active = null;
            return tex2d;
        }
    }
}