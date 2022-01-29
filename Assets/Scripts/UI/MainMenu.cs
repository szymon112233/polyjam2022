using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
	public GameObject CreditsPanel;

	public void NewGame()
	{
		SceneManager.LoadScene("gameplay");
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void ShowCredits(bool show)
	{
		CreditsPanel.SetActive(show);
	}
}
