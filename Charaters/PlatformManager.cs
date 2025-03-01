using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformManager : MonoBehaviour
{

    public GameObject platform;
    public int platformAmount;

    List<GameObject> platforms;

    
    public void Start()
    {
        platforms = new List<GameObject>();

        for (int i = 0; i < platformAmount; i++)
        {
            GameObject obj = (GameObject) Instantiate(platform);
            obj.SetActive(false);
            platforms.Add(obj);
        }

    }
    public GameObject GetPlatform()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            if (!platforms[i].activeInHierarchy)
            {
                return platforms[i];
            }
        }

        GameObject obj = (GameObject) Instantiate(platform);
        obj.SetActive(false);
        platforms.Add(obj);
        return obj;
    }

}
