using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
   [SerializeField]private TextMeshProUGUI _countdownText;

   private void Start()
   {
      GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

      Hide();
   }

   private void Update()
   {
      _countdownText.text = Mathf.Ceil(GameManager.Instance.GetCoundownToStartTimer()).ToString();
   }

   private void GameManager_OnStateChanged(object sender, EventArgs e)
   {
      if (GameManager.Instance.IsCountdownToStartActive())
      {
         Show();
      }
      else
      {
         Hide();
      }
   }

   private void Show()
   {
      gameObject.SetActive(true);
   }

   private void Hide()
   {
      gameObject.SetActive(false);
   }
}
