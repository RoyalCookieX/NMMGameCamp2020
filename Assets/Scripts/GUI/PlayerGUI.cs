using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    public static PlayerGUI Instance { get; private set; }
    public static Player CurrentPlayer;
    public static List<Transform> UncapturedPoints;

    [Header("PlayerGUI Components")]
    [SerializeField] Transform healthbar;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] Transform reloadIcon;

    [Space]
    [Header("PlayerGUI Properties")]
    [SerializeField] float reloadIconSpeed = -120;


    private void Update()
    {
        if (!CurrentPlayer) return;
        healthbar.localScale = new Vector3(Mathf.Clamp(CurrentPlayer.Health / 100, 0, 1), 1, 1);

        if (CurrentPlayer.Weapon)
        {
            ammoText.text = $"{CurrentPlayer.Weapon.CurAmmo}/{CurrentPlayer.Weapon.GetWeaponData().ammo}";
            reloadIcon.gameObject.SetActive(CurrentPlayer.Weapon.CurCooldown > 0);
            if (CurrentPlayer.Weapon.CurCooldown > 0)
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
