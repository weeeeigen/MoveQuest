using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
//using MiniJSON;

public class  HomeManager : MonoBehaviour {

	public Button quest;
	public Button monster;

	public Slider expslider;
	public Text usernametext;
	public Text userpointtext;
	public Text userleveltext;
	public Text userafterexptext;
	public Text detailtext;

	public string userid;
	public string username;
	public int userpoint;
	public int userlevel;
	public int userexp;
	public int usermaxexp;
	public int userafterexp;

	public string detail;

	public Dictionary<string, string> seria;
	public Dictionary<string, string> cal;
	public Dictionary<string, string> rugina;
	public Dictionary<string, string> paris;


	// Use this for initialization
	void Start () {

		//名前を取り出す
		username = PlayerPrefs.GetString("username");
		usernametext.text = username;

		//レベルを取り出す
		userlevel = PlayerPrefs.GetInt("userlv");
		userleveltext.text = "Lv." + userlevel.ToString ();

		//経験値を取り出す
		userexp = PlayerPrefs.GetInt ("userexp");
		expslider.value = userexp;

		//最大経験値を取り出す
		usermaxexp = PlayerPrefs.GetInt ("usermaxexp");
		expslider.maxValue = usermaxexp;

		//必要な経験値をとりだす
		userafterexp = usermaxexp - userexp;
		userafterexptext.text = "After" + userafterexp.ToString ();


		//useridからポイントを持ってくる
		userpoint = PlayerPrefs.GetInt("userpoint");
		userid = PlayerPrefs.GetString("userid");
		StartCoroutine ("getpoint");


		//ユニットデータを取り出す
		seria = PlayerPrefsUtility.LoadDict<string, string> ("Seria");
		cal = PlayerPrefsUtility.LoadDict<string, string> ("Cal");
		rugina = PlayerPrefsUtility.LoadDict<string, string> ("Rugina");
		paris = PlayerPrefsUtility.LoadDict<string, string> ("Paris");

	}
	
	// Update is called once per frame
	void Update () {
	}


	public void GoQuest(){
		SceneManager.LoadScene ("StageSelect");
	}


	public void GoPowerup(){
		SceneManager.LoadScene ("Powerup");
	}

	public void GoHelp(){
		SceneManager.LoadScene ("Help");
	}

	//選ばれたユニットのステータスを見せる
	public void showdetail(int i){
		if (i == 1) {
			detail = seria ["Name"] + "\n      Lv:" + seria["LV"] + "      Exp:" + seria["EXP"] + "\n" + "HP:" + seria ["HP"] + "      MP:" + seria ["MP"] + "\n" + "ATK:" + seria ["ATK"] + "      DEF:" + seria["DEF"];
		} else if (i == 2) {
			detail = cal["Name"] + "\n      Lv:" + cal["LV"] + "      Exp:" + cal["EXP"] + "\n" + "HP:" + cal["HP"] + "      MP:" + cal["MP"] + "\n" + "ATK:" + cal["ATK"] + "      DEF:" + cal["DEF"];
		} else if (i == 3) {
			detail = rugina["Name"] + "\n      Lv:" + rugina["LV"] + "      Exp:" + rugina["EXP"] + "\n" + "HP:" + rugina["HP"] + "      MP:" + rugina["MP"] + "\n" + "ATK:" + rugina["ATK"] + "      DEF:" + rugina["DEF"];
		} else if (i ==4){
			detail = paris["Name"] + "\n      Lv:" + paris["LV"] + "      Exp:" + paris["EXP"] + "\n" + "HP:" + paris["HP"] + "      MP:" + paris["MP"] + "\n" + "ATK:" + paris["ATK"] + "      DEF:" + paris["DEF"];
		}
		detailtext.text = detail;
	}

	public IEnumerator getpoint(){

		string url = "http://133.27.171.211/~eigen/getdata.php";

		//データ送信準備
		WWWForm wwwForm = new WWWForm ();			
		wwwForm.AddField ("FitbitID", userid);
	
		//データを取得する			
		WWW response = new WWW(url, wwwForm);

		//レスポンスを待つ
		yield return response;

	    //var json = MiniJSON.Json.Deserialize(response.text) as Dictionary<string,object>;
		userpoint += int.Parse( response.text );

		PlayerPrefs.SetInt ("userpoint", userpoint);
		userpointtext.text = userpoint.ToString ();


		yield break;
	}

		
}