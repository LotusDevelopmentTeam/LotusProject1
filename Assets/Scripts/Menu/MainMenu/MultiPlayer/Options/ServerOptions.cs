using UnityEngine;
using TMPro;

public class ServerOptions : MonoBehaviour
{
    Main_Menu mainScript;

    // Start is called before the first frame update
    void Start()
    {
        mainScript = GameObject.Find("MenuManager").GetComponent<Main_Menu>();
        
    }

    // Update is called once per frame
    public void Login()
    {
        mainScript.Login();
    }

    public void Register()
    {
        mainScript.Register();
    }
    public void Back()
    {
        Destroy(gameObject);
    }
}
