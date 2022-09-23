using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataLoader : MonoBehaviour
{
    
    [SerializeField] private InputField nameInput;
    [SerializeField] private InputField emailInput;
    [SerializeField] private WebManager manager;
    
    public void LoadData()
    {
        nameInput.text = WebManager.userData.playerData.nickname;
        emailInput.text = WebManager.userData.playerData.email;
    }

    public void SaveData()
    {
        var player = WebManager.userData.playerData;
        manager.SaveData(player.id, player.email, player.nickname);
    } 
   
}
