using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatActions : MonoBehaviour
{
    [SerializeField] private Canvas CanvasObject;
    [SerializeField] private TMP_InputField MessageInput;
    public void EnviarMensaje()
    {
        GameObject chatMessage = new GameObject("Mensaje");
        chatMessage.transform.SetParent(CanvasObject.GetComponent<Canvas>().transform);
        chatMessage.AddComponent<TextMeshProUGUI>().text = MessageInput.text;
        MessageInput.text = "";

        var chatText = chatMessage.GetComponent<TextMeshProUGUI>();
        chatText.color = UnityEngine.Color.black;
        chatText.fontSize = 24;

        RectTransform rectTransform;
        rectTransform = chatText.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector2(-65, 430);
        rectTransform.sizeDelta = new Vector2(650, 50);
        rectTransform.localScale = new Vector3(1F, 1F, 1F);
    }
}
