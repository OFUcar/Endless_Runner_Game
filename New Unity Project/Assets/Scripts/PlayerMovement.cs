using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    //public float moveSpeed = 0.01f;
    public bool isGameRunning = false;

    public int minLane = 0;
    public int maxLane = 4;
    public int startLane = 2;
    public float distanceLane =2f;
    public float moveSpeed = 3f;

    public Transform startMarker;
    public Transform endMarker;
    public float lineSwitchSpeed = 1.0f;
    public float startTime;
    public float journeyLength;
    public Vector3 targetPosition;

    void Start()
    {
        //Hareketin başlangıç zamanı ve yolculuk uzunluk hesabı //Lerp
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        //Başlangıç pozisyonu
        targetPosition = transform.position;
    }

    void FixedUpdate()
    {

    }
    void Update()
    {
        //Sağ klik ile başlangıç
        if (!isGameRunning && Input.GetMouseButtonDown(0))
        {
            isGameRunning = true;
        }
        if (isGameRunning) 
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;

            //Sağ ve sol şeride gitme --->
            if (Input.GetKeyDown(KeyCode.A) && startLane > minLane)
            {
                startLane--;
            }
            if (Input.GetKeyDown(KeyCode.D) && startLane < maxLane)
            {
                startLane++;
            }
            targetPosition = new Vector3(startLane * distanceLane - (distanceLane * 2), transform.position.y, transform.position.z);            
        }






        // Şerit değiştirme konumu hedef ---> Lerp yerine anında değişim

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, lineSwitchSpeed * Time.deltaTime);


    }    
}
/*yatayda hareket(line switch)
random object spawn (sadece spawn olsunlar random şekilde) :
objelerin aralıklarını yatayda ve dikeyde biz kontrol edebilelim.
random tipte objeler spawn olsun.
çarptığında fail edecek objeler (başlangıç noktasına dönülecek.)
çarptığında toplanabilir olacak ve bunların count'u tutulacak.*/


/* sağ-sol yapmanın konuma etkisi
Vector3 newPosition = transform.position;
newPosition.x = startLane * distanceLane - (distanceLane * 2);
transform.position = newPosition;*/



/*
        // x = v * t
        float distanceMoved = (Time.time - startTime) * lineSwitchSpeed;
        // tammalanan yolculuk kesiri = mevcut/ toplam
        float fractionOfJourney = distanceMoved / journeyLength;
        //Lerp ---> pozisyonu ona göre ayarla
        transform.position = Vector3.Lerp(startMarker.position , endMarker .position, fractionOfJourney);
        */

//public float xHorizontal;
//public float zVertical;
//private Rigidbody _rigidbody;
//public float jumpSpeed = 5f;
//public float jumpFall;
//public float nextJumpTime;
//public float gravityScale = 3f;

/*
    
        //_rigidbody.AddForce(Vector3.forward * moveSpeed * Time.fixedDeltaTime, ForceMode.Force);

        // Jump
        //_rigidbody.AddForce(Physics.gravity * gravityScale * Time.fixedDeltaTime, ForceMode.Acceleration);//zıplamayı gerçek dünyaya yakın yapmaya çalıştım.

 */
/*
 
        //Movement için input alımı
        xHorizontal = Input.GetAxis("Horizontal");
        zVertical = Input.GetAxis("Vertical");

        //Movement
        Vector3 moveSystem = new Vector3(xHorizontal, 0, zVertical);
        _rigidbody.MovePosition(transform.position + moveSystem * moveSpeed * Time.deltaTime);


        bool IsOnGround = Physics.Raycast(transform.position, Vector3.down, 1.5f); // Raycast atıp zemin ile mesafe ölçme

        if (Input.GetButtonDown("Jump") && IsOnGround)
        {
            Debug.Log("Calisip calismadigi kontrolu");
            _rigidbody.AddForce(Vector3.up * jumpSpeed * Time.fixedDeltaTime, ForceMode.Impulse);  
            
            nextJumpTime = jumpFall;
        }
*/

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
