using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGetter : MonoBehaviour
{
    CircleCollider2D circleCollider2D;
    NonPlayerCharacter nonPlayerCharacter;

    public bool priorityTargeter;

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        nonPlayerCharacter = transform.parent.GetComponent<NonPlayerCharacter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            print("trigger with: " + collision.gameObject.name);
            if (character.teamData != nonPlayerCharacter.teamData)
            {
                print("TRIGGER IS ENEMY");
                if (priorityTargeter)
                {
                    print("ENEMY TARGETED BY: " + gameObject.name);
                    nonPlayerCharacter.target = character;
                    return;
                }
                else if (!nonPlayerCharacter.target)
                {
                    nonPlayerCharacter.target = character;
                    return;
                }
            }
        }
    }
}
