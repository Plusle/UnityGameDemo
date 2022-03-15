#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class BuildBundle : Editor {

    [MenuItem("Assets/ Build AssetBundle")]
    static void BuildAssetBundle() {
        BuildPipeline.BuildAssetBundles(Application.dataPath + "/Bundle", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
    }
}
#endif