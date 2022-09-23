using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationMessage : MonoBehaviour
{
    [SerializeField] private GameObject textMessage;


    public void TurnOn()
    {
        textMessage.SetActive(true);
        Invoke("TurnOff", 0.7f);
    }

    private void TurnOff()
    {
        textMessage.SetActive(false);
    }
}
