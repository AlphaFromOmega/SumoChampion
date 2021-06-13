using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;

    public GameObject enemyUIParent;
    public GameObject ememyCount;
    public GameObject enemyBar;

    public GameObject waveUIParent;
    public GameObject waveTimer;

    public GameObject hudWaveText;
    public GameObject hudKillsText;
    public GameObject hudMoneyText;

    private int spawnEnemies = 1;

    private int wave = 0;
    private float spawnRange = 10;
    private float maxEnemies = 16;
    private float enemies = 8;
    private float maxSpawnEnemies = 8;

    public int kills = 0;
    public int money = 0;

    private int timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaveCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        hudWaveText.GetComponent<Text>().text = $"Wave: {wave}";
        hudKillsText.GetComponent<Text>().text = $"Kills: {kills}";
        hudMoneyText.GetComponent<Text>().text = $"§{money}";
    }

    private void SpawnWave(int amount)
    {
        wave++;
        for (var i = 0; i < amount; i++)
        {
            int randomPrefab = Random.Range(0, enemyPrefabs.Count);
            Instantiate(enemyPrefabs[randomPrefab], new Vector3(RandomSpawnRange(), 2, RandomSpawnRange() + 50), enemyPrefabs[randomPrefab].transform.rotation);
        }
        maxEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemies = maxEnemies;
        spawnEnemies += 2;
        UpdateEnemies();
    }

    private float RandomSpawnRange()
    {
        return Random.Range(-spawnRange, spawnRange);
    }

    public void UpdateEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyBar.GetComponent<Slider>().value = enemies / maxEnemies;
        ememyCount.GetComponent<Text>().text = (enemies == 1) ? $"{enemies} Enemy Left" : $"{enemies} Enemies Left";
        if (enemies == 0 && timer == 0)
        {
            StartCoroutine(WaveCooldown());
        }
    }

    IEnumerator WaveCooldown()
    {
        timer = 5;
        enemyUIParent.SetActive(false);
        waveUIParent.SetActive(true);
        while (timer > 0)
        {
            waveTimer.GetComponent<Text>().text = "Next Wave in: " + timer;
            yield return new WaitForSeconds(1);
            timer--;
        }
        enemyUIParent.SetActive(true);
        waveUIParent.SetActive(false);
        SpawnWave(spawnEnemies);
    }
}
