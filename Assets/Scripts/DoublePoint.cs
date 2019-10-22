using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class DoublePoint : MonoBehaviour
{
    [SerializeField] private Witch witch;
    private float speed;
    void Update()
    {
        speed = witch.getMaxSpeed();
        //Melakukan pengecekan burung mati atau tidak
        if (!witch.IsDead())
        {
            //menggerakan game object kesebelah kiri dengan kecepatan tertentu
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        //Mendapatkan komponen Witch
        Witch witch = collision.gameObject.GetComponent<Witch>();
        //Menambahkan score jika burung tidak null dan burung belum mati;
        if (witch && !witch.IsDead() && gameObject.activeSelf == true)
        {
            witch.AddScore(2);
            gameObject.SetActive(false);
        }
    }
}