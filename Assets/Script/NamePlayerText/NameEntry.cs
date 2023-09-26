using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameEntry : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] Button submitButton;
    public void SubmitName()
    {
        FusionConnector.Instance.ConnectToRunner(nameInputField.text);
    }

    public void ActivateButton()
    {
        //submitButton.onClick.RemoveAllListeners();
        submitButton.interactable = true;

    }
}
