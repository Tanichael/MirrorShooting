using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameSceneManager : MonoBehaviour
{
    [SerializeField] private InputField _inputField;
    [SerializeField] private Text _errorText;

    void OnEnable()
    {
        _errorText.text = "";
        _inputField.onEndEdit.AddListener(_ =>
        {
            if (_inputField.text == "")
            {
                Debug.Log("Input something");
                _errorText.text = "Please input something!";
                return;
            }
            PlayerInfo.Instance.SetName(_inputField.text);
            SceneManager.LoadScene("RoomScene");
        });
    }
}
