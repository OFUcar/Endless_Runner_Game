using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float moveSpeed = 50f;
    public float jumpSpeed;
    public float jumpFall;
    public float nextJumpTime;
    public float gravityScale;
    public float xHorizontal;
    public float zVertical;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        gravityScale = 20f;

    }

    void FixedUpdate()
    {

        //Movement
        Vector3 moveSystem = new Vector3(xHorizontal, 0, zVertical);
        _rigidbody.MovePosition(transform.position + moveSystem * moveSpeed * Time.fixedDeltaTime);


        _rigidbody.AddForce(Vector3.forward * moveSpeed * Time.fixedDeltaTime, ForceMode.Force);

        // Jump
        _rigidbody.AddForce(Physics.gravity * gravityScale * Time.fixedDeltaTime, ForceMode.Acceleration);//zıplamayı gerçek dünyaya yakın yapmaya çalıştım.

    }
    void Update()
    {
        //Movement için input alımı
        xHorizontal = Input.GetAxis("Horizontal");
        zVertical = Input.GetAxis("Vertical");

        
        bool IsOnGround = Physics.Raycast(transform.position, Vector3.down, 1.5f); // Raycast atıp zemin ile mesafe ölçme

        if (Input.GetButtonDown("Jump") && IsOnGround)
        {
            Debug.Log("Calisip calismadigi kontrolu");
            _rigidbody.AddForce(Vector3.up * jumpSpeed * Time.fixedDeltaTime, ForceMode.Impulse);  
            
            nextJumpTime = jumpFall;
        }
    }    
}


//_rigidbody.AddForce(Vector3.up * 1000, ForceMode.Force);
/*
transform.position += Vector3.forward * 10;
transform.position += Vector3.forward * 10 * Time.deltaTime;*/

// zıplama ve eğilme

//nextJumpTime -= Time.deltaTime;  // zamanı güncellemeye çalışıyor bunun ayrıntısı nedir?

// oyun flappy Bird gibi oldu. space ye bastıkça gökyüzüne uçuyor. Bunu nasıl çözeriz ???
// karakterin zemine dokunup öyle zıplaması gerekiyor. Sürekli uçmayacak yani
//zemine deyip değmediği kotrol edilmeli


// Time.fixedDeltaTime kullanmak gerekiyor mu neden? -----> Kullanmana gerek yok force'u impulse olarak verdiğin için, düzenli bir force değil
// Space tuşuna basınca zıplıyor ama zıpladıktan sonra biraz beklemem gerekiyor sebebi nedir? --->fixedupdate içindeyse kaçırıyor ve getButtonDown kullanımı
// Play e bastığımda karakter titreye titreye hatta ışınlnarak hareket ediyor. Sebebi void Update()'de transform.position ataması yapıyoruz ama
//void FixedUpdate() 'te ise fizik motorunu kullanıyor. Böyle olduğu için bizim atama ile fizik motoru çakışıyor gibi bir sonuç buldum.
