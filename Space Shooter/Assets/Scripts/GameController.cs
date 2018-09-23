using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public float powerUpTimer;
	private float powerConstant;
	public GameObject powerUp;
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public int hazardCount;
	private int score;
	public Text scoreText, restartText, gameOverText;
	private bool gameOver, restart;
	// Use this for initialization
	void Start () {
		powerConstant = powerUpTimer;
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore();
		StartCoroutine(SpawnWaves());
	}
	
	void Update(){
		if(restart){
			if(Input.GetKeyDown(KeyCode.R)){
				SceneManager.LoadScene("Main");
			}
		}
	}
	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds(startWait);
		while(true){
			for(int i=0; i<hazardCount; i++){
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 0.0f, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				
				Instantiate(hazard, spawnPosition, spawnRotation);
				if(gameOver) {
					break;
				}
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
			if(gameOver){
				restartText.text = "Press 'R' to restart";
				restart = true;
				break;
			}
			if(powerUpTimer == 0){
				Vector3 spawnPower = new Vector3(Random.Range
				(-spawnValues.x, spawnValues.x), 0.0f, Random.Range(-4, 14));
				Instantiate(powerUp, spawnPower, Quaternion.identity);
				powerUpTimer = powerConstant;
			}
		}
	}

	public void AddScore(int newScoreValue){
		score += newScoreValue;
		UpdateScore();
	}
	void UpdateScore () {
		scoreText.text = "Score: " + score;
	}
	public void GameOver(){
		gameOver = true;
		gameOverText.text = "Game Over";
		restartText.text = "Press 'R' to restart";
		restart = true;
	}
}
