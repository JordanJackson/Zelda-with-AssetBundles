using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AssetBundleHandler : Singleton<AssetBundleHandler> 
{
    public string assetBundleVersionFileName;
    public float currentVersion;
    AssetBundle contentVersionBundle;

    AssetBundleVersion bundleMap;

    private IEnumerator Start()
    {
        AssetBundleCreateRequest contentVersionBundleRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "AssetBundles/Windows/" + assetBundleVersionFileName));
        yield return contentVersionBundleRequest;
        Debug.Log("Content version: " + assetBundleVersionFileName + " loaded.");

        contentVersionBundle = contentVersionBundleRequest.assetBundle;

        if (contentVersionBundle.Contains("AssetBundleVersion" + currentVersion.ToString("0.0")))
        {
            Debug.Log("AssetBundleVersion" + currentVersion.ToString("0.0") + " found.");

            bundleMap = contentVersionBundle.LoadAsset<AssetBundleVersion>("AssetBundleVersion" + currentVersion.ToString("0.0"));
            if (bundleMap != null)
            {
                Debug.Log("BundleMap loaded.");

                // iterate through bundles referenced by bundleMap and load them
                for (int i = 0; i < bundleMap.bundles.Count; i++)
                {
                    AssetBundleVersion.AssetBundleEntry entry = bundleMap.bundles[i];
                    Debug.Log("Bundle: " + bundleMap.bundles[i].key.ToString());

                    // pass bundle to specific handler by key
                    switch (entry.key)
                    {
                        case "Character":

                            // iterate through bundle IDs in entry
                            for (int j = 0; j < entry.bundleIds.Count; j++)
                            {
                                AssetBundleCreateRequest cBundleRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "AssetBundles/Windows/" + entry.bundleIds[j]));
                                yield return cBundleRequest;
                                Debug.Log("Character Bundle: " + cBundleRequest.assetBundle.name + " loaded.");
                                CharacterSelectHandler.Instance.AddBundle(cBundleRequest.assetBundle);
                            }
                            break;
                        case "Level":
                            // iterate through bundle IDs in entry
                            for (int j = 0; j < entry.bundleIds.Count; j++)
                            {
                                LevelSelectHandler.Instance.AddBundle(entry.bundleIds[j]);
                            }
                            break;
                        case "Item":
                            Debug.Log("Item Bundle");
                            break;
                    }

                }

                // signal all bundle handlers that bundles are loaded, populate their selection UI
                CharacterSelectHandler.Instance.BundlesLoaded();
                LevelSelectHandler.Instance.BundlesLoaded();
            }
            else
            {
                Debug.LogError("BundleMap failed to load.");
            }
        }
    }

}
