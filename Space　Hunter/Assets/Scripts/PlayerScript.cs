using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerScript : MonoBehaviour
{
    public GameObject straightBullet;
    public GameObject curveBullet;
    public GameObject Bomb;
    public GameObject robot1;
    public GameObject robot2;

    public Transform muzzle;
    public Transform bombMuzzle;
    public float speed = 2.0f;
    public float bulletSpeed = 5.0f;
    public float HP = 5;
    public float maxMP = 10;
    public float currentMP = 0;
    public Text HPLabel;
    public Text MPLabel;
    public float DurationSeconds;
    public Ease EaseType;
    public int attacksNumber = 5;

    public float shotMP = 2.0f;
    public float curveShotMP = 3.0f;
    public float tripleShotMP = 4.0f;
    public float bombMP = 4.0f;
    public float robotMP = 8.0f;

    public float chargeSpeed = 3.0f;
    public float hitTime = 0.5f;
    public float forcePower = 100;
    public int id;
    public int nextAttack;
    public Image nextAttackImage;
    public Image[] imageArray = new Image[5];
    public Sprite[] imageResources = new Sprite[5];
    public int[] keys = new int[3];
    //public int slashKey; 
    //public int underBarKey;
    
    public Text winnerLabel;
    public Slider HPSlider;
    public Slider MPSlider;
    Rigidbody playerRb;
    Quaternion HPRotation;
    bool isInshadow = false;
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
        MPSlider.value = 0f;
        HPRotation = HPSlider.transform.rotation;
        //shotButton.onClick.AddListener(Shot);
        this.winnerLabel.gameObject.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            keys[i] = Random.Range(1, attacksNumber + 1);
        }
       
        nextAttack = Random.Range(1, attacksNumber + 1);
        nextAttackImage.sprite = imageResources[nextAttack - 1];
        //画像を持ってくる
        for (int i = 0; i < 3; i++)
        {
            imageArray[i].sprite = imageResources[keys[i] - 1];
        }

    }

    // Update is called once per frame
    void Update()
    {
        HPLabel.text = HP.ToString("f0");
        MPLabel.text = currentMP.ToString("f0");
        //CountTime += Time.deltaTime;
        //Debug.Log(currentMP);
        MPSlider.value = currentMP; 
       // HPSlider.value = HP;
        HPSlider.transform.rotation = HPRotation;
        if (currentMP >= maxMP)
        {
            currentMP = maxMP;
        }

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

        for (int i = 0; i < 3; i++)
        {
            //技入れ替えの処理
            if (Input.GetButtonDown("FireP" + id + "_" + (i + 1)))
            {


                switch (keys[i])
                {
                    case 1:
                        if (currentMP >= shotMP)
                        {
                            Shot();
                            ChangeImage();
                        }
                        break;
                    case 2:
                        if (currentMP >= curveShotMP)
                        {
                            CurveShot();
                            ChangeImage();
                        }
                        break;
                    case 3:
                        if (currentMP >= tripleShotMP)
                        {
                            TripleShot();
                            ChangeImage();
                        }
                        break;
                    case 4:
                        if (currentMP >= bombMP)
                        {
                            BombPut();
                            ChangeImage();
                        }
                        break;
                    case 5:
                        if (currentMP >= robotMP)
                        {
                            RobotPut();
                            ChangeImage();
                        }
                        break;
                }
               
                
            }
            void ChangeImage()
            {
                keys[i] = nextAttack;
                imageArray[i].sprite = imageResources[keys[i] - 1];
                nextAttack = Random.Range(1, attacksNumber + 1);
                nextAttackImage.sprite = imageResources[nextAttack - 1];
                transform.DOScale(1.1f, 0.5f).SetEase(Ease.OutElastic);
            }
        }


        if (HP <= 0)
        {
            //SceneManager.LoadScene("EndScene");

            //Invoke("Load", 1.0f);
            HP = 0;
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
        if (isInshadow == false)
        {
            HP -= 0.05f;
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
        if (currentMP >= shotMP)
        {
            Debug.Log(this.transform.position.x);
            GameObject obj = Instantiate(straightBullet, muzzle.position, muzzle.rotation) as GameObject;
            obj.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            currentMP -= shotMP;
        }
    }

    public void CurveShot()
    {
        if (currentMP >= curveShotMP)
        {
            Debug.Log("CurveShot");
            GameObject obj = Instantiate(curveBullet, muzzle.position, muzzle.rotation) as GameObject;
            obj.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            currentMP -= curveShotMP;
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
        if (currentMP >= tripleShotMP)
        {
            Debug.Log("TripleShot");
            Invoke("NormalShot", 0.1f);
            Invoke("NormalShot", 0.4f);
            Invoke("NormalShot", 0.7f);

            currentMP -= tripleShotMP;
        }
    }

    public void BombPut()
    {
        if (currentMP >= bombMP)
        {
            GameObject obj = Instantiate(Bomb, bombMuzzle.position , bombMuzzle.rotation) as GameObject;
            currentMP -= bombMP;
        }
    }

    public void RobotPut()
    {
        if (currentMP >= robotMP)
        {
            if (id == 1)
            { 
                GameObject obj = Instantiate(robot1, bombMuzzle.position, bombMuzzle.rotation) as GameObject;
            }
            else
            {
                GameObject obj = Instantiate(robot2, bombMuzzle.position, bombMuzzle.rotation) as GameObject;
            }
            currentMP -= robotMP;
        }
    }




    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Damage")
        {
            Debug.Log("collision");
            Damage();
            playerRb.AddForce((transform.position - other.transform.position).normalized * forcePower);
        }
        // Destroy(bullet);
    }

    void Damage() {
        hitTime = 0;
        HP--;
        HPSlider.DOValue(HP, 2.0f);
        HPLabel.DOFade(0.01f, this.DurationSeconds).SetEase(this.EaseType).SetLoops(5, LoopType.Restart);
    }

    private void OnTriggerStay(Collider other)
    {
       
        if (other.gameObject.tag == "Sphere")
            { 
                isInshadow = true;
            }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sphere")
        {
            isInshadow = false;
        }
    }
}