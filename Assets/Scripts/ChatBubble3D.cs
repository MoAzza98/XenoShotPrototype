using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatBubble3D : MonoBehaviour
{
    private Image backgroundImage;
    private Image iconImage;
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        backgroundImage = transform.Find("Frame").GetComponent<Image>();
        iconImage = transform.Find("Icon").GetComponent<Image>();
        textMesh = transform.Find("PopupText").GetComponent<TextMeshProUGUI>();

    }

    private void Start()
    {
        //Setup("Hey, testing here!");
    }

    private void Setup(string text)
    {
        textMesh.SetText(text);
        textMesh.ForceMeshUpdate();
        Vector2 textSize = textMesh.GetRenderedValues(false);

        Vector2 padding = new Vector2(4f, 2f);
        //backgroundImage.sprite. = textSize + padding;
    }
}
