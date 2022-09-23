using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour
{
    [System.Serializable]
    public class MenuLogin
    {
        public Text login, password;
    }

    
    [System.Serializable]
    public class MenuRegistration
    {
        public Text login, password1, password2, nickname, email;

        public InputField loginField, pass1Field, pass2Field, nicknameField, emailField;
    }
        public MenuLogin loginWindow;
        public MenuRegistration registrationWindow;

        [SerializeField] 
        private WebManager webManager;

    public void Login()
    {
        webManager.Login(loginWindow.login.text, loginWindow.password.text);
    }

    public void Register()
    {
        webManager.Registration(registrationWindow.login.text, registrationWindow.password1.text, registrationWindow.password2.text, registrationWindow.nickname.text, registrationWindow.email.text);
    }

    public void Clear()
    {
        registrationWindow.loginField.text = "";
        registrationWindow.pass1Field.text = "";
        registrationWindow.pass2Field.text = "";
        registrationWindow.nicknameField.text = "";
        registrationWindow.emailField.text = ""; 
    }

}
