using System.IO;
using UnityEditor;

public class ChinarAssetBundle
{
    [MenuItem("Chinar工具/打包AssetsBundle资源")] //菜单栏添加按钮
    static void BuildAllAssetsBundles()
    {
        string folder = "Assets/Plugins/UnityShaderPackage/AssetBundles";                                                                               //定义文件夹名字
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);                                                  //文件夹不存在，则创建
        BuildPipeline.BuildAssetBundles("Assets/Plugins/UnityShaderPackage/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows); //创建AssetBundle
    }
}