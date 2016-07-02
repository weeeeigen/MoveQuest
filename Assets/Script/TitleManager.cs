using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {
	public Button button;

	// Use this for initialization
	void Start () {
		//PlayerPrefs.DeleteAll();

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void GoHome(){
		
		//初回ログイン判定
		if(PlayerPrefs.HasKey("userid")){
			SceneManager.LoadScene ("Home");
		}else{
			SceneManager.LoadScene("Login");
		}
	}
}
