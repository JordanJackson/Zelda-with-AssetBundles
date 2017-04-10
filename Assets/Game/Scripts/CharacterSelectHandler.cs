using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectHandler : Singleton<CharacterSelectHandler>
{
    public GameObject characterSelectButtonPrefab;
    public GameObject characterScrollView;
    public Vector3 characterButtonOffset;
    List<AssetBundle> characterBundles;

    int characterCount = 0;

    public void AddBundle(AssetBundle bundle)
    {
        if (characterBundles == null)
        {
            characterBundles = new List<AssetBundle>();
        }

        characterBundles.Add(bundle);
    }

    public void BundlesLoaded()
    {
        for (int i = 0; i < characterBundles.Count; i++)
        {
            string[] charNames = characterBundles[i].GetAllAssetNames();

            for (int j = 0; j < charNames.Length; j++)
            {
                characterCount += 1;
                GameObject go = characterBundles[i].LoadAsset<GameObject>(charNames[j]);
                GameObject button = Instantiate(characterSelectButtonPrefab, characterScrollView.transform);
                button.transform.localPosition = new Vector3(characterButtonOffset.x, characterButtonOffset.y + (characterCount * -40.0f), characterButtonOffset.z);
                button.GetComponentInChildren<Text>().text = go.name;
                // set position
                LoadAssets la = button.GetComponent<LoadAssets>();
                if (la != null)
                {
                    la.assetBundleName = characterBundles[i].name;
                    la.assetName = go.name;
                }
                PlayerSelectButton psb = button.GetComponent<PlayerSelectButton>();
                if (psb != null)
                {
                    psb.assetBundleName = characterBundles[i].name;
                    psb.assetName = go.name;
                }
            }
        }
    }
}
