using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;

public class LoginManager : MonoBehaviour
{
    public string phpURL = "http://localhost/Querys/Login.php";
    public InputField nickInput;
    public InputField passwordInput;

    private string _nick;
    private string _password;

    public GameObject txtOculto;

    public string sha256_hash(string value)
    {
        StringBuilder Sb = new StringBuilder();

        using (var hash = SHA256.Create())
        {
            Encoding enc = Encoding.UTF8;
            byte[] result = hash.ComputeHash(enc.GetBytes(value));

            foreach (byte b in result)
                Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
    }

    public void OnLoginButtonClicked()
    {
        string nick = nickInput.text;
        string password = passwordInput.text;

        
        StartCoroutine(SendLoginRequest(nick, password));
        
    }

    private IEnumerator SendLoginRequest(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("nick", username);
        form.AddField("password", password);

        UnityWebRequest request = UnityWebRequest.Post(phpURL, form);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            if (response == "0")
            {
                SceneManager.LoadScene("MainMenuScene");
            }
            else
            {
                MakeObjectVisible();
            }
        }
        else
        {
            
            Debug.LogError("Error en la solicitud: " + request.error);
        }
    }

    private void Start()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        Screen.SetResolution(1920, 1080, FullScreenMode.MaximizedWindow);
    }

    public void EnterAsGuest()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void MakeObjectVisible()
    {
        // Activa el objeto para hacerlo visible
        txtOculto.SetActive(true);

        // Si el objeto tiene un componente Renderer, puedes habilitarlo también
        Renderer renderer = txtOculto.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }
    }

   
}
