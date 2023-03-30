using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePicture : MonoBehaviour
{
    private string key = "puzzlePicture"; // key used to access PlayerPrefs

    private string defaultImageFilePath = "Puzzlepics/mbs";

    private SpriteRenderer renderer;

    private void Start()
    {
        Sprite sprite = Resources.Load<Sprite>(PlayerPrefs.GetString(key, defaultImageFilePath));
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
    }
}
