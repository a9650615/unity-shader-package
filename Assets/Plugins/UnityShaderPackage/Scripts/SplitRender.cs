using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Experiemntal.Rendering.Universal
{

    public class SplitRender : ScriptableRendererFeature
    {
        [System.Serializable]
        public class BlitSettings
        {
            private RenderPassEvent Event = RenderPassEvent.BeforeRenderingPostProcessing;

            public Material blitMaterial = null;
        }

        public BlitSettings settings = new BlitSettings();

        SplitRenderPass m_ScriptablePass;

        public override void Create()
        {
            if (settings.blitMaterial == null)
            {
                settings.blitMaterial = SplitData.material;
            }

            Debug.Log(settings.blitMaterial);

            m_ScriptablePass = new SplitRenderPass(settings.blitMaterial, -1, name);

            // Configures where the render pass should be injected.
            // m_ScriptablePass.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
        }

        // Here you can inject one or multiple render passes in the renderer.
        // This method is called when setting up the renderer once per-camera.
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            var src = renderer.cameraColorTarget;
            var dest = RenderTargetHandle.CameraTarget;

            if (settings.blitMaterial == null)
            {
                Debug.LogWarningFormat("Missing Blit Material. {0} blit pass will not execute. Check for missing reference in the assigned renderer.", GetType().Name);
                return;
            }

            m_ScriptablePass.Setup(src, dest);
            renderer.EnqueuePass(m_ScriptablePass);
        }
    }
}
