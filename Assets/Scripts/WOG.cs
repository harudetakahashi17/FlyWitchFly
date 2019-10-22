using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WOG : MonoBehaviour
{
    //Global variable
    [SerializeField] private Witch witch;

    private float speed;

    //Dipanggil setiap frame
    private void Update()
    {
        speed = witch.getMaxSpeed();
        //Melakukan pengecekan jika burung belum mati
        if (!witch.IsDead())
        {
            //Membuat pipa bergerak kesebelah kiri dengan kecepatan tertentu
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
    }

    //Membuat witch mati ketika bersentuhan dan menjatuhkannya ke ground jika mengenai di atas collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Witch witch = collision.gameObject.GetComponent<Witch>();

        //Pengecekan Null value
        if (witch)
        {
            //Mendapatkan komponent Collider pada game object
            Collider2D collider = GetComponent<Collider2D>();

            //Melakukan pengecekan Null varibale atau tidak
            if (collider)
            {
                //Menonaktifkan collider
                collider.enabled = false;
            }

            //Burung Mati
            witch.Dead();
        }
    }
}
