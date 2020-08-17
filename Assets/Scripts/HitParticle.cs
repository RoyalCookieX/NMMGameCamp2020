using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitParticle : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] AnimationCurve curve;
    [SerializeField] int maxTimeSteps;
    [SerializeField] float duration;

    public void SetText(float value)
    {
        if(value > 99999999)
        {
            text.text = "Infinity";
        }
        else
        {
            text.text = $"{Mathf.Round(value)}";
        }
    }

    public void SetColor(Color color)
    {
        text.color = color;
    }

    private void OnEnable()
    {
        StartCoroutine(AnimateHitParticle());
    }

    IEnumerator AnimateHitParticle()
    {
        for (float i = 0; i < 1; i += 1 / (maxTimeSteps * duration))
        {
            float value = curve.Evaluate(i);
            transform.localScale = Vector3.one * value;
            yield return null;
        }
        Destroy(gameObject);
    }
}
