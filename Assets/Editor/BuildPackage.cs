using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TeshShader : MonoBehaviour
{
    [MenuItem("打包工具 / Build")]
    static void BuildAssetBundle()
    {
        var path = "Assets/Shaders/SplitBlend.shader";
        AssetBundleBuild build = new AssetBundleBuild();
        build.assetBundleName = "asset_data";
        build.assetNames = new string[1];
        build.assetNames[0] = path;
        AssetBundleBuild[] builds = new AssetBundleBuild[1];
        builds[0] = build;
        Debug.Log(Application.dataPath);
        var assetBundleManifest = BuildPipeline.BuildAssetBundles(Application.dataPath + "/../Packages/com.jiteng.split-package/Assets", builds,
        BuildAssetBundleOptions.ChunkBasedCompression
        | BuildAssetBundleOptions.DeterministicAssetBundle, BuildTarget.StandaloneWindows);
    }
}