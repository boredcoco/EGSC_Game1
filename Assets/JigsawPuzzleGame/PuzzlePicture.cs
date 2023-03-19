using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePicture : MonoBehaviour
{
    private string key = "puzzlePicture"; // key used to access PlayerPrefs

    private string defaultImageFilePath = "Puzzlepics/mbs";

    private void Start()
    {
        Sprite sprite = Resources.Load<Sprite>(PlayerPrefs.GetString(key, defaultImageFilePath));
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
