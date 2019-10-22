using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WogSpawner : MonoBehaviour
{
    //Global variables
    [SerializeField] private Witch witch;
    [SerializeField] private WOG WOGUp, WOGDown;
    [SerializeField] private float spawnInterval = 1;
    [SerializeField] public float holeSize = 1f;
    [SerializeField] private float maxMinOffset = 1;
    [SerializeField] private Point point;
    [SerializeField] private DoublePoint doublepoint;

    //variable penampung coroutine yang sedang berjalan
    private Coroutine CR_Spawn;
    private void Start()
    {
        // Memulai Spawning 
        StartSpawn();
    }
    void StartSpawn()
    {
        //Menjalankan Fungsi Coroutine IeSpawn()
        if (CR_Spawn == null)
        {
            CR_Spawn = StartCoroutine(IeSpawn());
        }
    }

    void StopSpawn()
    {
        //Menhentikan Coroutine IeSpawn jika sebeumnya sudah di jalankan
        if (CR_Spawn != null)
        {
            StopCoroutine(CR_Spawn);
        }
    }

    void SpawnWOG()
    {
        //menduplikasi game object WOGUp dan menempatkan posisinya sama dengan game object ini tetapi dirotasi 180 derajat
        WOG newWOGUp = Instantiate(WOGUp, transform.position, Quaternion.Euler(0, 0, 180));

        //Mengaktifkan game object newWOGUp
        newWOGUp.gameObject.SetActive(true);

        //menduplikasi game object WOGDown dan menempatkan posisinya sama dengan game object
        WOG newWOGDown = Instantiate(WOGDown, transform.position, Quaternion.identity);

        //Mengaktifkan game object newWOGUp
        newWOGDown.gameObject.SetActive(true);

        //menempatkan posisi pipa yang telah dibentuk agar posisinya menyesuaikan dengan fungsi Sin
        
        float y;
        y = maxMinOffset * Mathf.Sin(Time.time) + maxMinOffset * Mathf.Cos(Time.time);
        

        newWOGUp.transform.position += Vector3.up * (holeSize / 2);
        newWOGDown.transform.position += Vector3.down * (holeSize / 2);
        newWOGUp.transform.position += Vector3.up * y;
        newWOGDown.transform.position += Vector3.up * y;

        float bonus = Random.Range(1,100);
        if (bonus % 5 == 0)
        {
            DoublePoint newDoublePoint = Instantiate(doublepoint, transform.position, Quaternion.identity);
            newDoublePoint.gameObject.SetActive(true);
            newDoublePoint.transform.position += Vector3.up * y;
        } else
        {
            Point newPoint = Instantiate(point, transform.position, Quaternion.identity);
            newPoint.gameObject.SetActive(true);
            newPoint.SetSize(holeSize);
            newPoint.transform.position += Vector3.up * y;
        }
    }

    IEnumerator IeSpawn()
    {
        while (true)
        {
            //Jika Burung mati maka menhentikan pembuatan Pipa Baru
            if (witch.IsDead())
            {
                StopSpawn();
            }

            //Membuat Pipa Baru
            SpawnWOG();

            //Menunggu beberapa detik sesuai dengan spawn interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}