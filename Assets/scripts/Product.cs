using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Product : MonoBehaviour
{
    public Item item;

    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] TextMeshProUGUI Stock;
    [SerializeField] Image image;

    private void Update()
    {
        if(item.Name != null)
        {
            Name.text = item.Name;
        }

        price.text = item.price.ToString();

        Stock.text = item.stock.ToString();

        if(item.sprite != null)
        {
            image.sprite = item.sprite;
        }
    }
}
