using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetInformationFromCanvas : MonoBehaviour
{

    [SerializeField]
    private Text text = null;


    [SerializeField]
    private Image image = null;

    [SerializeField]
    private RectTransform rectTransform = null;


    public Text GetText()
    {
        return text;
    }
    public void SetText(string newtext)
    {
        text.text = newtext;
    }

    public Image GetImage()
    {
        return image;
    }
    public void SetSprite(Sprite newsprite)
    {
        image.sprite = newsprite;
    }
    public RectTransform GetRectTransform()
    {
        return rectTransform;
    }
}
