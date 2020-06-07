using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public GameObject straightBullet;
    public GameObject curveBullet;
    public Transform muzzle;
    public float speed = 2.0f;
    public float bulletSpeed = 5.0f;
    public float HP = 5;
    public float maxMP = 10;
    public float currentMP = 0;
    public float chargeSpeed = 1.0f;
    public float hitTime = 0.5f;
    public float forcePower = 100;
    public int id = 0;
    public int key1;
    public int key2;
    public int key3;
    //public int slashKey; 
    //public int underBarKey;
    public int nextAttack;
    public Text winnerLabel;
    public Slider HPSlider;
    public Slider MPSlider;
    Rigidbody playerRb;
    Quaternion HPRotation;
    //float CountTime = 0.0f;
    //public Button shotButton;
    //public Button tripleShotButton;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        HPSlider.maxValue = HP;
        HPSlider.value = HP;
        MPSlider.maxValue = maxMP;
        MPSlider.value = 1f;
        HPRotation = HPSlider.transform.rotation;
        //shotButton.onClick.AddListener(Shot);
        this.winnerLabel.gameObject.SetActive(false);

        key1 = Random.Range(1, 4);
        key2 = Random.Range(1, 4);
        key3 = Random.Range(1, 4);
        //2P
        //dotKey = Random.Range(1, 4);
        //slashKey = Random.Range(1, 4);
        //underBarKey = Random.Range(1, 4);
        nextAttack = Random.Range(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        //CountTime += Time.deltaTime;
        //Debug.Log(currentMP);
        MPSlider.value = currentMP; 
        HPSlider.value = HP;
        HPSlider.transform.rotation = HPRotation;
        if (currentMP >= maxMP)
        {
            currentMP = maxMP;
        }

        /* if (Input.GetKey(KeyCode.W))
         {
             transform.position += transform.forward * 0.05f;
         }
         if (Input.GetKey(KeyCode.S))
         {
             transform.position -= transform.forward * 0.05f;
         }
         if (Input.GetKey(KeyCode.D))
         {
             transform.Rotate(0, 10, 0);
         }
         if (Input.GetKey(KeyCode.A))
         {
             transform.Rotate(0, -10, 0);
         }
         */

         //ノックバックの処理
        if (hitTime >= 0.5f){
            float inputX = (Input.GetAxis("Horizontal" + id));
            transform.Rotate(0, inputX * Time.deltaTime * 100, 0);

            float inputZ = (Input.GetAxis("Vertical" + id));
            //transform.position += transform.forward * inputZ * speed;
            playerRb.velocity = transform.forward * inputZ * speed;
        }
        if (hitTime <= 0.5f)
        {
            hitTime += Time.deltaTime;
        }

        //技入れ替えの処理
        if (Input.GetButtonDown("Fire" + id + "a"))
        {


            switch (key1)
            {
                case 1:
                    Shot();
                    break;
                case 2:
                    CurveShot();
                    break;
                case 3:
                    TripleShot();
                    break;
            }
            key1 = nextAttack;
            nextAttack = Random.Range(1, 4);
        }

        if (Input.GetButtonDown("Fire" + id + "b"))
        {


            switch (key2)
            {
                case 1:
                    Shot();
                    break;
                case 2:
                    CurveShot();
                    break;
                case 3:
                    TripleShot();
                    break;
            }
            key2 = nextAttack;
            nextAttack = Random.Range(1, 4);
        }

        if (Input.GetButtonDown("Fire" + id + "c"))
        {


            switch (key3)
            {
                case 1:
                    Shot();
                    break;
                case 2:
                    CurveShot();
                    break;
                case 3:
                    TripleShot();
                    break;
            }
            key3 = nextAttack;
            nextAttack = Random.Range(1, 4);
        }

        /* if (Input.GetButtonDown("Fire" + id + "a"))
         {
             Shot();
         }

        if (Input.GetButtonDown("Fire" + id + "b"))
        {
            CurveShot();
        }

        if (Input.GetButtonDown("Fire" + id + "c"))
        {
            TripleShot();
        }*/

        if (HP <= 0)
        {
            //SceneManager.LoadScene("EndScene");

            //Invoke("Load", 1.0f);
            Debug.Log("HPが0になりました");
            this.winnerLabel.gameObject.SetActive(true);

            
            if (id == 1)
            {   
                winnerLabel.text = "P2win";
            }
            else 
            {
                winnerLabel.text = "P1win";
            } 

            //gameObject.SetActive(false);
            //Destroy(this.gameObject);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("StartScene");
            }
        }
    }

    public void Load()
    {
        Debug.Log("mittann");
        SceneManager.LoadScene("EndScene");
    }

    private void FixedUpdate()
    {
        currentMP += chargeSpeed * Time.fixedDeltaTime;
    }

    public void Shot()
    {
        if (currentMP >= 2.0f)
        {
            Debug.Log(this.transform.position.x);
            GameObject obj = Instantiate(straightBullet, muzzle.position, muzzle.rotation) as GameObject;
            obj.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            currentMP -= 2.0f;
        }
    }

    public void CurveShot()
    {
        if (currentMP >= 3.0f)
        {
            Debug.Log("CurveShot");
            GameObject obj = Instantiate(curveBullet, muzzle.position, muzzle.rotation) as GameObject;
            obj.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            currentMP -= 3.0f;
        }
    }

    public void NormalShot()
    { 
           // Debug.Log(this.transform.position.x);
            GameObject obj = Instantiate(straightBullet, muzzle.position, muzzle.rotation) as GameObject;
            obj.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }

    public void TripleShot()
    {
        if (currentMP >= 3.0f)
        {
            Debug.Log("TripleShot");
            Invoke("NormalShot", 0.1f);
            Invoke("NormalShot", 0.4f);
            Invoke("NormalShot", 0.7f);

            /* if (currentMP >= 3.0f)
             {
                 Debug.Log(this.transform.position.x);
                 GameObject obj = Instantiate(straightBullet, muzzle.position, muzzle.rotation) as GameObject;
                 for (int i = 0; i < 3; i++)
                 {

                     CountTime += ;
                     if (CountTime >= 0.5f)
                     {
                         Shot();
                         CountTime = 0.0f;
                     }
                 }
             }*/
            // Destroy(bullet, 1);
            currentMP -= 3.0f;
        }
    }

    public void Bomb()
    {
        
    }

    

    void OnCollisionEnter(Collision other)
    {
        
        Debug.Log("collision");
        Damage();
        playerRb.AddForce((transform.position - other.transform.position).normalized * forcePower);

        // Destroy(bullet);
    }

    void Damage() {
        hitTime = 0;
        HP--;
    }
}