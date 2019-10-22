using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Witch : MonoBehaviour
{
    //Global Variables
    [SerializeField] private float upForce = 115;
    [SerializeField] private bool isDead;
    [SerializeField] private UnityEvent OnJump, OnDead;
    [SerializeField] private int score;
    [SerializeField] private UnityEvent OnAddPoint;
    [SerializeField] private Text scoreText;

    private Rigidbody2D rigidBody2d;
    private Animator animator;
    private float maxSpeed = 1;

    //init variable
    void Start()
    {
        //Mendapatkan komponent ketika game baru berjalan
        rigidBody2d = GetComponent<Rigidbody2D>();
        //Mendapatkan komponen animator pada game object   
        animator = GetComponent<Animator>();
    }
    //Update setiap frame  
    void Update()
    {
        //Melakukan pengecekan jika belum mati dan klik kiri pada mouse
        if (!isDead && Input.GetMouseButtonDown(0))
        {
            //Burung meloncat
            Jump();
        }
    }
    //Fungsi untuk mengecek sudah mati apa belum
    public bool IsDead()
    {
        return isDead;
    }

    //Membuat Burung Mati
    public void Dead()
    {
        //Pengecekan jika belum mati dan value OnDead tidak sama dengan Null
        if (!isDead && OnDead != null)
        {
            //Memanggil semua event pada OnDead
            OnDead.Invoke();
        }

        //Mengeset variable Dead menjadi True
        isDead = true;

    }

    void Jump()
    {
        //Mengecek rigidbody null atau tidak
        if (rigidBody2d)
        {
            //menghentikan kecepatan burung ketika jatuh
            rigidBody2d.velocity = Vector2.zero;

            //Menambahkan gaya ke arah sumbu y agar burung meloncat
            rigidBody2d.AddForce(new Vector2(0, upForce));
        }

        //Pengecekan Null variable
        if (OnJump != null)
        {
            //Menjalankan semua event OnJump event
            OnJump.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //menghentikan Animasi Burung ketika bersentukan dengan object lain
        animator.enabled = false;
    }

    public void AddScore(int value)
    {
        if(score % 10 == 0)
        {
            maxSpeed += 0.1f;
        }
        //Menambahkan Score value
        score += value;
        //Mengubah nilai text pada score text
        scoreText.text = score.ToString();

        //Pengecekan Null Value
        if (OnAddPoint != null)
        {
            //Memanggil semua event pada OnAddPoint
            OnAddPoint.Invoke();
        }
    }

    public int getScore()
    {
        return score;
    }

    public float getMaxSpeed()
    {
        return maxSpeed;
    }
}
