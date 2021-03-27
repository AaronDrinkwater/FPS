using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Game")]
    public Player player;
    public GameObject enemyObjects;

    [Header("UI")]
    public Text ammoText;
    public Text healthText;
    public Text enemyText;
    public Text infoText;

    private int initialEnemyCount;

    private float resetTimer = 3f;

    private bool playerKilled;
    private bool gameOver = false;
    public bool PlayerKilled { get => playerKilled; }

    void Start()
    {
        initialEnemyCount = enemyObjects.GetComponentsInChildren<Enemy>().Length;

        infoText.gameObject.SetActive(false);
    }
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    ammoText.text = "Ammo: " + player.Ammo;
        //}

        healthText.text = "Health: " + player.Health;
        ammoText.text = "Ammo: " + player.Ammo;

        //int killedEnimies = 0;
        int aliveEnimies = 0;

        foreach (Enemy enemy in GetComponentsInChildren<RangedEnemy>())
        {
            if(enemy.isKilled == false)
            {
                aliveEnimies++;
            }
        }

        enemyText.text = "Enimies: " + aliveEnimies;

        if(aliveEnimies == 0)
        {
            gameOver = true;
            infoText.gameObject.SetActive(true);
            infoText.text = "You Win Kid!\n";
        }

        if(player.PlayerKilled == true)
        {
            gameOver = true;
            infoText.gameObject.SetActive(true);
            infoText.text = "You Loose Kid!\n";
        }

        if(gameOver == true)
        {
            resetTimer -= Time.deltaTime;

            if(resetTimer <= 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
