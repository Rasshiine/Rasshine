using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public GameObject straightBullet;
    public GameObject curveBullet;
    public Transform muzzle;
    public float speed = 1.0f;
    public float bulletSpeed = 2.0f;
    public float HP = 5;
    public float maxMP = 10;
    public float currentMP = 0;
    public float chargeSpeed = 1.0f;
    public int id = 0;
    float CountTime = 0.0f;
    public Slider HPSlider;
    public Slider MPSlider;
    Quaternion HPRotation;
    //public Button shotButton;
    //public Button tripleShotButton;

    // Start is called before the first frame update
    void Start()
    {
        HPSlider.maxValue = HP;
        HPSlider.value = HP;
        MPSlider.maxValue = maxMP;
        MPSlider.value = 1f;
        HPRotation = HPSlider.transform.rotation;
        //shotButton.onClick.AddListener(Shot);
    }

    // Update is called once per frame
    void Update()
    {
        CountTime += Time.deltaTime;
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
        float inputX = (Input.GetAxis("Horizontal"+id));
        transform.Rotate(0, inputX * Time.deltaTime * 100, 0);

        float inputZ = (Input.GetAxis("Vertical"+id));
        transform.position += transform.forward * inputZ * speed * Time.deltaTime;

        if (Input.GetButtonDown("Fire"+id+"a"))
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
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }*/
        if (HP <= 0)
        {
            Debug.Log("HPが0になりました");
            Destroy(this.gameObject);
        }
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
        if (currentMP >= 2.0f)
        {
            Debug.Log(this.transform.position.x);
            GameObject obj = Instantiate(straightBullet, muzzle.position, muzzle.rotation) as GameObject;
            obj.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            
        }
    }

    public void TripleShot()
    {
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



    void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision");
        HP--;
        // Destroy(bullet);
    }
}
