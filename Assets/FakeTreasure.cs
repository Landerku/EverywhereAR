using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTreasure : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DialogBox;
    public void Tryprint()
    {
        Dialog.Open(DialogBox, DialogButtonType.Confirm, "What a pity!", "This is a fake treasure", true);
    }
}
