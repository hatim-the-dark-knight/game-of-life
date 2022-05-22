using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public bool status;
    public Color color;

    Button cellButton;
    ColorBlock cellButtonColorBlock;
    // Start is called before the first frame update
    void Start()
    {
        cellButton = transform.GetComponent<Button>();
        cellButton.onClick.AddListener (() => SetCellProps ());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCellProps () {
        if (status == false) {
            status = true;
            color = Color.white;
            SetColor (cellButton, color);
        }
        else {
            status = false;
            color = Color.black;
            SetColor (cellButton, color);
        }
        
    }

    void SetColor (Button cellButton, Color color) {
        cellButtonColorBlock = cellButton.colors;
        cellButtonColorBlock.normalColor = color;
        cellButtonColorBlock.selectedColor = color;
        cellButtonColorBlock.highlightedColor = color;
        cellButtonColorBlock.pressedColor = color;
        cellButton.colors = cellButtonColorBlock;
    }

    public void ResetCell (Button cellBtn) {
        status = false;
        color = Color.black;
        SetColor (cellBtn, color);
    }
}
