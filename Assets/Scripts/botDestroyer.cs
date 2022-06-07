using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.isGameover)
        {
            if (other.gameObject.CompareTag("Object"))
            {
                Level.instance.objectInScene--;
                UIManager.instance.UpdateLevelProgress();
                Magnet.instance.RemoveFromMagnetField(other.attachedRigidbody);
                Destroy(other.gameObject);
                if (Level.instance.objectInScene == 0)
                {
                    Invoke("NextLevel",2f);
                    UIManager.instance.levelCompletedText.SetActive(true);
                    UIManager.instance.confetti.Play(true);
                }
            }

            if (other.gameObject.CompareTag("Obstacle"))
            {
                GameManager.isGameover = true;
                Level.instance.RestartLevel();
            }
        }
    }

    void NextLevel()
    {
        Level.instance.LoadNextLevel();
    }
}
