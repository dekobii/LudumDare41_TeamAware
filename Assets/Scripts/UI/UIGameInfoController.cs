﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameInfoController : MonoBehaviour {

	public static UIGameInfoController Instance { get; private set; }

	public GameObject gameInfo;
	public Text carrotsNeededAmountText;

	private Goal goal;
	private PlayerState playerState;

	private int carrotsGoal;
	private int carrotsPlayer;

	void Awake()
	{
		if (Instance == null)
		{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (Regex.Match(scene.name, @"\d$").Success)
		{
			gameInfo.SetActive(true);
			goal = null;
			playerState = null;
		}
		else
		{
			gameInfo.SetActive(false);
		}
	}

	void Update()
	{
		if (!gameInfo.activeInHierarchy)
		{
			return;
		}
		
		if (goal == null)
		{
			goal = GameObject.Find("Goal").GetComponent<Goal>();
		}
		if (playerState == null)
		{
			playerState = GameObject.Find("Player").GetComponent<PlayerState>();
		}
		
		carrotsGoal = goal.carrotsNeeded;
		carrotsPlayer = playerState.carrots;
		carrotsNeededAmountText.text = (Mathf.Max((carrotsGoal - carrotsPlayer), 0)).ToString();
	}
}