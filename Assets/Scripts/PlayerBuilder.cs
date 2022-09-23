using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBuilder : MonoBehaviour
{
    [SerializeField] private Text nicknamePlayer;
    [SerializeField] private InputField fieldSelectTextName, fieldSelectTextEmail;
    string input_textName, input_textEmail;
    
    public void Update()
    {
        input_textName = fieldSelectTextName.text;
        nicknamePlayer.text = input_textName;

        input_textEmail = fieldSelectTextEmail.text;
    }

    public void ChangeNickName(Text newName)
    {
        input_textName = newName.text;
        WebManager.userData.playerData.nickname = newName.text;
    }

    public void ChangeEmail(Text newEmail)
    {
        input_textEmail = newEmail.text;
        WebManager.userData.playerData.email = newEmail.text;
    }
    

}
