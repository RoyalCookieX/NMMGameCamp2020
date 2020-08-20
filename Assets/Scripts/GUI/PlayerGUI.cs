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
    [SerializeField] Transform reloadIcon;

    [Space]
    [Header("PlayerGUI Properties")]
    [SerializeField] float reloadIconSpeed = -120;

    private void Update()
    {
        if (!Player) return;
        healthbar.localScale = new Vector3(Mathf.Clamp(Player.Health / 100, 0, 1), 1, 1);

        if (Player.Weapon)
        {
            ammoText.text = $"{Player.Weapon.CurAmmo}/{Player.Weapon.GetWeaponData().ammo}";
            reloadIcon.gameObject.SetActive(Player.Weapon.CurCooldown > 0);
            if (Player.Weapon.CurCooldown > 0)
            {
                reloadIcon.transform.rotation = Quaternion.AngleAxis(Time.time * reloadIconSpeed, Vector3.forward);
            }
        }
        else
        {
            ammoText.text = "";
            reloadIcon.gameObject.SetActive(false);
        }
    }
}
