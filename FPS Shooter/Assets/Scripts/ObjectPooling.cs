using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private static ObjectPooling instance;

    public static ObjectPooling Instance { get { return instance; } }

    public GameObject bulletPrefab;
    public int bulletAmount = 24;


    private List<GameObject> bullets;
    void Awake()
    {
        instance = this;

        //Preload bullets
        bullets = new List<GameObject>(bulletAmount); //generic list of bullet gameobjects with a capicity of bullet amount

        for(int i = 0; i < bulletAmount; i++)
        {
            GameObject prefabInstance = Instantiate(bulletPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            bullets.Add(prefabInstance);
        }

    }

    public GameObject GetBullet(bool shotByPlayer)
    {
        foreach(GameObject bullet in bullets)
        {
            if(!bullet.activeInHierarchy)
            {
                bullet.SetActive (true);
                bullet.GetComponent<Bullet>().ShotByPlayer = shotByPlayer;

                return bullet;
            }
        }

        GameObject prefabInstance = Instantiate(bulletPrefab);
        prefabInstance.transform.SetParent(transform);
        prefabInstance.GetComponent<Bullet>().ShotByPlayer = shotByPlayer;
        bullets.Add(prefabInstance);

        return prefabInstance;
    }
  
}
