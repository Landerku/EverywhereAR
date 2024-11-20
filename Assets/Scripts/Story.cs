using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class Story : MonoBehaviour
{
    public GameObject DialogBox;
    public void Vase()
    {
        Dialog.Open(DialogBox, DialogButtonType.Confirm, "Vase", "This is a vase of medieval style, its surface decorated with delicate geometric patterns, which were often used to " +
            "symbolize the order and harmony of the cosmos at that time. It seems its owner. Its owner was evidently quite rich. ", true);
    }

    public void Candle()
    {
        Dialog.Open(DialogBox, DialogButtonType.Confirm, "Candle", "This medieval candle, made of beeswax with a thick cotton wick, was used to light up the dark halls of old castles.", true);
    }

    public void Book()
    {
        Dialog.Open(DialogBox, DialogButtonType.Confirm, "Book", "These old books, bound in leather and filled with handwritten texts on vellum. They cover a range of topics, " +
            "suggesting that the owner of this room had a interest in painting and swordsmanship.", true);
    }

    public void Box()
    {
        Dialog.Open(DialogBox, DialogButtonType.Confirm, "Table", "A simple wooden table, featuring a sturdy barrel and a tankard, essentials for the storage and " +
            "consumption of beverages, likely ale or mead, commonly enjoyed during the period.", true);
    }

    public void Scroll()
    {
        Dialog.Open(DialogBox, DialogButtonType.Confirm, "Scroll", "A scroll, crafted from parchment, commonly used for recording important texts, from legal decrees to scholarly works.", true);
    }

    public void Paint()
    {
        Dialog.Open(DialogBox, DialogButtonType.Confirm, "Paint", "Perhaps the craftsmanship of the creator is so great that the person within the painting looks a little weird, is she looking at you?", true);
    }

    public void Pillow()
    {
        Dialog.Open(DialogBox, DialogButtonType.Confirm, "Pillow", "A common throw pillow, unremarkable except for a musty smell.", true);
    }

    public void Weapons()
    {
        Dialog.Open(DialogBox, DialogButtonType.Confirm, "Weapons Rack", "A wooden weapon rack, robustly crafted to hold swords, a testament to the martial focus of its owner.", true);
    }

    public void Cat()
    {
        Dialog.Open(DialogBox, DialogButtonType.Confirm, "Wooden Cat", "A wooden carving of a cat, detailed to the whiskers, hinting at the artisan's softer side.", true);
    }

    public void Bowl()
    {
        Dialog.Open(DialogBox, DialogButtonType.Confirm, "Wooden bowl", "A simple wooden bowl, the everyday essentials of medieval dining, worn smooth from use.", true);
    }

    public void Plate()
    {
        Dialog.Open(DialogBox, DialogButtonType.Confirm, "Wooden plate", "A common wooden plate, nothing special.", true);
    }
}
