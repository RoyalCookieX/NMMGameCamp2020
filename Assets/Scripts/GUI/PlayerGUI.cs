using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    public static PlayerGUI Instance { get; private set; }
    public static Player Player;

    [Header("PlayerGUI Components")]
    [SerializeField] Transform healthbar;
    [SerializeField] TMP_Text ammoText;

    private void Update()
    {
        if (!Player) return;
        healthbar.localScale = new Vector3(Player.Health / 100, 1, 1);

        if (Player.Weapon)
        {
            ammoText.text = $"{Player.Weapon.CurAmmo}/{Player.Weapon.GetWeaponData().ammo}";
        }
        else
        {
            ammoText.text = "";
        }
    }
}
