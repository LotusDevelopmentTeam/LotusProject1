<<<<<<< Updated upstream
<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.SceneManagers;
=======
﻿using UnityEngine;
using TMPro;
>>>>>>> Stashed changes
=======
﻿using UnityEngine;
using TMPro;
>>>>>>> Stashed changes

public class ConnectServer : MonoBehaviour
{
    Main_Menu mainScript;

    // Start is called before the first frame update
    void Start()
    {
        mainScript = GameObject.Find("MenuManager").GetComponent<Main_Menu>();
    }

    // Update is called once per frame
    public void Connect()
    {
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
        string username = GameObject.Find("UsernameInput").GetComponent<TextMeshProUGUI>().text;
        string password = GameObject.Find("PasswordInput").GetComponent<TextMeshProUGUI>().text;

        GameObject.Find("LoginManager").GetComponent<LoginManager>().LogIn(username, password);       
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}