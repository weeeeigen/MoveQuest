using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour {

	public Slider expslider;
	public Text usernametext;
	public Text userpointtext;
	public Text userleveltext;
	public Text userafterexptext;

	public string userid;
	public string username;
	public int userpoint;
	public int userlevel;
	public int userexp;
	public int usermaxexp;
	public int userafterexp;

	public int stagevalue;

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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void backHome(){
		SceneManager.LoadScene ("Home");
	}

	public void goStage(int i){
		if (i == 1) {
			PlayerPrefs.SetInt ("Enemy", 1);
		}else if (i == 2) {
			PlayerPrefs.SetInt ("Enemy", 2);
		}else if (i == 3) {
			PlayerPrefs.SetInt ("Enemy", 3);
		}else if (i == 4) {
			PlayerPrefs.SetInt ("Enemy", 4);
		}else if (i == 5) {
			PlayerPrefs.SetInt ("Enemy", 5);
		}

		SceneManager.LoadScene ("Battle");

	}
}
