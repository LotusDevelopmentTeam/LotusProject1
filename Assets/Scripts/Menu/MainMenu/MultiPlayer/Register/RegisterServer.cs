using UnityEngine;
using TMPro;

public class RegisterServer : MonoBehaviour
{
    Main_Menu mainScript;

    // Start is called before the first frame update
    void Start()
    {
        mainScript = GameObject.Find("MenuManager").GetComponent<Main_Menu>();
        
    }

    // Update is called once per frame
    public void SignUp()
    {

        string username = GameObject.Find("UsernameInput").GetComponent<TextMeshProUGUI>().text.ToString();
        username = username.Remove(username.Length - 1);
        string password = GameObject.Find("PasswordInput").GetComponent<TextMeshProUGUI>().text.ToString();
        password = password.Remove(password.Length - 1);
        if (!Server.isConnected()) Server.Connect();
        GameObject.Find("LoginManager").GetComponent<LoginManager>().SignUp(username, password);       
    }

    public void Back()
    {
        Server.Disconnect();
        Destroy(gameObject);
    }
}
