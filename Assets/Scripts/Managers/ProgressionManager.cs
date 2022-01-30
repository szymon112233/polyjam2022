using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ProgressionManager : MonoBehaviour
{
	[SerializeField]
	private NodeEvent NodeActivatedEvent;
	[SerializeField]
	private GameObjectEvent LightObjectCreated;
	[SerializeField]
	private GameObjectEvent LightObjectDestroyed;
	[SerializeField]
	private List<GameObject> Levels;

	[Header("UI elements")]
	[SerializeField]
	private TextMeshProUGUI ScoreText;
	[SerializeField]
	private GameObject VictoryScreen;
	[SerializeField]
	private GameObject FailureScreen;
	[SerializeField]
	private float SceenShowDelay = 1f;

	private int NumberOfCurrentlyActivatedNodes;
	private int TotalNumberOfNodes;

	private List<GameObject> LevelsLeftToPlay;
	private GameObject CurrentlyPlayedLevel;
	private GameObject CurrentLevelPrefab;
	private List<ControllableNode> ControllableNodesInCurrentLevel;
	private List<GameObject> CurrentLightObjects = new List<GameObject>();
	private bool IsVictory;

	private void Awake()
	{
		NodeActivatedEvent.Action += OnNodeActivated;
		LightObjectCreated.Action += OnLightObjectCreated;
		LightObjectDestroyed.Action += OnLightObjectDestroyed;

		LevelsLeftToPlay = new List<GameObject>(Levels);

		LoadNewLevel();
	}

	private void OnDestroy()
	{
		NodeActivatedEvent.Action -= OnNodeActivated;
		LightObjectCreated.Action -= OnLightObjectCreated;
		LightObjectDestroyed.Action -= OnLightObjectDestroyed;
	}

	private void LoadNewLevel()
	{
		if (LevelsLeftToPlay.Count == 0)
		{
			LoadMainMenu();
			return;
		}

		if (CurrentlyPlayedLevel != null)
		{
			DestroyImmediate(CurrentlyPlayedLevel);
		}

		CurrentLevelPrefab = LevelsLeftToPlay[0];
		LevelsLeftToPlay.RemoveAt(0);
		CurrentlyPlayedLevel = Instantiate(CurrentLevelPrefab);

		PrepareToStartLevel();
	}

	private void RestartCurrentLevel()
	{
		if (CurrentlyPlayedLevel != null)
		{
			DestroyImmediate(CurrentlyPlayedLevel);
		}

		CurrentlyPlayedLevel = Instantiate(CurrentLevelPrefab);
		PrepareToStartLevel();
	}

	private void PrepareToStartLevel()
	{
		IsVictory = false;
		NumberOfCurrentlyActivatedNodes = 0;
		TotalNumberOfNodes = FindObjectsOfType<BasicNode>().Count(node => !(node is ControllableNode) && !node.Activated);
		ControllableNodesInCurrentLevel = FindObjectsOfType<ControllableNode>().ToList();

		VictoryScreen.SetActive(false);
		FailureScreen.SetActive(false);
		UpdateGUI();
	}

	private void OnNodeActivated(BasicNode activatedNode)
	{
		NumberOfCurrentlyActivatedNodes++;
		UpdateGUI();

		if (NumberOfCurrentlyActivatedNodes >= TotalNumberOfNodes)
		{
			Debug.Log("Wygranko!");
			IsVictory = true;
			StartCoroutine(DoActionWithDelay(SceenShowDelay, () => VictoryScreen.SetActive(true)));
		}
	}

	private void UpdateGUI()
	{
		ScoreText.text = $"PARTY: {(((float)NumberOfCurrentlyActivatedNodes/ (float)TotalNumberOfNodes) * 100f).ToString("0")}%";
	}

	public void NextLevelButtonBehaviour()
	{
		LoadNewLevel();
	}

	public void RestartLevelButtonBehaviour()
	{
		RestartCurrentLevel();
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	private void OnLightObjectCreated(GameObject lightObject)
	{
		CurrentLightObjects.Add(lightObject);
	}

	private void OnLightObjectDestroyed(GameObject lightObject)
	{
		CurrentLightObjects.Remove(lightObject);

		if (!IsVictory && CurrentLightObjects.Count == 0 && ControllableNodesInCurrentLevel.All(node => !node.HasAnyActionsLeft()))
		{
			StartCoroutine(DoActionWithDelay(SceenShowDelay, () => FailureScreen.SetActive(true)));
		}
	}

	private IEnumerator DoActionWithDelay(float delayInSeconds, Action action)
	{
		yield return new WaitForSeconds(delayInSeconds);

		action.Invoke();
	}
}
