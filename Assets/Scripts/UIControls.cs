using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIControls : MonoBehaviour
{
    [HideInInspector] public bool isDisabled = true;
    [SerializeField] public WeaponAmmo player;
    [SerializeField] TextMeshProUGUI extraAmmo;
    [SerializeField] TextMeshProUGUI currentAmmo;

    [SerializeField] GameObject rewardPanel;
    [SerializeField] GameObject backSword1;
    [SerializeField] GameObject backItem2;
    [SerializeField] GameObject backItem3;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<WeaponAmmo>();
        isDisabled = true;
        currentAmmo.text = player.currentAmmo.ToString();
        extraAmmo.text = player.extraAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (rewardPanel.active)
            {
                rewardPanel.SetActive(false);
            }
            else
            {
                rewardPanel.SetActive(true);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (backSword1.active)
            {
                backSword1.SetActive(false);
            }
            else if (!backSword1.active)
            {
                backItem2.SetActive(false);
                backItem3.SetActive(false);
                backSword1.SetActive(true);
            }

        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (backItem2.active)
            {
                backItem2.SetActive(false);
            }
            else
            {
                backSword1.SetActive(false);
                backItem3.SetActive(false);
                backItem2.SetActive(true);
            }

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (backItem3.active)
            {
                backItem3.SetActive(false);
            }
            else
            {
                backSword1.SetActive(false);
                backItem2.SetActive(false);
                backItem3.SetActive(true);
            }

        }

        currentAmmo.text = player.currentAmmo.ToString();
        extraAmmo.text = player.extraAmmo.ToString();

        if (isDisabled && Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isDisabled = false;
        }
        else if(!isDisabled && Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            isDisabled = true;
        }
    }
}
