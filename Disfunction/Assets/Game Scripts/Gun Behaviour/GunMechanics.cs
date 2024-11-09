using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunMechanics : MonoBehaviour
{
     [Header("Gun Vector Position")]
    public Vector3 targetRotation;
    public Vector3 currentRotation;
    public Vector3 initialPosition;
    public Vector3 targetMovement;
    public Vector3 camChange;
    public Vector3 defaultAimPosition;
    public Vector3 aimPosition;
    public Vector3 currentTargetAim;
    public Vector3 camSen;
    public Vector3 camRecoil;

    [Header("Prefabs")]
    public GameObject gunModelPrefab;

    [Header("Gun Attributes")]
    public float recoilX;
    public float recoilY;
    public float recoilZ;
    public float returnspeed;
    private float initialReturnspeed;
    public float snapiness;
    public float kickBackPower;
    public float fireRate = 0.1f;
    public float sensitivity = 1;

    public int bulletsInMag = 40;

    [Header("Gun Conditions")]
    public bool isSingleClick;
    public bool isAiming;
    public bool canShoot;
    public bool isLock = false;
    public bool minimizeFPSError;
    public bool controlRecoilWhileAiming;

    [Header("Gun Objects")]
    public GameObject gunMouth;
    public GameObject debugBall;

    private Vector3 camRot;
    private Vector3 camCurr;
    private Vector3 prevPos;

    private float initialCoolDownTime;
    private float latestTime;
    private float mouseY;
    private float currentFPSOffset;
    private float baseFPS = 30;
    private float initialKickBack;

    private int initialBulletsInMag;
    private int frameCount;
    private int frameRate;

    private GunViewAnimationTrigger gunViewAnimation;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        initialPosition = transform.localPosition;
        initialKickBack = kickBackPower;
        targetMovement = initialPosition;
        defaultAimPosition = transform.localPosition;
        currentTargetAim = defaultAimPosition;

        initialBulletsInMag = bulletsInMag;
        initialCoolDownTime = fireRate;
        initialReturnspeed = returnspeed;

        gunViewAnimation = gunModelPrefab.GetComponent<GunViewAnimationTrigger>();
    }

    private void Update()
    {
        DetermineAim();
        if (Input.GetKeyDown(KeyCode.R))
        {
            bulletsInMag = initialBulletsInMag;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isLock)
            {
                sensitivity = 0;
                PlayerRotation.mouseSensitivity = 0;
                isLock = true;
            }
            else
            {
                sensitivity = 1;
                PlayerRotation.mouseSensitivity = 1;
                isLock = false;
            }
        }
    }

    void FixedUpdate()
    {
        mouseY = Input.GetAxis("Mouse Y");

        if(minimizeFPSError) {
            getFrameRates();
            controlGunMechanicsByFrameRate();
        }

        if(Input.GetMouseButton(0) && canShoot && bulletsInMag > 0) {
           if(Time.time - latestTime > fireRate) {
             if(gunViewAnimation) {
                gunViewAnimation.disableViewAnimation();
             } 
             latestTime = Time.time;
             canShoot = false;
             Shoot();
           }
        } 

        targetRotation = Vector3.Lerp(targetRotation, new Vector3(0,0,0), Time.fixedDeltaTime * returnspeed);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snapiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
        
        targetMovement = Vector3.Lerp(targetMovement, initialPosition, Time.deltaTime * 7.6f);
        transform.localPosition = targetMovement;

        camSen += new Vector3(-mouseY * sensitivity, 0, 0);
        camRecoil = camSen + targetRotation;

        transform.parent.localRotation = Quaternion.Euler(camRecoil);
    }

    void Shoot() {
       RaycastHit hit;
       if(Physics.Raycast(gunMouth.transform.position, gunMouth.transform.TransformDirection(Vector3.forward) , out hit, Mathf.Infinity)) {
            Debug.DrawRay(gunMouth.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            GameObject obj = Instantiate(debugBall, hit.point, Quaternion.identity);
            obj.transform.SetParent(hit.transform, true);
       }

        float yRecoil = UnityEngine.Random.Range(-recoilY, recoilY) ;
        float zRecoil = UnityEngine.Random.Range(-recoilZ, recoilZ) ;
        targetRotation += new Vector3(recoilX , yRecoil, zRecoil) * Time.fixedDeltaTime;

        Vector3 kickBack = new Vector3(0f, 0, -kickBackPower);
        targetMovement += kickBack  * Time.fixedDeltaTime;
        // targetMovement.z = Mathf.Clamp(targetMovement.z,kickBackPower , 0.7f);
        bulletsInMag--;
        canShoot = true;
    }

    void DetermineAim() {
         if(isSingleClick) {
            SingleClickAim();
        }
        else {
            HoldToAim();
        }
        //  Vector3 currentPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * 4);
        //  transform.localPosition = currentPosition;
    }

     void SingleClickAim() {
        if(Input.GetMouseButtonDown(1)) {
            if (isAiming) {
                initialPosition = defaultAimPosition;
                if (gunViewAnimation)
                {
                    gunViewAnimation.lockAnimationTrigger(false);
                }
                isAiming = false;
                kickBackPower = initialKickBack;
                // crosshair.GetComponent<RectTransform>().anchoredPosition = new Vector2(-33.1f, 11.3f);
            }
            else {
                initialPosition = aimPosition;
                isAiming = true;
                if (controlRecoilWhileAiming)
                {
                    kickBackPower /= 2;
                }
                if (gunViewAnimation)
                {
                    gunViewAnimation.lockAnimationTrigger(true);
                }
                    // crosshair.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2.5752f, -8.1848f);
            }
        }
    }

    void HoldToAim() {
         initialPosition = defaultAimPosition;
         if(Input.GetMouseButton(1)) {
            initialPosition = aimPosition;
            if (gunViewAnimation)
            {
                gunViewAnimation.lockAnimationTrigger(true);
            }
            // crosshair.GetComponent<RectTransform>().anchoredPosition = new Vector2(-33.1f, 11.3f);
        } else {
            kickBackPower = initialKickBack;
            // crosshair.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2.5752f, -8.1848f);
        }
        if (gunViewAnimation)
        {
            gunViewAnimation.lockAnimationTrigger(false);
        }
    }

    private void controlGunMechanicsByFrameRate() {        
        currentFPSOffset = frameRate - baseFPS;
        currentFPSOffset = Mathf.Clamp(currentFPSOffset, 0 , Int32.MaxValue);
        Debug.Log("frameRate: " + frameRate);
        float requiredFireRate = currentFPSOffset * 0.000100f;
        //float requiredReturnspeed = currentFPSOffset * 0.150f;

        fireRate = initialCoolDownTime + requiredFireRate;
        //returnspeed = initialReturnspeed - requiredReturnspeed;
        //returnspeed = Mathf.Clamp(returnspeed, 2.0f , Int32.MaxValue);
    }

    private void getFrameRates() {
        frameRate = ApplicationFrameRate.GetCurrentFrameRate(0.1f);
    }

    //private void controlRecoilByFrameRateDepndent()
    //{
    //    if(frameRate >= 30 && frameRate < 60) 9 || 37 - 50 = 6.5 || 53 - 65 = 5.2 || 70 - 83 = 4.2 || 81 = 3.8{
    //        returnspeed = 9.0f;
    //    }
    //    else if(frameRate >= 60 && frameRate < 90) = 5.0f
    //}
}
