using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : SingletonMonoBehaviourBase<DebugManager>
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.Z))
        {
                SavesManager.Instance.PlayerStats.Value.ChangeMoney(-50);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                SavesManager.Instance.PlayerStats.Value.ChangeMoney(50);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                DayManager.Instance.DebugSkipDay();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SavesManager.Instance.PlayerStats.Clear();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Time.timeScale = 1.0f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Time.timeScale = 2.0f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Time.timeScale = 3.0f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Time.timeScale = 4.0f;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Time.timeScale = 5.0f;
            }
        }
    }
}
