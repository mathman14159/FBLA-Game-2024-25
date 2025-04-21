using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public GameObject purifyBullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    
    public int value;
    // Start is called before the first frame update
    void Start()
    {
        
        
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

       Vector3 rotation = mousePos - transform.position;

       float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if(!canFire)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if(Input.GetMouseButton(0) && canFire && ArrowKepper.instance.currentArrows > 0)
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            
            ArrowKepper.instance.DecreaseArrows(1);
        }

        if (Input.GetMouseButton(1) && canFire && ArrowKepper.instance.currentArrows >= 5)
        {
            canFire = false;
            Instantiate(purifyBullet, bulletTransform.position, Quaternion.identity);

            ArrowKepper.instance.DecreaseArrows(5);
        }

    }
}
