﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerHP : MonoBehaviour
{
	public static Action<int> HealingPlayerEvent;

	[Header("Settings: ")]
	[SerializeField] private int maxHealth = 3;
	[SerializeField] private List<Image> heartSprites = new List<Image>();
	[SerializeField] private Sprite fullHeart = null;
	[SerializeField] private Sprite brokenHeart = null;

	private float currentHealth;

	private void Awake()
	{
		ResetHealth();
	}

	private void Update()
	{
		ChangeSpriteBasedOnLives();
	}

	private void ResetHealth()
	{
		currentHealth = maxHealth;
	}

	private void ChangeSpriteBasedOnLives()
	{
		for (int i = 0; i < heartSprites.Count; i++)
		{
			if (i < currentHealth)
			{
				heartSprites[i].sprite = fullHeart;
			}
			else
			{
				heartSprites[i].sprite = brokenHeart;
			}

			if (i < currentHealth)
			{
				heartSprites[i].enabled = true;
			}

			//Als je de sprites wilt disabelen.
			/* 
			else
			{
				heartSprites[i].enabled = false;
			}
			*/
		}
	}

	private void TakeDamage(int _damageTaken)
	{
		currentHealth -= _damageTaken;
		DeathState();
	}

	private void HealPlayer(int _healAmount)
	{
		currentHealth += _healAmount;
	}

	private void DeathState()
	{
		if (currentHealth <= 0)
		{
			Debug.Log("Player Death");

			Destroy(gameObject, 1f);
		}
	}

	private void OnEnable()
	{
		Enemy.EnemyAttackHitEvent += TakeDamage;
		HealingPlayerEvent += HealPlayer;
	}

	private void OnDisable()
	{
		Enemy.EnemyAttackHitEvent -= TakeDamage;
		HealingPlayerEvent -= HealPlayer;
	}
}