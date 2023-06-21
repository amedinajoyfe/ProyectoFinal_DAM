using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class RegisterManager : MonoBehaviour
{
    public string phpURL = "http://localhost/Querys/Register.php";

    public InputField inputNick;
    public InputField inputPassword;
    public InputField inputPassword2;
    public InputField inputEmail;

    private string _nick;
    private string _password1;
    private string _password2;
    private string _email;

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

    public void ReadStringNickInput(string nick)
    {
        _nick = nick;
    }

    public void ReadStringPasswd1Input(string password1)
    {
        _password1 = sha256_hash(password1);
    }
    public void ReadStringPasswd2Input(string password2)
    {
        _password2 = sha256_hash(password2);
    }

    public void ReadStringEmailInput(string email)
    {
        _email = email;
    }


    public void OnRegisterButtonClicked()
    {
        string nick = inputNick.text;
        string email = inputEmail.text;
        string password = inputPassword.text;
        string password2 = inputPassword2.text;


        if (password==password2)
        {
            StartCoroutine(SendInputRequest(nick, email, password));
        }

    }

    private IEnumerator SendInputRequest(string nick, string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("nick", nick);
        form.AddField("email", email);
        form.AddField("password", password);

        Debug.Log(nick + email + password);

        UnityWebRequest request = UnityWebRequest.Post(phpURL, form);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            Debug.Log("Respuesta del servidor: " + response);
        }
        else
        {
            Debug.LogError("Error en la solicitud: " + request.error);
        }
    }
}
