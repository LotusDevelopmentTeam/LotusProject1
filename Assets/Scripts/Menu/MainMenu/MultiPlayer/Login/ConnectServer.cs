using UnityEngine;
using TMPro;

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

        string username = GameObject.Find("UsernameInput").GetComponent<TextMeshProUGUI>().text;
        string password = GameObject.Find("PasswordInput").GetComponent<TextMeshProUGUI>().text;

        GameObject.Find("LoginManager").GetComponent<LoginManager>().LogIn(username, password);       
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}
