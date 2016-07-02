using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {

	//UIパーツ
	public Button seriaButton, calButton, ruginaButton, parisButton;
	public Button c1, c2, c3, c4;
	public Slider enemyhpSlider;     
	public Slider seriahpSlider, calhpSlider, ruginahpSlider, parishpSlider; 
	public Slider seriampSlider, calmpSlider, ruginampSlider, parismpSlider;
	public Text log;
	public Image enemyImage;
	public Sprite enemySprite;

	public Text seriahpText;
	public Text seriampText;
	public Text calhpText;
	public Text calmpText;
	public Text ruginahpText;
	public Text ruginampText;
	public Text parishpText;
	public Text parismpText;

	public Text seriacommandText;
	public Text calcommandText;
	public Text ruginacommandText;
	public Text pariscommandText;

	public Text enemynameText;
	public Text enemyhpText;

	//ユニットデータ
	public string[] uname = { "セリア", "カル", "ルジーナ", "パリス"};
	public int[] uhp;
	public int[] ump;
	public int[] ulv = { 0, 0, 0, 0 };
	public int[] ubb;
	public int[] usbb;
	public int[] uatk;
	public int[] udef;

	public string[] ucommand;
	public string[,] allcommand = new string[4, 5]{ 
		{"sskill", "sattack", "sbb", "ssbb", "sdead"}, 
		{"cskill", "cattack", "cbb", "csbb", "cdead"}, 
		{"rskill", "rattack", "rbb", "rsbb", "rdead"}, 
		{"pskill", "pattack", "pbb", "psbb", "pdead"} 
	};

	public string[] ucommandlog;
	public string[,] allcommandlog = new string[4, 4]{
		{"味方全体のHPを回復!!", "敵に攻撃!!", "敵に強力な攻撃!!", "敵に超強力な攻撃!!\n味方全体のHPを大回復!!"}, 
		{"味方全体の攻撃力&防御力をアップ!!", "敵に攻撃!!", "敵に強力な攻撃!!", "敵に超強力な攻撃!!\n味方全体の攻撃力&防御力大アップ!!\","}, 
		{"味方全体の攻撃力を大アップ", "敵に攻撃!!", "敵に強力な攻撃!!", "敵に超強力な攻撃!!\n味方全体の攻撃力を超アップ!!"}, 
		{"味方全体の防御力を大アップ!!", "敵に攻撃!!", "敵に強力な攻撃!!", "敵に超強力な攻撃!!\n味方全体の防御力を超アップ!!\","}
	};

	public string[] ucommandtext;
	public string[,] allcommandtext = new string[4, 5]{ 
		{"SKILL", "ATTACK", "BB", "SBB", "SDEAD"},
		{"SKILL", "ATTACK", "BB", "SBB", "CDEAD"},
		{"SKILL", "ATTACK", "BB", "SBB", "RDEAD"},
		{"SKILL", "ATTACK", "BB", "SBB", "PDEAD"}
	};

	public int[] atkbuff = { 0, 0, 0, 0 };
	public int[] defbuff = { 0, 0, 0, 0 };
	public int[] timing =  { 0, 0, 0, 0 };

	public Dictionary<string, string> seria;
	public Dictionary<string, string> cal;
	public Dictionary<string, string> rugina;
	public Dictionary<string, string> paris;

	public int enemynum;
	public string enemyname;
	public int enemyhp;
	public int enemymaxhp;

	public int[] enemyrandomcommand;
	public int[] unitrandom;

	public int getexp;


	// Use this for initialization
	void Start () {


		//選択された敵の情報を取り出す
		enemynum = PlayerPrefs.GetInt ("Enemy");

		//enemynum = 4;

		//敵の情報をセッティングする
		if (enemynum == 1) {
			enemyname = "創造神マクスウェル";
			enemyhp = 6000;
			enemySprite = Resources.Load<Sprite> ("Enemy/enemy001");
			getexp = 10;
		} else if (enemynum == 2) {
			enemyname = "魔統神カルデス";
			enemyhp = 7000;
			enemySprite = Resources.Load<Sprite> ("Enemy/enemy002");
			getexp = 20;
		} else if (enemynum == 3) {
			enemyname = "神界帝セヴァルア";
			enemyhp = 8000;
			enemySprite = Resources.Load<Sprite> ("Enemy/enemy003");
			getexp = 30;
		} else if (enemynum == 4) {
			enemyname = "真神アフラ・ディリス";
			enemyhp = 9000;
			enemySprite = Resources.Load<Sprite> ("Enemy/enemy004");
			getexp = 40;
		} else if (enemynum == 5) {
			enemyname = "封神ルシアス";
			enemyhp = 10000;
			enemySprite = Resources.Load<Sprite> ("Enemy/enemy005");
			getexp = 50;
		}

		//選択されたキャラのイメージ、名前に変更する
		enemyImage.sprite = enemySprite;
		enemynameText.text = enemyname;
	

		//ユニットデータを取り出す
		seria = PlayerPrefsUtility.LoadDict<string, string> ("Seria");
		cal = PlayerPrefsUtility.LoadDict<string, string> ("Cal");
		rugina = PlayerPrefsUtility.LoadDict<string, string> ("Rugina");
		paris = PlayerPrefsUtility.LoadDict<string, string> ("Paris");

		//ユニットのHPを配列に入れて管理する
		uhp [0] = int.Parse(seria ["HP"]);
		uhp [1] = int.Parse(cal ["HP"]);
		uhp [2] = int.Parse(rugina ["HP"]);
		uhp [3] = int.Parse(paris ["HP"]);

		//ユニットのMPを配列に入れて管理する
		ump [0] = int.Parse(seria ["MP"]);
		ump [1] = int.Parse(cal ["MP"]);
		ump [2] = int.Parse(rugina ["MP"]);
		ump [3] = int.Parse(paris ["MP"]);

		//ユニットのLVを配列に入れて管理する
		ulv [0] = int.Parse(seria ["LV"]);
		ulv [1] = int.Parse(cal ["LV"]);
		ulv [2] = int.Parse(rugina ["LV"]);
		ulv [3] = int.Parse(paris ["LV"]);

		//ユニットのBBを配列に入れて管理する
		ubb [0] = int.Parse(seria ["BBLV"]);
		ubb [1] = int.Parse(cal ["BBLV"]);
		ubb [2] = int.Parse(rugina ["BBLV"]);
		ubb [3] = int.Parse(paris ["BBLV"]);

		//ユニットのSBBを配列に入れて管理する
		usbb [0] = int.Parse(seria ["SBBLV"]);
		usbb [1] = int.Parse(cal ["SBBLV"]);
		usbb [2] = int.Parse(rugina ["SBBLV"]);
		usbb [3] = int.Parse(paris ["SBBLV"]);

		//ユニットのATKを配列に入れて管理する
		uatk [0] = int.Parse(seria ["ATK"]);
		uatk [1] = int.Parse(cal ["ATK"]);
		uatk [2] = int.Parse(rugina ["ATK"]);
		uatk [3] = int.Parse(paris ["ATK"]);

		//ユニットのDEFを配列に入れて管理する
		udef [0] = int.Parse(seria ["DEF"]);
		udef [1] = int.Parse(cal ["DEF"]);
		udef [2] = int.Parse(rugina ["DEF"]);
		udef [3] = int.Parse(paris ["DEF"]);


		//UIに反映させる
		enemyhpSlider.value = enemyhp;
		enemymaxhp = enemyhp;
		enemyhpSlider.maxValue = enemymaxhp;

		seriahpSlider.value = uhp[0];
		calhpSlider.value = uhp[1];
		ruginahpSlider.value = uhp[2];
		parishpSlider.value = uhp[3];

		seriahpSlider.maxValue = uhp[0];
		calhpSlider.maxValue = uhp[1];
		ruginahpSlider.maxValue = uhp[2];
		parishpSlider.maxValue = uhp[3];

		seriampSlider.value = ump[0];
		calmpSlider.value = ump[1];
		ruginampSlider.value = ump[2];
		parismpSlider.value = ump[3];

		seriampSlider.maxValue = ump[0];
		calmpSlider.maxValue = ump[1];
		ruginampSlider.maxValue = ump[2];
		parismpSlider.maxValue = ump[3];
	
	}

	// Update is called once per frame
	void Update () {
		
		//選ばれたコマンドをUIに反映する
		seriacommandText.text = ucommandtext [0];
		calcommandText.text = ucommandtext [1];
		ruginacommandText.text = ucommandtext [2];
		pariscommandText.text = ucommandtext [3];


		//ユニット攻撃開始
		if (timing [0] == 2 && timing [1] == 2 && timing [2] == 2 && timing [3] == 2) {

			//コルーチンで1秒ごとに攻撃行う
			StartCoroutine("command");

			//ボタンを使えないようにする
			seriaButton.interactable = false;
			calButton.interactable = false;
			ruginaButton.interactable = false;
			parisButton.interactable = false;

			//フラグをリセット
			timingreset();
		}

		//HPが0より小さくなったらボタンを押せないようにする
		if (uhp [0] <= 0) {
			seriaButton.interactable = false;
			ucommand[0] = "sdead";
			timing [0] = 2;
		}

		if (uhp [1] <= 0) {
			calButton.interactable = false;
			ucommand[1] = "cdead";
			timing [1] = 2;
		}

		if (uhp [2] <= 0) {
			ruginaButton.interactable = false;
			ucommand[2] = "rdead";
			timing [2] = 2;
		}

		if (uhp [3] <= 0) {
			parisButton.interactable = false;
			ucommand[3] = "pdead";
			timing [3] = 2;
		}


		//全員のhpがなくなったらゲームオーバー画面に移行する
		if (uhp [0] < 0 && uhp [1] < 0 && uhp [2] < 0 && uhp [3] < 0) {
			SceneManager.LoadScene ("GameOver");
		}


		//敵のhpがなくなったらクリア画面に移動する
		if(enemyhpSlider.value == 0) {
			PlayerPrefs.SetInt ("GetExp", getexp);
			SceneManager.LoadScene ("Crear");
		}


		//敵、味方のhpを常時反映する
		enemyhpText.text = enemyhp.ToString () + "/" + enemymaxhp.ToString ();

		seriahpText.text = uhp[0] + "/" + int.Parse(seria ["HP"]);
		seriampText.text = ump[0] + "/" + int.Parse(seria ["MP"]);
		calhpText.text = uhp[1] + "/" + int.Parse(cal ["HP"]);
		calmpText.text = ump[1] + "/" + int.Parse(cal ["MP"]);
		ruginahpText.text = uhp[2] + "/" + int.Parse(rugina ["HP"]);
		ruginampText.text = ump[2] + "/" + int.Parse(rugina ["MP"]);
		parishpText.text = uhp[3] + "/" + int.Parse(paris ["HP"]);
		parismpText.text = ump[3] + "/" + int.Parse(paris ["MP"]);
	}


	//ユニットボタンを押したとき
	public void chooseunit(int num){
		timing [num] = 1; 

		//MPが足りない時
		if(ump[num] < ulv[num] * 7){
			c4.interactable = false;
			if(ump[num] < ulv[num] * 5){
				c3.interactable = false;
				if (ump [num] < ulv [num] * 3) {
					c2.interactable = false;
					if (ump [num] < ulv [num]) {
						c1.interactable = false;
						timing [num] = 2;
					}
				}
			}
		}

		//変更するとき
		for (int i=0; i<4; i++) {
			if (i != num && i != 2 && timing [i] == 1) {
				timing [i] = 0;
			}
		}
	}

	//コマンドボタンを押した時
	public void choosecommand(int  commandnum){
		for (int i = 0; i < 4; i++) {
			if (timing [i] == 1) {
				switch (commandnum) {
				case 1:
					ucommand [i] = allcommand [i, 0];
					log.text = allcommandlog [i, 0];
					ucommandtext [i] = allcommandtext [i, 0];
					timing [i] = 2;
					break;
				case 2:
					ucommand[i] = allcommand [i, 1];
					log.text = allcommandlog[i, 1];
					ucommandtext [i] = allcommandtext [i, 1];
					timing [i] = 2;
					break;
				case 3:
					ucommand[i] = allcommand [i, 2];
					log.text = allcommandlog[i, 2];
					ucommandtext [i] = allcommandtext [i, 2];
					timing [i] = 2;
					break;
				case 4:
					ucommand[i] = allcommand [i, 3];
					log.text = allcommandlog[i, 3];
					ucommandtext [i] = allcommandtext [i, 3];
					timing [i] = 2;
					break;
				}
			}
		}
	}
	

	//1秒ごとに攻撃、そのあと相手の攻撃
	public IEnumerator command(){

		//味方の攻撃
		for (int i=0; i<4; i++) {
			if (ucommand [i] == "sskill") {
				sskill ();
			}else if (ucommand [i] == "sattack") {
				sattack ();
			}else if (ucommand [i] == "sbb") {
				sbb ();
			}else if (ucommand [i] == "ssbb") {
				ssbb ();
			}else if (ucommand [i] == "sdead") {
				sdead ();
			}else if (ucommand [i] == "cskill") {
				cskill ();
			}else if (ucommand [i] == "cattack") {
				cattack ();
			}else if (ucommand [i] == "cbb") {
				cbb ();
			}else if (ucommand [i] == "csbb") {
				csbb ();
			}else if (ucommand [i] == "cdead") {
				cdead ();
			}else if (ucommand [i] == "rskill") {
				rskill ();
			}else if (ucommand [i] == "rattack") {
				rattack ();
			}else if (ucommand [i] == "rbb") {
				rbb ();
			}else if (ucommand [i] == "rsbb") {
				rsbb ();
			}else if (ucommand [i] == "rdead") {
				rdead ();
			}else if (ucommand [i] == "pskill") {
				pskill ();
			}else if (ucommand [i] == "pattack") {
				pattack ();
			}else if (ucommand [i] == "pbb") {
				pbb ();
			}else if (ucommand [i] == "psbb") {
				psbb ();
			}else if (ucommand [i] == "pdead") {
				pdead ();
			}

			//値をUIに反映する
			uimatch();
			buffplus ();

			yield return new WaitForSeconds(1f);

		}
			
		//敵の攻撃
		for(int i=0; i<3; i++){
			int rannum = Random.Range (1, 5);
			int rannum2 = Random.Range (0, 4);
			enemyrandomcommand [i] = rannum;
			unitrandom [i] = rannum2;
		}

		for (int i = 0; i < 3; i++) {
			if (enemynum == 1) {
				enemy1 (enemyrandomcommand[i], unitrandom [i]);
			}else if (enemynum == 2) {
				enemy2 (enemyrandomcommand[i], unitrandom [i]);
			}else if (enemynum == 3) {
				enemy3 (enemyrandomcommand[i], unitrandom [i]);
			}else if (enemynum == 4) {
				enemy4 (enemyrandomcommand[i], unitrandom [i]);
			}else if (enemynum == 5) {
				enemy5 (enemyrandomcommand[i], unitrandom [i]);
			}

			uimatch ();
			buffplus ();

			yield return new WaitForSeconds(1f);
		}


		//ボタンを使えるようにする
		seriaButton.interactable = true;
		calButton.interactable = true;
		ruginaButton.interactable = true;
		parisButton.interactable = true;

		for(int i=0; i < 4; i++){
			ucommandtext [i] = "";
		}
	}


	// UIに反映させる
	public void uimatch(){
		
		if (uhp [0] > int.Parse (seria ["HP"]) || ump [0] > int.Parse (seria ["MP"])) {
			uhp [0] = int.Parse (seria ["HP"]);
			ump [0] = int.Parse (seria ["MP"]);
		}

		if (uhp [1] > int.Parse (cal ["HP"]) || ump [1] > int.Parse (cal ["MP"])) {
			uhp [1] = int.Parse (cal ["HP"]);
			ump [1] = int.Parse (cal ["MP"]);
		}

		if (uhp [2] > int.Parse (rugina ["HP"]) || ump [2] > int.Parse (rugina ["MP"])) {
			uhp [2] = int.Parse (rugina ["HP"]);
			ump [2] = int.Parse (rugina ["MP"]);
		}

		if (uhp [3] > int.Parse (paris ["HP"]) || ump [3] > int.Parse (paris ["MP"])) {
			uhp [3] = int.Parse (paris ["HP"]);
			ump [3] = int.Parse (paris ["MP"]);
		}

		if (enemyhp > enemymaxhp) {
			enemyhp = enemymaxhp;
		}

		seriahpSlider.value = uhp[0];
		calhpSlider.value = uhp[1];
		ruginahpSlider.value = uhp[2];
		parishpSlider.value = uhp[3];

	
		seriampSlider.value = ump[0];
		calmpSlider.value = ump[1];
		ruginampSlider.value = ump[2];
		parismpSlider.value = ump[3];

		enemyhpSlider.value = enemyhp;

	}

	//バフを反映させる
	public void buffplus(){
		for (int i = 0; i < 4; i++) { 
			uatk[i] += atkbuff [i];
			udef[i] += defbuff [i];
		}
	}

	//フラグをリセットする
	public void timingreset(){
		for (int i = 0; i < 4; i++) {
			timing [i] = 0;
		}
	}


	//味方の攻撃パターン
	public void sskill(){
		log.text = "セリアはスキルを使った!!\n" + "味方の攻撃力アップ!!\n" + "HP回復!!";
		for (int i = 0; i < 4; i++) {
			uhp [i] += ulv [0] * 5; 
		}
		ump[0] -= ulv[0] * 3;

	}

	public void sattack(){
		int damage = uatk [0];
		log.text = "セリアの攻撃!!\n" + "敵に " + damage + "のダメージ!";
		enemyhp -= damage;
	}

	public void sbb(){
		int damage = ( uatk[0] + ubb[0] ) * 2;
		log.text = "セリアの攻撃!!\n" + "敵に " + damage + "のダメージ!";
		enemyhp -= damage;
		ump[0] -= ulv[0] * 5;
	}

	public void ssbb(){
		int damage = ( uatk[0] + usbb[0] ) * 3;
		log.text = "セリアの攻撃!!\n" + "敵に " + damage + "のダメージ!\n" + "味方のHP大回復!!";
		enemyhp -= damage;
		ump[0] -= ulv[0] * 7;
		for (int i = 0; i < 4; i++) {
			uhp [i] += ulv [0] * 10; 
		}
	}

	public void sdead(){
		log.text = "セリアは倒れている";
	}


	public void cskill(){
		log.text = "カルはスキルを使った!!\n" + "味方の攻撃力アップ!!\n" + "味方の防御力アップ!!";
		for (int i = 0; i < 4; i++) { 
			atkbuff [i] += ulv [0] * 5;
			defbuff [i] += ulv [0] * 5;
		}
		ump[1] -= ulv[1] * 3;
	}

	public void cattack(){
		int damage = uatk [1];
		log.text = "カルの攻撃!!\n" + "敵に " + damage + "のダメージ!";
		enemyhp -= damage;
	}

	public void cbb(){
		int damage = ( uatk[1] + ubb[1] ) * 2;
		log.text = "カルの攻撃!!\n" + "敵に " + damage + "のダメージ!";
		enemyhp -= damage;
		ump[1] -= ulv[1] * 5;
	}

	public void csbb(){
		int damage = ( uatk[1] + usbb[1] ) * 3;
		log.text = "カルの攻撃!!\n" + "敵に " + damage + "のダメージ!\n" + "味方の攻撃力、防御力大アップ!!";
		enemyhp -= damage;
		ump[1] -= ulv[1] * 7;
		for (int i = 0; i < 4; i++) {
			atkbuff [i] += ulv [1] * 7;
			defbuff [i] += ulv [1] * 7;
		}
	}

	public void cdead(){
		log.text = "カルは倒れている";
	}


	public void rskill(){
		log.text = "ルジーナはスキルを使った!!\n" + "味方の攻撃力を大幅にアップ!!";
		for (int i = 0; i < 4; i++) { 
			atkbuff [i] += ulv [2] * 7;
		}
		ump[2] -= ulv[2] * 3;
	}

	public void rattack(){
		int damage = uatk [2];
		log.text = "ルジーナの攻撃!!\n" + "敵に " + damage + "のダメージ!";
		enemyhp -= damage;
	}

	public void rbb(){
		int damage = ( uatk[2] + ubb[2] ) * 2;
		log.text = "ルジーナの攻撃!!\n" + "敵に " + damage + "のダメージ!";
		enemyhp -= damage;
		ump[2] -= ulv[2] * 5;
	}

	public void rsbb(){
		int damage = ( uatk[2] + usbb[2] ) * 3;
		log.text = "ルジーナの攻撃!!\n" + "敵に " + damage + "のダメージ!\n" + "味方の攻撃力大アップ!!";
		enemyhp -= damage;
		ump[2] -= ulv[2] * 7;
		for (int i = 0; i < 4; i++) {
			atkbuff [i] += ulv [3] * 10; 
		}
	}

	public void rdead(){
		log.text = "ルジーナは倒れている";
	}

	public void pskill(){
		log.text = "パリスはスキルを使った!!\n" + "味方の防御力を大幅にアップ!!";
		for (int i = 0; i < 4; i++) { 
			defbuff [i] += ulv [3] * 7;
		}
		ump[3] -= ulv[3] * 3;
	}

	public void pattack(){
		int damage = uatk [3];
		log.text = "パリスの攻撃!!\n" + "敵に " + damage + "のダメージ!";
		enemyhp -= damage;
	}

	public void pbb(){
		int damage = ( uatk[3] + ubb[3] ) * 2;
		log.text = "パリスの攻撃!!\n" + "敵に " + damage + "のダメージ!";
		enemyhp -= damage;
		ump[3] -= ulv[3] * 5;
	}

	public void psbb(){
		int damage = ( uatk[3] + usbb[3] ) * 3;
		log.text = "パリスの攻撃!!\n" + "敵に " + damage + "のダメージ!\n" + "味方の防御力大アップ!!";
		enemyhp -= damage;
		ump[3] -= ulv[3] * 7;
		for (int i = 0; i < 4; i++) {
			defbuff [i] += ulv [3] * 10;
		}
	}
		
	public void pdead(){
		log.text = "パリスは倒れている";
	}


	//敵の攻撃パターン
	public void enemy1(int i, int j){
		if (i == 1 || i == 2) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + uname[j].ToString() + "に20のダメージ!";
			uhp [j] -= 20;
		}
		if (i == 3 || i ==4) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + "味方全体に10のダメージ!";
			for (int k = 0; k < 4; k++) {
				uhp [k] -= 10;
			}
		}
	}

	public void enemy2(int i, int j){
		if (i == 1) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + uname[j].ToString() + "に80のダメージ!";
			uhp [j] -= 80;
		}
		if (i == 2) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + "味方全体に60のダメージ!";
			for (int k = 0; k < 4; k++) {
				uhp [k] -= 60;
			}
		}
		if (i == 3 || i == 4) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + "味方全体のバフが消された！";
			for (int l = 0; l < 4; l++) {
				defbuff [l] = 0;
				atkbuff [l] = 0;
			}
		}
	}

	public void enemy3(int i, int j){
		if (i == 1) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + uname[j].ToString() + "に100のダメージ!";
			uhp [j] -= 100;
		}
		if (i == 2) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + "味方全体に80のダメージ!";
			for (int k = 0; k < 4; k++) {
				uhp [k] -= 80;
			}
		}
		if (i == 3) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + "味方全体のバフが消された！";
			for (int l = 0; l < 4; l++) {
				defbuff [l] = 0;
				atkbuff [l] = 0;
			}
		}
		if (i == 4) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + enemyname.ToString () + "はHPを回復した！";
			enemyhp += 600;
		}
	}

	public void enemy4(int i, int j){
		if (i == 1) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + uname[j].ToString() + "に100のダメージ!";
			uhp [j] -= 100;
		}
		if (i == 2) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + "味方全体に80のダメージ!";
			for (int k = 0; k < 4; k++) {
				uhp [k] -= 80;
			}
		}
		if (i == 3) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + "味方全体のバフが消された！";
			for (int l = 0; l < 4; l++) {
				defbuff [l] = 0;
				atkbuff [l] = 0;
			}
		}
		if (i == 4) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + "味方全体のMPが減少した！";
			for (int n = 0; n < 4; n++) {
				ump [n] -= 20;
			}
		}
	}

	public void enemy5(int i, int j){
		if (i == 1) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + uname[j].ToString() + "に150のダメージ!";
			uhp [j] -= 150;
		}
		if (i == 2) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + "味方全体に100のダメージ!";
			for (int k = 0; k < 4; k++) {
				uhp [k] -= 100;
			}
		}
		if (i == 3) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + "味方全体のバフが消された！";
			for (int l = 0; l < 4; l++) {
				defbuff [l] = 0;
				atkbuff [l] = 0;
			}
		}
		if (i == 4) {
			log.text = enemyname.ToString () + "の攻撃!!\n" + "味方全体のMPが減少した！\n" + enemyname.ToString () + "はHPを回復した！";
			for (int n = 0; n < 4; n++) {
				ump [n] -= 30;
			}
			enemyhp += 1000;
		}
	}
		
}
