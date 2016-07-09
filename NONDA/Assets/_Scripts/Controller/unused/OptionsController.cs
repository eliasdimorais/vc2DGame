using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
	#region Public Variables
	public Slider volumeSlider;
	public LevelManager levelManager;
	#endregion

	#region Private Variabless
	//private MusicManager musicManager;
	#endregion

	void Start(){
		//musicManager = GameObject.FindObjectOfType<MusicManager>();
	}

	public void SaveOptionsAndExit(){
		//PlayerPrefsManager.SetMasterVolume(volumeSlider);
		//levelManager.LoadLevel("01a_Start");
	}
}


