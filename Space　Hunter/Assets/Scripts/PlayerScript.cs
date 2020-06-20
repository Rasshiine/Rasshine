﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerScript : MonoBehaviour
{
    public bool isPlaying = false;
    public Text countDownText;

    public GameObject straightBullet;
    public GameObject curveBullet;
    public GameObject bomb;
    public GameObject robot1;
    public GameObject robot2;
    public GameObject explosion;

    float straightBulletDamage = 3.0f;
    float curveBulletDamage = 3.0f;
    float tripleBulletDamage = 2.0f;
    float bombDamage = 5.0f;
    float robotDamage = 4.0f;
    float rockDamage = 2.0f;
    float playerDamage = 2.0f;

    public Transform muzzle;
    public Transform bombMuzzle;
    public float speed = 2.0f;
    public float bulletSpeed = 5.0f;
    public float HP = 5;
    public float maxMP = 10;
    float currentMP = 10;
    public Text HPLabel;
    public Text MPLabel;
    public float DurationSeconds;
    public Ease EaseType;
    private int attacksNumber = 5;

    public float shotMP = 2.0f;
    public float curveShotMP = 3.0f;
    public float tripleShotMP = 5.0f;
    public float bombMP = 4.0f;
    public float robotMP = 8.0f;

    float chargeSpeed = 2.0f;
    public float hitTime = 0.5f;
    public float forcePower = 100;
    public int id;

    public int nextAttack;
    public Image nextAttackImage;
    public Image[] imageArray = new Image[5];
    public Sprite[] imageResources = new Sprite[5];
    public int[] keys = new int[3];
    
    public Text winnerLabel;
    public Slider HPSlider;
    public Slider MPSlider;
    Rigidbody playerRb;
    Quaternion HPRotation;
    bool isInShadow = false;

    public AudioClip countDown1;
    public AudioClip countDown2;
    public AudioClip damage;
    public AudioClip bigExplosion;
    //public AudioClip BGM;
    public AudioClip winner;
    public bool explosionPlay = true;
    public bool isGameSet = false;
    public GameObject UFO;
    public GameObject light;
    private AudioSource audioSource;

    //float CountTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        HPSlider.maxValue = HP;
        HPSlider.value = 0.1f;
        
        HPSlider.DOValue(20, 3.0f);
        HP = 20f;
        MPSlider.maxValue = maxMP;
        MPSlider.value = 0f;
        MPSlider.DOValue(10, 3.0f);

        HPRotation = HPSlider.transform.rotation;
        StartCoroutine(CountDown());
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

    IEnumerator CountDown()
    {
        countDownText.text = "3";
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "2";
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "1";
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "GO!";
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "";
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying == false || isGameSet == true)
            return;

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
        if (hitTime >= 0.5f)
        {
            float inputX = (Input.GetAxis("Horizontal" + id));
            transform.Rotate(0, inputX * Time.deltaTime * 100, 0);

            float inputZ = (Input.GetAxis("Vertical" + id));
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
            HP = 0;
            isGameSet = true;
            this.winnerLabel.gameObject.SetActive(true);
            UFO.SetActive(false);
            light.SetActive(false);
            GameObject obj = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
            if(explosionPlay == true)
                {
                audioSource.PlayOneShot(bigExplosion);
                explosionPlay = false;
                }


            if (id == 1)
                winnerLabel.text = "P2win";
            else
                winnerLabel.text = "P1win";

            //this.gameObject.SetActive(false);
            //isPlaying = false;
            //gameObject.SetActive(false);
            //Invoke("Explode", 2.0f);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("StartScene");
            }
        }

        if (isInShadow == true)
        {
            HP -= 0.05f;
            HPSlider.value = HP;
        }
    }

    private void FixedUpdate()
    {
        if (isPlaying == false)
            return;
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
            GameObject obj = Instantiate(bomb, bombMuzzle.position , bombMuzzle.rotation) as GameObject;
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
        switch (other.gameObject.tag)
        {
            case "Straight":
                HP -= straightBulletDamage;
                break;

            case "Curve":
                HP -= curveBulletDamage;
                break;

            case "Triple":
                HP -= tripleBulletDamage;
                break;

            case "Bomb":
                HP -= bombDamage;
                break;

            case "Robot":
                HP -= robotDamage;
                break;

            case "DamageRock":
                HP -= rockDamage;
                break;

            case "Player1":
                HP -= playerDamage;
                break;

            case "Player2":
                HP -= playerDamage;
                break;
        }
        if (other.gameObject.tag != "Untagged")
        {
            Damage();
            playerRb.AddForce((transform.position - other.transform.position).normalized * forcePower);
        }
        //ここもうちょっと上手く書きたい
    }

    void Damage() {
        if (HP > 0)
        {
            hitTime = 0;
            //HP--;
            audioSource.PlayOneShot(damage);
            HPSlider.DOValue(HP, 2.0f);
            HPLabel.gameObject.transform.DOShakePosition(0.3f, 10, 10, 90, false, true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
       
        if (other.gameObject.tag == "Sphere")
            { 
                isInShadow = false;
            }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sphere")
        {
            isInShadow = true;
        }
    }
}