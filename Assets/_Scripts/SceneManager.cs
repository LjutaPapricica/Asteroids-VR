using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

    public GameObject[] asteroids;
    public GameObject[] lifes;
    public GameObject scoreValue;
    public GameObject gameOverPanel;
    public GameObject gameOverScore;

    public bool gameOver = false;
    int score = 0;

    void Start() {
        Input.backButtonLeavesApp = true;
        StartCoroutine("SpawnAsteroid");
    }

    void Update() {
        // Exit when (X) is tapped.
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        //Update score
        scoreValue.GetComponent<Text>().text = "" + score;
    }

    

    public IEnumerator SpawnAsteroid() {
        while (true) {
            Vector3 randomPosition = new Vector3(Random.Range(10, 30), Random.Range(10, 30), Random.Range(10, 30));
            Instantiate(asteroids[Random.Range(0, 3)], randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(2f);
        }
    }

    public void AddScore(int points) {
        score += points;
    }

    public void SubHeart() {
        if (lifes[2].activeInHierarchy)
            lifes[2].SetActive(false);
        else if (lifes[1].activeInHierarchy)
            lifes[1].SetActive(false);
        else {
            gameOver = true;
            lifes[0].SetActive(false);
            gameOverPanel.SetActive(true);
            gameOverScore.GetComponent<Text> ().text = "" + score;
            Time.timeScale = 0;
        }
    }
}
