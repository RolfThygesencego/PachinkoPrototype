using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class DropDownBehaviour : MonoBehaviour
{
    [SerializeField] private Button _activateMenu;
    [SerializeField] private Canvas _dropdownCanvas;

    public bool _isClicked = false;
    public bool openingClick = false;
    private void Awake()
    {
        _dropdownCanvas.gameObject.SetActive(false);
        _isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IsClickedOpen()
    {
        _dropdownCanvas.gameObject.SetActive(true);
        _isClicked = true;
        
    }

    public void IsClickedCLosed()
    {
        _dropdownCanvas.gameObject.SetActive(false);
        _isClicked = false;
        
    }

    public void Clicked()
    {
        if (_isClicked == false)
        {
            IsClickedOpen();
            openingClick = true;
        }
        if (_isClicked == true && openingClick == false)
        {
            IsClickedCLosed();
            return;
        }
        openingClick = false;
    }
}
