using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackController : MonoBehaviour
{
    [SerializeField] private float attackDuration = 0.5f;

    [SerializeField] private CharacterDamageTrigger damageTrigger;

    public void Attack()
    {
        StopAllCoroutines();

        damageTrigger.gameObject.SetActive(true);

        StartCoroutine(HideAttackTriggerDelayed(attackDuration));
    }

    public void StopAttack()
    {
        damageTrigger.gameObject.SetActive(false);

        StopAllCoroutines();
    }

    private IEnumerator HideAttackTriggerDelayed(float time)
    {
        yield return new WaitForSeconds(time);

        damageTrigger.gameObject.SetActive(false);
    }
}
