using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Powerup : MonoBehaviour {

	public Button seriabButton;            //ユニットチェックボタン
	public Button calButton;
	public Button ruginaButton;
	public Button parisButton;
	public Button powerup;                 //強化ボタン

	public Slider expslider;               //ユーザー情報（）
	public Text usernametext;
	public Text userpointtext;
	public Text userleveltext;
	public Text userafterexptext;

	public Text bbleveltext;               //レベルチェックテキスト

	public string bblevel;                 //ユニットBB, SBBレベル
	public string sbblevel;

	public string username;                //ユーザー情報（使い回し）
	public string userid;
	public int userpoint;
	public int userlevel;
	public int userexp;
	public int usermaxexp;
	public int userafterexp;

	public int unitvalue = 0;              //
	public int restpoint;                  //
	public int pluspoint;
	public int pluslevel;

	public Dictionary<string, string> seria;              //
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


		//ポイントを取り出す
		userpoint = PlayerPrefs.GetInt("userpoint");
		userpointtext.text = userpoint.ToString ();

		//ユニットデータを取り出す
		seria = PlayerPrefsUtility.LoadDict<string, string> ("Seria");
		cal = PlayerPrefsUtility.LoadDict<string, string> ("Cal");
		rugina = PlayerPrefsUtility.LoadDict<string, string> ("Rugina");
		paris = PlayerPrefsUtility.LoadDict<string, string> ("Paris");

	}
	
	// Update is called once per frame
	void Update () {

	}


	//押されたユニットのBB、SBBのレベルを見せる
	public void levelshow(int i){
		if (i == 1) {
			
			bblevel = seria["BBLV"];
			sbblevel = seria["SBBLV"];
			unitvalue = 1;

		} else if (i == 2) {
			
			bblevel = cal["BBLV"];
			sbblevel = cal["SBBLV"];
			unitvalue = 2;

		} else if (i == 3) {
			
			bblevel = rugina["BBLV"];
			sbblevel = rugina["SBBLV"];
			unitvalue = 3;

		} else if (i == 4){
			
			bblevel = paris["BBLV"];
			sbblevel = paris["SBBLV"];
			unitvalue = 4;

		}
		bbleveltext.text = "BBLV:" + bblevel + "      SBBLV:" + sbblevel;
	}


	//選ばれたユニットのBB、SBBのレベル上げ
	public void levelup(){
		if (unitvalue == 1) {
			
			bblevel = bbcalculation (int.Parse (seria ["BBLV"])).ToString();
			if (bblevel == "10") {
				sbblevel = sbbcalculation (int.Parse (seria ["SBBLV"])).ToString();
			}
			sbblevel = "1";

			seria ["BBLV"] = bblevel;
			seria ["SBBLV"] = sbblevel;

		} else if (unitvalue == 2) {
			
			bblevel = bbcalculation (int.Parse (cal ["BBLV"])).ToString();
			if (bblevel == "10") {
				sbblevel = sbbcalculation (int.Parse (cal ["SBBLV"])).ToString ();
			} else {
				sbblevel = "1";
			}

			cal ["BBLV"] = bblevel;
			cal ["SBBLV"] = sbblevel;

		}else if (unitvalue == 3) {
			
			bblevel = bbcalculation (int.Parse (rugina ["BBLV"])).ToString();
			if (bblevel == "10") {
				sbblevel = sbbcalculation (int.Parse (rugina ["SBBLV"])).ToString();
			}
			sbblevel = "1";

			rugina ["BBLV"] = bblevel;
			rugina ["SBBLV"] = sbblevel;

		}else if (unitvalue == 4) {
			
			bblevel = bbcalculation (int.Parse (paris ["BBLV"])).ToString();
			if (bblevel == "10") {
				sbblevel = sbbcalculation (int.Parse (paris ["SBBLV"])).ToString();
			}
			sbblevel = "1";

			paris ["BBLV"] = bblevel.ToString ();
			paris ["SBBLV"] = sbblevel.ToString ();

		}

		bbleveltext.text = "BBLV:" + bblevel + "      SBBLV:" + sbblevel;
		userpointtext.text = userpoint.ToString ();

	}


	//BB計算
	public int bbcalculation(int i){
		if (i == 10) {
			return 10;
		} else {
			restpoint = userpoint % 10;
			pluspoint = userpoint - restpoint;
			pluslevel = pluspoint / 10;
			if (i + pluslevel >= 10) {
				userpoint = userpoint - 100 + i * 10;
				return 10;
			}
			userpoint = userpoint - pluspoint;
			return i + pluslevel;
		}
		
	}


	//SBB計算
	public int sbbcalculation(int k){
		restpoint = userpoint % 10;
		pluspoint = userpoint - restpoint % 10;
		pluslevel = pluspoint / 10;
		if (k + pluslevel > 10) {
			userpoint = userpoint - 100 + k * 10;
			return 10;
		}
		userpoint = userpoint - pluspoint;
		return k + pluslevel;
	}


	//ホームに戻る時値を保存する
	public void backhome(){

		PlayerPrefs.SetInt ("userpoint", userpoint);
		
		PlayerPrefsUtility.SaveDict<string, string> ("Seria", seria);
		PlayerPrefsUtility.SaveDict<string, string> ("Cal", cal);
		PlayerPrefsUtility.SaveDict<string, string> ("Rugina", rugina);
		PlayerPrefsUtility.SaveDict<string, string> ("Paris", paris);

		PlayerPrefs.Save ();

		SceneManager.LoadScene ("Home");
	}

}
