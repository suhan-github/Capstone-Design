using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPanelInitializer : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static CardPanelInitializer instance;
    public static CardPanelInitializer Instance { get { return instance; } }
    #endregion

    public List<GameObject> cardImages;
    [SerializeField]private bool[] cardImage_enable;
    void Awake()
    {
        instance = this;
        cardImages = new List<GameObject>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).CompareTag("Image_Collect"))
                cardImages.Add(this.transform.GetChild(i).gameObject);
        }
        cardImage_enable = new bool[cardImages.Count];
        foreach (GameObject cardImage in cardImages)
        {
            cardImage.gameObject.SetActive(false);
        }
        this.transform.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        for (int i = 0; i < cardImages.Count; i++)
        { cardImages[i].SetActive(cardImage_enable[i]); }
    }

    // Start is called before the first frame update
    void Start()
    {       

    }

    public int GetImageIndex(string dummy_)
    {
        for (int i = 0; i < cardImages.Count; i++)
        {            
            if (dummy_.Equals(cardImages[i].GetComponent<ImageContent>().Content))
            {
                return i;
            }
        }
        return -1;
    }

    public void ActivateImage(int imageIndex)
    {
        if (imageIndex < 0) { return; }
        // Ensure the index is valid
        if (imageIndex >= 0 && imageIndex < cardImages.Count)
        {
            // Activate the corresponding image
            cardImage_enable[imageIndex] = true;
            cardImages[imageIndex].gameObject.SetActive(true);
        }
    }
}
