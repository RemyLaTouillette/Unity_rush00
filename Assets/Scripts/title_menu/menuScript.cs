using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menuScript : MonoBehaviour {

	public delegate	void	txtSelected ();
	public static event	txtSelected		isSelected;

	public Title	startTitle;
	public Title	exitTitle;
	public Text		start;
	public Text		exit;

	void	 selectionChanged()
	{
		start.GetComponent<Outline> ().enabled = startTitle.isSelected;
		start.GetComponent<Title> ().enabled = startTitle.isSelected;
		exit.GetComponent<Outline> ().enabled = exitTitle.isSelected;
		exit.GetComponent<Title> ().enabled = exitTitle.isSelected;
	}


	void	OnEnable()
	{
		menuScript.isSelected += selectionChanged;
	}

	void	OnDisable()
	{
		menuScript.isSelected -= selectionChanged;
	}

	//public GameObject PauseUI;
	void Start() {
		startTitle = start.GetComponent<Title> ();
		exitTitle = exit.GetComponent<Title> ();
		startTitle.isSelected = true;
		exitTitle.isSelected = false;

		if (isSelected != null)
			isSelected ();
	}

	void	getInput()
	{
		if (Input.GetKeyUp(KeyCode.W)) //up
		{
			startTitle.isSelected = true;
			exitTitle.isSelected = false;
			if (isSelected != null)
				isSelected();
			Debug.Log ("Up pressed"); //Debug
		}
		if (Input.GetKeyUp(KeyCode.S)) //down
		{
			startTitle.isSelected = false;
			exitTitle.isSelected = true;
			if (isSelected != null)
				isSelected();
			Debug.Log ("Down pressed"); //Debug
		}
		if (Input.GetKeyUp (KeyCode.Return))
		{
			Debug.Log ("Enter is pressed"); //Debug
			if (startTitle.isSelected)
			{
				Debug.Log ("I load level"); //Debug
				Application.LoadLevel(1);
			}
			else if (exitTitle.isSelected)
			{
				Debug.Log ("I quit level"); //debug
				Application.Quit();
			}
		}
	}
	
	void Update() {
		getInput ();
	}

	public void Start_Game()
	{
		Application.LoadLevel(1);
	}

	public void	Restart_Level()
	{
		Application.LoadLevel (1); //load current level
	}

	public void MainMenu()
	{
		Application.LoadLevel (1); //load Main menu scene
	}

	public void Exit() {
		Application.Quit();
	}
}