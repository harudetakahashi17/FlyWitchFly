using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Point : MonoBehaviour
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

    public void SetSize(float size)
    {
        //Mendapatkan komponent BoxCollider2D
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        //Pengecekan Null variable
        if (collider != null)
        {
            //mengubah ukuran collider sesuai dengan paramater
            collider.size = new Vector2(collider.size.x, size);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //Mendapatkan komponen Witch
        Witch witch = collision.gameObject.GetComponent<Witch>();
        //Menambahkan score jika burung tidak null dan burung belum mati;
        if (witch && !witch.IsDead())
        {
            witch.AddScore(1);
        }
    }
}