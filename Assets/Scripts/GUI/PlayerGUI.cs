using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    public static PlayerGUI Instance { get; private set; }
    public static Player CurrentPlayer;

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

    public Vector2 GetNearestPoint(Vector2[] worldPoints)
    {
        float GetMagnitude(Vector2 point) { return ((Vector2)CurrentPlayer.transform.position - point).magnitude; }

        if (worldPoints.Length <= 1) return new Vector2();
        float curMagnitude = GetMagnitude(worldPoints[0]);
        Vector2 result = worldPoints[0];

        for(int i = 1; i < worldPoints.Length; i++)
        {
            float magnitude = GetMagnitude(worldPoints[i]);
            if(magnitude < curMagnitude)
            {
                curMagnitude = magnitude;
                result = worldPoints[i];
            }
        }
        return result;
    }
}
