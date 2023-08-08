using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class DropDownBehaviour : MonoBehaviour
{
    [SerializeField] private Button _activateMenu;
    [SerializeField] private Button _deactivateMenu;
    [SerializeField] private Canvas _dropdownCanvas;

    private void Awake()
    {
        _activateMenu.onClick.AddListener(IsClicked);
        _deactivateMenu.onClick.AddListener(AnotherClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IsClicked()
    {
        _dropdownCanvas.gameObject.SetActive(true);
        _deactivateMenu.gameObject.SetActive(true);
        _activateMenu.gameObject.SetActive(false);
    }
    
    public void AnotherClick()
    {
        _dropdownCanvas.gameObject.SetActive(false);
        _deactivateMenu.gameObject.SetActive(false);
        _activateMenu.gameObject.SetActive(true);
    }
}
