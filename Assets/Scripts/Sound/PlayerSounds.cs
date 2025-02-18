using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player _player;
    private float footstepTimer;
    private float footstepTimerMax = .1f;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;
            float soundVolume = 1f;
            if (_player.IsWalking())
            {
                SoundManager.Instance.PlayFootstepSound(_player.transform.position, soundVolume);
            }
        }
    }
}