using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;



[System.Serializable]
public class UserData
{
    
    public Player playerData;
    public Error error;

}

[System.Serializable]
public class Error
{
    public string errorText;
    public bool isError;
}

[System.Serializable]
public class Player
{
    public int id;
    public string nickname; 
    public string email; 

    public Player()
    {
        
    }

    public Player(string nick, string mail)
    {
        nickname = nick;
        email = mail;
    }

    public void SetNickname(string name) => nickname = name;
    public void SetEmail(string mail) => email = mail;
    

}
public class WebManager : MonoBehaviour
{

    public static UserData userData = new UserData();
    
    [SerializeField] private string targetURL;

    [SerializeField] private UnityEvent OnLogged, OnRegistered, OnError;

    public enum RequestType
    {
        logging, register, save
    }
    
    
    public string GetUserData(UserData data)
    {
        return JsonUtility.ToJson(data);
    }

    public UserData SetUserData(string data)
    {
        print(data);
        return JsonUtility.FromJson<UserData>(data);
    }

    private void Start()
    {
        userData.error = new Error() {errorText = "text", isError = true};
        userData.playerData = new Player("sobash", "mail");
    }

    public void Login(string login, string password)
    {
       StopAllCoroutines(); 
       //Logging(login, password);
       if (CheckString(login) && CheckString(password))
        {
            Logging(login, password);
        }
        else
        {
            //Debug.Log(userData.error.errorText);
            userData.error.errorText = "To small length";
            Debug.Log(userData.error.errorText);
            OnError.Invoke();
        }
    }

    public void Registration(string login, string password, string password2, string nickname, string email)
    {
       StopAllCoroutines(); 
       //Registering(login, password, password2, nickname, email);
       if (CheckString(login) && CheckString(password) && CheckString(password2) && CheckString(nickname) && CheckString(email) && password == password2)
        {
            Registering(login, password, password2, nickname, email);
        }
        else
        {
            Debug.Log("To small length");
            userData.error.errorText = "To small length";
            OnError.Invoke();
        }
    }

    public void SaveData(int id, string nickname, string email)
    {
        StopAllCoroutines();
        SaveProgress(id, nickname, email);
    }

    public bool CheckString(string toCheck)
    {
        toCheck = toCheck.Trim();
        if (toCheck.Length > 4 && toCheck.Length < 16)
        {
            return true;
        }
        return false;
    }


    public void Logging(string login, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.logging.ToString());
        form.AddField("login", login);
        form.AddField("password", password);
        StartCoroutine(SendData(form, RequestType.logging));

    }

    public void Registering(string login, string password1, string password2, string nickname, string email)
    {
        WWWForm form = new WWWForm();
        
        form.AddField("type", RequestType.register.ToString());
        form.AddField("login", login);
        form.AddField("password1", password1);
        form.AddField("password2", password2);
        form.AddField("nickname", nickname);
        form.AddField("email", email);
        StartCoroutine(SendData(form, RequestType.register));

    }

    public void SaveProgress(int id, string email, string nickname)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", RequestType.save.ToString());
        form.AddField("id", id);
        form.AddField("email", email);
        form.AddField("nickname", nickname);
        StartCoroutine(SendData(form, RequestType.save));
    }

    IEnumerator SendData(WWWForm form, RequestType type)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(targetURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                var data = SetUserData(www.downloadHandler.text);
                if (!data.error.isError)
                {
                    if (type != RequestType.save)
                    {
                        userData = data;
                        if (type == RequestType.logging)
                        {
                            OnLogged.Invoke();
                        }
                        else
                        {
                            OnRegistered.Invoke();
                        }
                    }
                }
                else
                {
                    userData = data;
                    OnError.Invoke();
                }
            }
        }
    }

}
