using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    private TextMeshProUGUI playerNameText;
    private Image healthBar;

    private void Awake()
    {
        playerNameText = GetComponentInChildren<TextMeshProUGUI>();

        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            if (image.name.Equals("Fill")) {
                healthBar = image;
            }
        }
    }

    public void SetPlayerName(string name) {
        playerNameText.text = name;
    }

    public void SetHealth(float health) {
        healthBar.fillAmount = health / 100;
    }
}
