using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : CharacterControl
{
    public Transform spawnPos;
    public Text hitText;

    public override void TakeDamage()
    {
        print("Ahhhhh!!!");
        transform.position = spawnPos.position;
        hitText.gameObject.SetActive(true);
        StartCoroutine(HideHitTextAfter(1));
    }

    private IEnumerator HideHitTextAfter(float time)
    {
        yield return new WaitForSeconds(time);
        hitText.gameObject.SetActive(false);
    }
}
