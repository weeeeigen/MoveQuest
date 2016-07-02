using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LoginManager : MonoBehaviour {
	public InputField Inputname;
	public InputField InputID;

	public string username;
	public string userid;

	public Dictionary<string, string> seria = new Dictionary<string, string> (){
		{"Name", "煌華の神陽姫セリア"},
		{"EXP", "1"},
		{"LV", "1"},
		{"HP", "30"},
		{"MP", "20"},
		{"ATK", "5"},
		{"DEF", "5"},
		{"BBLV", "1"},
		{"SBBLV", "1"}
	};
	public Dictionary<string, string> cal = new Dictionary<string, string> (){
		{"Name", "氷勇の神皇騎カル"},
		{"EXP", "1"},
		{"LV", "1"},
		{"HP", "30"},
		{"MP", "20"},
		{"ATK", "5"},
		{"DEF", "5"},
		{"BBLV", "1"},
		{"SBBLV", "1"}
	};

	public Dictionary<string, string> rugina = new Dictionary<string, string> (){
		{"Name", "闘刄の神烈騎ルジーナ"},
		{"EXP", "1"},
		{"LV", "1"},
		{"HP", "30"},
		{"MP", "20"},
		{"ATK", "5"},
		{"DEF", "5"},
		{"BBLV", "1"},
		{"SBBLV", "1"}
	};

	public Dictionary<string, string> paris = new Dictionary<string, string> (){
		{"Name", "雷響の神凰姫パリス"},
		{"EXP", "1"},
		{"LV", "1"},
		{"HP", "30"},
		{"MP", "20"},
		{"ATK", "5"},
		{"DEF", "5"},
		{"BBLV", "1"},
		{"SBBLV", "1"}
	};

	// Use this for initialization
	void Start () {

		//初回ログイン時に認証画面へ移動する
		Application.OpenURL ("https://www.fitbit.com/oauth2/authorize?response_type=code&client_id=229XJK&redirect_uri=http%3A%2F%2F133.27.171.211%2F~eigen%2Ftest.php&scope=activity%20nutrition%20heartrate%20location%20nutrition%20profile%20settings%20sleep%20social%20weight");
		
		PlayerPrefsUtility.SaveDict<string, string> ("Seria", seria);
		PlayerPrefsUtility.SaveDict<string, string> ("Cal", cal);
		PlayerPrefsUtility.SaveDict<string, string> ("Rugina", rugina);
		PlayerPrefsUtility.SaveDict<string, string> ("Paris", paris);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Save(){
		username = Inputname.text;
		userid = InputID.text;

		//入力された情報などを保存する
		PlayerPrefs.SetString ("username", username);
		PlayerPrefs.SetString ("userid", userid);
		PlayerPrefs.SetInt ("userpoint", 0);
		PlayerPrefs.SetInt ("userexp", 0);
		PlayerPrefs.SetInt ("usermaxexp", 5);
		PlayerPrefs.SetInt ("userlv", 1);

		PlayerPrefs.Save ();

		SceneManager.LoadScene ("Home");
	}
		
}
