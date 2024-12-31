using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Username : MonoBehaviour
{
    public InputField Name;
    public GameObject UsernamePage;
    public Text MyUsername;
    void Start()
    {
        if(PlayerPrefs.GetString("Username")== "" || PlayerPrefs.GetString("Username") == null)
        {
            UsernamePage.SetActive(true);

        }
        else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("Username");
            MyUsername.text = PlayerPrefs.GetString("Username");
            UsernamePage.SetActive(false);

        }

    }

  public void SaveUsername()
    {
        PhotonNetwork.NickName = Name.text;
        PlayerPrefs.SetString("Username", Name.text);
        MyUsername.text = Name.text;
        UsernamePage.SetActive(false);
    }
}
