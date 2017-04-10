using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectHandler : Singleton<LevelSelectHandler>
{
    public GameObject levelSelectButtonPrefab;
    public GameObject levelSelectScrollView;
    public Vector3 levelButtonOffset;

    public List<string> levelBundles;

    public void AddBundle(string bundle)
    {
        if (levelBundles == null)
        {
            levelBundles = new List<string>();
        }

        levelBundles.Add(bundle);
    }

    public void BundlesLoaded()
    {
        // load default button?
        GameObject defaultButton = (GameObject)Instantiate(levelSelectButtonPrefab, levelSelectScrollView.transform);
        defaultButton.transform.localPosition = levelButtonOffset;
        defaultButton.GetComponent<LevelSelectButton>().levelBundleName = "Default";
        defaultButton.GetComponentInChildren<Text>().text = "Default Level";

        for (int i = 0; i < levelBundles.Count; i++)
        {
            // populate selection list by bundle name
            GameObject button = (GameObject)Instantiate(levelSelectButtonPrefab, levelSelectScrollView.transform);
            button.transform.localPosition = new Vector3(levelButtonOffset.x, levelButtonOffset.y - 40 * (i + 1), levelButtonOffset.z);
            button.GetComponent<LevelSelectButton>().levelBundleName = levelBundles[i];
            button.GetComponentInChildren<Text>().text = levelBundles[i];
        }
    }
}
