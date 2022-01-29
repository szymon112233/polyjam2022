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
	private List<GameObject> Levels;

	[Header("UI elements")]
	[SerializeField]
	private TextMeshProUGUI ScoreText;
	[SerializeField]
	private GameObject VictoryScreen;

	private int NumberOfCurrentlyActivatedNodes;
	private int TotalNumberOfNodes;

	private List<GameObject> LevelsLeftToPlay;
	private GameObject CurrentlyPlayedLevel;

	private void Awake()
	{
		NodeActivatedEvent.Action += OnNodeActivated;

		LevelsLeftToPlay = new List<GameObject>(Levels);

		LoadNewLevel();
	}

	private void OnDestroy()
	{
		NodeActivatedEvent.Action -= OnNodeActivated;
	}

	private void LoadNewLevel()
	{
		if (LevelsLeftToPlay.Count == 0)
		{
			SceneManager.LoadScene("MainMenu");
			return;
		}

		if (CurrentlyPlayedLevel != null)
		{
			Destroy(CurrentlyPlayedLevel);
			var waveObjectsLeft = GameObject.FindGameObjectsWithTag("Wave");
			foreach (var wave in waveObjectsLeft)
			{
				Destroy(wave);
			}
		}

		var levelToLoad = LevelsLeftToPlay[0];
		LevelsLeftToPlay.RemoveAt(0);
		CurrentlyPlayedLevel = Instantiate(levelToLoad);

		NumberOfCurrentlyActivatedNodes = 0;
		TotalNumberOfNodes = FindObjectsOfType<BasicNode>().Count(node => !(node is ControllableNode) && !node.Activated);

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
}
