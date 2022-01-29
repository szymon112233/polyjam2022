using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
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

	private int NumberOfCurrentlyActivatedNodes;
	private int TotalNumberOfNodes;

	private List<GameObject> LevelsLeftToPlay;
	private GameObject CurrentlyPlayedLevel;
	private List<ControllableNode> ControllableNodesInCurrentLevel;
	private List<GameObject> CurrentLightObjects = new List<GameObject>();

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

		var levelToLoad = LevelsLeftToPlay[0];
		LevelsLeftToPlay.RemoveAt(0);
		CurrentlyPlayedLevel = Instantiate(levelToLoad);

		NumberOfCurrentlyActivatedNodes = 0;
		TotalNumberOfNodes = FindObjectsOfType<BasicNode>().Count(node => !(node is ControllableNode) && !node.Activated);
		ControllableNodesInCurrentLevel = FindObjectsOfType<ControllableNode>().ToList();

		VictoryScreen.SetActive(false);
		UpdateGUI();
	}

	private void OnNodeActivated(BasicNode activatedNode)
	{
		NumberOfCurrentlyActivatedNodes++;
		UpdateGUI();

		if (NumberOfCurrentlyActivatedNodes >= TotalNumberOfNodes)
		{
			Debug.Log("Wygranko!");
			VictoryScreen.SetActive(true);
		}
	}

	private void UpdateGUI()
	{
		ScoreText.text = $"Activated nodes: {(((float)NumberOfCurrentlyActivatedNodes/ (float)TotalNumberOfNodes) * 100f).ToString("0")}%";
	}

	public void NextLevelButtonBehaviour()
	{
		LoadNewLevel();
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

		if (CurrentLightObjects.Count == 0 && ControllableNodesInCurrentLevel.All(node => !node.HasAnyActionsLeft()))
		{
			FailureScreen.SetActive(true);
		}
	}
}
