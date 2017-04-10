using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButton : MonoBehaviour
{
    public string levelBundleName;

	public void Selected()
    {
        if (levelBundleName == "Default")
        {
            // signal hide UI
            transform.root.gameObject.SetActive(false);
            SceneStreamManager.Instance.StartDefaultLevel();
            return;
        }
        SceneStreamManager.Instance.AddLevelBundle(levelBundleName);
        transform.root.gameObject.SetActive(false);
    }
}
