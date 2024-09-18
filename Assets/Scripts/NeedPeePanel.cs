using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NeedPeePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI peeCountText;

    private GameManager _gameManager;

    private Transform lookRotationTarget;

    private void Awake()
    {
        _gameManager = GameManager.Instance;

        lookRotationTarget = _gameManager.CharacterProvider.transform;
    }

    private void Update()
    {
        if (lookRotationTarget == null) return;

        var newRotation = transform.rotation.eulerAngles;
        newRotation.y = Quaternion.LookRotation(lookRotationTarget.position - transform.position).eulerAngles.y;
        transform.rotation = Quaternion.Euler(newRotation);
    }

    public void SetCount(int count)
    {
        count = Mathf.Clamp(count, 0, count);

        if(count == 0)
        {
            peeCountText.color = Color.green;
        }
        else
        {
            peeCountText.color = Color.black;
        }

        peeCountText.text = $"x{count}";
    }
}
