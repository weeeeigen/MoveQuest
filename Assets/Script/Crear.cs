using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Crear : MonoBehaviour {

	public Text userleveluptext;       //ユーザーレベルアップ表示テキスト
	public Text serialeveluptext;      //ユニットのレベルアップ表示テキスト
	public Text calleveluptext;        
	public Text ruginaleveluptext;     
	public Text parisleveluptext; 
	         
	public Text exptext;           //獲得経験値表示テキスト
	public Slider expslider;       
	public Text afterexptext;  

	public Text seriaafterexptext;        //ユニット必要経験値表示テキスト
	public Text calafterexptext;
	public Text ruginaafterexptext;
	public Text parisafterexptext;

	public Slider seriaexpslider;       //ユニット経験値バー
	public Slider calexpslider;
	public Slider ruginaexpslider;
	public Slider parisexpslider;

	public Slider userexpslider;           //ユーザー情報（使い回し）
	public Text usernametext;
	public Text userpointtext;
	public Text userleveltext;
	public Text userafterexptext;

	public Dictionary<int, int> seriatable = new Dictionary<int, int>();          //ユニットの経験値テーブル
	public Dictionary<int, int> caltable = new Dictionary<int, int>();
	public Dictionary<int, int> ruginatable = new Dictionary<int, int>();
	public Dictionary<int, int> paristable = new Dictionary<int, int>();

	public Dictionary<int, int> usertable = new Dictionary<int, int>();           //ユーザーの経験値テーブル

	public int userlevel;           //ユーザー情報（使い回し）
	public int userexp;
	public int unitexp;
	public string userid;
	public string username;
	public int userpoint;
	public int usermaxexp;
	public int userafterexp;

	public int plusexp;         //獲得経験値

	public int userplusexp;     //ユーザー用獲得経験値

	public int seriaplusexp;    //ユニット用獲得経験値
	public int calplusexp;
	public int ruginaplusexp;
	public int parisplusexp;

	public int serialevel;      //ユニットレベル
	public int callevel;
	public int ruginalevel;
	public int parislevel;

	public int userlevelchabge = 0;        //ユーザーレベルアップ判定

	public int serialevelchabge = 0;       //ユニットレベルアップ判定
	public int callevelchabge = 0;
	public int ruginalevelchabge = 0;
	public int parislevelchabge = 0;

	public Dictionary<string, string> seria;         //ユニット情報
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

		//経験値を取り出す
		userexp = PlayerPrefs.GetInt ("userexp");

		//最大経験値を取り出す
		usermaxexp = PlayerPrefs.GetInt ("usermaxexp");

		//必要な経験値をとりだす
		userafterexp = usermaxexp - userexp;

		//useridからポイントを持ってくる
		userpoint = PlayerPrefs.GetInt("userpoint");

		//経験値テーブルに値をいれる
		for (int i = 1; i <= 50; i++) {
			seriatable.Add (i, 8*i );
		}
		for (int i = 1; i <= 50; i++) {
			caltable.Add (i, 8*i + 1 );
		}
		for (int i = 1; i <= 50; i++) {
			ruginatable.Add (i, 8*i - 1);
		}
		for (int i = 1; i <= 50; i++) {
			paristable.Add (i, 8*i - 2);
		}

		for (int i = 1; i <= 50; i++) {
			usertable.Add (i, 5*i);
		}

		//ユニットデータを取り出す
		seria = PlayerPrefsUtility.LoadDict<string, string> ("Seria");
		cal = PlayerPrefsUtility.LoadDict<string, string> ("Cal");
		rugina = PlayerPrefsUtility.LoadDict<string, string> ("Rugina");
		paris = PlayerPrefsUtility.LoadDict<string, string> ("Paris");

		//ユニットレベルを取り出す
		serialevel = int.Parse (seria ["LV"]);
		callevel = int.Parse (cal ["LV"]);
		ruginalevel = int.Parse (rugina ["LV"]);
		parislevel = int.Parse (paris ["LV"]);
		 
		//獲得経験値
		plusexp = PlayerPrefs.GetInt("GetExp");

		//ユーザー用経験値
		userplusexp = plusexp + userexp;

		//ユニット用経験値
		seriaplusexp = plusexp + int.Parse (seria ["EXP"]);
		calplusexp = plusexp+ int.Parse (cal ["EXP"]);
		ruginaplusexp = plusexp + int.Parse (rugina ["EXP"]);
		parisplusexp = plusexp + int.Parse (paris ["EXP"]);

		//獲得経験値表示
		exptext.text = "獲得経験値　　　　" + plusexp;

	}
	
	// Update is called once per frame
	void Update () {

		//ユーザーレベルアップ
		if (userlevel < 50 && usertable [userlevel] <= userplusexp) {
			userplusexp -= usertable [userlevel];
			userlevel++;
			userlevelchabge = 1;
		}
		if (userlevelchabge == 1) {
			userleveluptext.text = "レベルが" + userlevel.ToString () + "になった！";
		} else {
			userleveluptext.text = "";
		}

		//UIに反映させる
		userleveltext.text = "Lv." + userlevel.ToString ();
		userafterexptext.text = "After" + (usertable [userlevel] - userplusexp).ToString ();
		afterexptext.text = "次のレベルまであと" + (usertable [userlevel] - userplusexp).ToString ();
		userpointtext.text = userpoint.ToString ();

		userexpslider.value = userplusexp;
		userexpslider.maxValue = usertable [userlevel];

		expslider.value = userplusexp;
		expslider.maxValue = usertable [userlevel];


		//ユニットレベルアップ

		//セリアレベルアップ
		if (serialevel < 50 && seriatable [serialevel] <= seriaplusexp) {
			seriaplusexp -= seriatable [serialevel];
			serialevel++;
			serialevelchabge = 1;
		}
		if (serialevelchabge == 1){
			serialeveluptext.text = "レベルが" + serialevel.ToString() + "になった！";
		}else {
			serialeveluptext.text = "";
		}

		//UIに反映させる
		seriaafterexptext.text = "次のレベルまであと" + (seriatable [serialevel] - seriaplusexp).ToString ();
		seriaexpslider.value = seriaplusexp;
		seriaexpslider.maxValue = seriatable [serialevel];


		//カルレベルアップ
		if (callevel < 50 && caltable [callevel] <= calplusexp) {
			calplusexp -= caltable [callevel];
			callevel++;
			callevelchabge = 1;
		}
		if (callevelchabge == 1){
			calleveluptext.text = "レベルが" + callevel.ToString() + "になった！";
		}else {
			calleveluptext.text = "";
		}

		//UIに反映させる
		calafterexptext.text = "次のレベルまであと" + (caltable [callevel] - calplusexp).ToString ();
		calexpslider.value = calplusexp;
		calexpslider.maxValue = caltable [callevel];


		//ルジーナレベルアップ
		if (ruginalevel < 50 && ruginatable [ruginalevel] <= ruginaplusexp) {
			ruginaplusexp -= ruginatable [ruginalevel];
			ruginalevel++;
			ruginalevelchabge = 1;
		}
		if (ruginalevelchabge == 1){
			ruginaleveluptext.text = "レベルが" + ruginalevel.ToString() + "になった！";
		}else {
			ruginaleveluptext.text = "";
		}

		//UIに反映させる
		ruginaafterexptext.text = "次のレベルまであと" + (ruginatable [ruginalevel] - ruginaplusexp).ToString ();
		ruginaexpslider.value = ruginaplusexp;
		ruginaexpslider.maxValue = ruginatable [ruginalevel];


		//パリスレベルアップ
		if (parislevel < 50 && paristable [parislevel] <= parisplusexp) {
			parisplusexp -= paristable [parislevel];
			parislevel++;
			parislevelchabge = 1;
		}
		if (parislevelchabge == 1){
			parisleveluptext.text = "レベルが" + parislevel.ToString() + "になった！";
		}else {
			parisleveluptext.text = "";
		}

		//UIに反映させる
		parisafterexptext.text = "次のレベルまであと" + (paristable [parislevel] - parisplusexp).ToString ();
		parisexpslider.value = parisplusexp;
		parisexpslider.maxValue = paristable [parislevel];
	
	}



	public void gohome(){

		if (serialevel != int.Parse (seria ["LV"])) {  //レベルアップした場合
			seria ["LV"] = serialevel.ToString ();
			seria ["EXP"] = seriaplusexp.ToString ();
			seria ["HP"] = (int.Parse (seria ["HP"]) + serialevel * 20).ToString (); 
			seria ["MP"] = (int.Parse (seria ["MP"]) + serialevel * 10).ToString ();
			seria ["ATK"] = (int.Parse (seria ["ATK"]) + serialevel * 5).ToString (); 
			seria ["DEF"] = (int.Parse (seria ["DEF"]) + serialevel * 5).ToString ();
		} else {    //しなかった場合
			seria ["EXP"] = (int.Parse (seria ["EXP"]) + plusexp).ToString ();
		}

		if (callevel != int.Parse (cal ["LV"])) {
			cal ["LV"] = callevel.ToString ();
			cal ["EXP"] = calplusexp.ToString ();
			cal ["HP"] = (int.Parse (cal ["HP"]) + callevel * 20).ToString (); 
			cal ["MP"] = (int.Parse (cal ["MP"]) + callevel * 10).ToString ();
			cal ["ATK"] = (int.Parse (cal ["ATK"]) + callevel * 5).ToString (); 
			cal ["DEF"] = (int.Parse (cal ["DEF"]) + callevel * 5).ToString ();
		}else {
			cal ["EXP"] = (int.Parse (cal ["EXP"]) + plusexp).ToString ();
		}

		if (ruginalevel != int.Parse (rugina ["LV"])) {
			rugina ["LV"] = ruginalevel.ToString ();
			rugina ["EXP"] = ruginaplusexp.ToString ();
			rugina ["HP"] = (int.Parse (rugina ["HP"]) + ruginalevel * 20).ToString (); 
			rugina ["MP"] = (int.Parse (rugina ["MP"]) + ruginalevel * 10).ToString ();
			rugina ["ATK"] = (int.Parse (rugina ["ATK"]) + ruginalevel * 5).ToString (); 
			rugina ["DEF"] = (int.Parse (rugina ["DEF"]) + ruginalevel * 5).ToString ();
		}else {
			rugina ["EXP"] = (int.Parse (rugina ["EXP"]) + plusexp).ToString ();
		}

		if (parislevel != int.Parse (paris ["LV"])) {
			paris ["LV"] = parislevel.ToString ();
			paris ["EXP"] = parisplusexp.ToString ();
			paris ["HP"] = (int.Parse (paris ["HP"]) + parislevel * 20).ToString (); 
			paris ["MP"] = (int.Parse (paris ["MP"]) + serialevel * 10).ToString ();
			paris ["ATK"] = (int.Parse (paris ["ATK"]) + parislevel * 5).ToString (); 
			paris ["DEF"] = (int.Parse (paris ["DEF"]) + parislevel * 5).ToString ();
		}else {
			paris ["EXP"] = (int.Parse (paris ["EXP"]) + plusexp).ToString ();
		}

	

		PlayerPrefsUtility.SaveDict<string, string> ("Seria", seria);
		PlayerPrefsUtility.SaveDict<string, string> ("Cal", cal);
		PlayerPrefsUtility.SaveDict<string, string> ("Rugina", rugina);
		PlayerPrefsUtility.SaveDict<string, string> ("Paris", paris);

		userexp = userplusexp;
		usermaxexp = usertable[userlevel];

		PlayerPrefs.SetInt ("userexp", userexp);
		PlayerPrefs.SetInt ("usermaxexp", usermaxexp);
		PlayerPrefs.SetInt ("userlv", userlevel);

		PlayerPrefs.Save ();

		SceneManager.LoadScene ("Home");
	}
}
