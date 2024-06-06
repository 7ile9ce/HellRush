using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platform;
    public Transform generationPoint;
    public float distanceBetween;
    float platformWidth;
    public float distanceMin;
    public float distanceMax;

    //public GameObject[] platforms;
    int platformSelector;
    float[] platformsWidth;

    public PlatformManager[] platfromsM;

    void Start()
    {
        platformWidth = platform.GetComponent<BoxCollider2D>().size.x;

        platformsWidth = new float[platfromsM.Length];

        for (int i = 0; i < platfromsM.Length; i++)
        {
            platformsWidth[i] = platfromsM[i].platform.GetComponent<BoxCollider2D>().size.x;
        }
    }

    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceMin, distanceMax);

            platformSelector = Random.Range(0,platfromsM.Length);

            transform.position = new Vector3(transform.position.x + platformsWidth[platformSelector] + distanceBetween, transform.position.y, transform.position.z);

            //Instantiate(/*platform*/ platforms[platformSelector],transform.position, transform.rotation);
            GameObject newPlatform =platfromsM[platformSelector].GetPlatform();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);
        }
    }

}
