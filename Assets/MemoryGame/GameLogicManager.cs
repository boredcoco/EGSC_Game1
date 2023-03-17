using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameLogicManager : MonoBehaviour
{
    [SerializeField] private GameObject[] memoryCards;
    [SerializeField] private GameObject[] pictureCards;
    [SerializeField] private string[] filePaths;

    // Score updates
    [SerializeField] private TMP_Text scoreText;

    //Image pairings
    private Dictionary<string, GameObject> colorPairings = new Dictionary<string, GameObject>();

    //Store and generate pairings
    private Dictionary<string, string> pairings = new Dictionary<string, string>();
    //Store ids of flipped cards
    private HashSet<string> isFlipped = new  HashSet<string>();
    private int totalFlipped = 0;
    // Keep track of paired status of memoryCards
    private HashSet<string> isPaired = new HashSet<string>();

    private void Start()
    {
      // Generate random numbers as pairings
      int[] allPairs = new int[6];
      for (int i = 0; i < 3; i++)
      {
        int firstNum = (int) Random.Range(0f, 10000f);
        int secondNum = (int) Random.Range(0f, 10000f);
        pairings.Add(firstNum.ToString(), secondNum.ToString());
        pairings.Add(secondNum.ToString(), firstNum.ToString());
        allPairs[i * 2] = firstNum;
        allPairs[i * 2 + 1] = secondNum;

        Vector3 scaleChange = new Vector3(-1f, -1f, -1f);

        // Set the first sprite image
        Sprite firstSprite = Resources.Load<Sprite>(filePaths[i]);
        pictureCards[i * 2].GetComponent<SpriteRenderer>().sprite = firstSprite;
        pictureCards[i * 2].transform.localScale += scaleChange;

        // Set the second sprite Image
        Sprite secondSprite = Resources.Load<Sprite>(filePaths[i]);
        pictureCards[i * 2 + 1].GetComponent<SpriteRenderer>().sprite = secondSprite;
        pictureCards[i * 2 + 1].transform.localScale += scaleChange;

        colorPairings.Add(firstNum.ToString(), pictureCards[i * 2]);
        colorPairings.Add(secondNum.ToString(), pictureCards[i * 2 + 1]);
      }
      System.Array.Sort(allPairs);
      // Set the unique nums to the memoryCards
      for (int i = 0; i < 6; i++)
      {
        string id = allPairs[i].ToString();
        GameObject obj = colorPairings[id];
        memoryCards[i].GetComponent<MemoryCard>().Initialize(id, obj);
      }

      // Set score to zero initially
      UpdateScore();
    }

    private void UpdateScore()
    {
      int newScore =  (int) Mathf.Floor(isPaired.Count / 2f);
      scoreText.text = "Score: " + newScore.ToString() + "/3";
    }

    // check if flip was successful
    public bool FlipCard(string cardId)
    {
      // if there are 2 cards open, or a card already paired, return
      if (totalFlipped >= 2 || isPaired.Contains(cardId))
      {
        return false;
      }
      totalFlipped += 1; // increment the totalFlipped
      isFlipped.Add(cardId); // flip the card
      string cardNeeded = pairings[cardId]; // get the ideal pairing

      if (isFlipped.Contains(cardNeeded))
      {
        // pair found
        isPaired.Add(cardNeeded);
        isPaired.Add(cardId);
        totalFlipped = 0;
      }
      UpdateScore();
      if (isPaired.Count == 6)
      {
        SceneManager.LoadScene("GameOver");
      }
      return true;
    }

    public bool UnflipCard(string cardId)
    {
      if (isPaired.Contains(cardId)){
        return false;
      }
      if (isFlipped.Contains(cardId))
      {
        isFlipped.Remove(cardId);
        totalFlipped -= 1;
        return true;
      }
      return false;
    }

}
