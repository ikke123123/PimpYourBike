using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomGeneration : MonoBehaviour
{
    public List<GameObject> SegmentPrefabs = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {

    }

    GameObject BuildTrack()
    {
        GameObject level = new GameObject("Level");



        int numberOfSegments = 24;


        for (int i = 0; i < numberOfSegments; i++)
        {
            int LevelElement = Random.Range(0, SegmentPrefabs.Count);
            GameObject firstFloor = Instantiate(SegmentPrefabs[LevelElement], level.transform.position + new Vector3(80f + 80f * i, 0, 0), level.transform.rotation, level.transform);
        }

        return level;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }


            GameObject level = BuildTrack();
            level.transform.parent = transform;
            level.transform.position = transform.position;
            level.transform.rotation = transform.rotation;

        }
    }
}
