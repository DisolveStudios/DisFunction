using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
    public float aimPositionSpeed;
    public float snapiness;
    public float kickBackPower;
    public float fireRate = 0.1f;
    public float sensitivity = 1;
    public float gunHorizontalSway = 1.0f;
    public float gunVerticalSway = 1.0f;

    public int bulletsInMag = 40;

    [Header("Gun Damage and Impact Variables")]
    public float damage = 10.0f;
    public Distance closeRange = Distance.CLOSE;
    public Distance midRange = Distance.NEUTRAL;
    public float ImpactWithinCloseRange;
    public float ImpactWithinMidRange;
    public float ImpactWithinFarRange;

    [Header("Fps Camera Attributes")]
    public Quaternion peekDegree;
    public float camShake;

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

    [Header("Damage Statistics")]
    public DummySpawnSystem spawnSystem;

    private Vector3 camRot;
    private Vector3 camCurr;
    private Vector3 prevPos;
    private Vector3 prevShootPosition;

    private float initialCoolDownTime;
    private float initialReturnspeed;
    private float latestTime;
    private float mouseY;
    private float mouseX;
    private float currentFPSOffset;
    private float baseFPS = 30;
    private float initialKickBack;
    private float mouseYMax;

    private int initialBulletsInMag;
    private int frameCount;
    private int frameRate;

    private GunViewAnimationTrigger gunViewAnimation;
    private Unwrap distanceUnwrap = new Unwrap(1.0f, 4.2f);

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        initialPosition = transform.localPosition;
        initialKickBack = kickBackPower;
        initialReturnspeed = returnspeed;
        targetMovement = initialPosition;
        defaultAimPosition = transform.localPosition;
        currentTargetAim = defaultAimPosition;

        initialBulletsInMag = bulletsInMag;
        initialCoolDownTime = fireRate;

        gunViewAnimation = gunModelPrefab.GetComponent<GunViewAnimationTrigger>();
    }

    private void Update()
    {

        //Debug.Log(distanceUnwrap.distance(this.closeRange));
        //Debug.Log(distanceUnwrap.distance(this.midRange));

        DetermineAim();
        if (Input.GetKeyDown(KeyCode.R))
        {
            bulletsInMag = initialBulletsInMag;
        }

        if (Input.GetMouseButton(0) && canShoot && bulletsInMag > 0)
        {
            initialReturnspeed = returnspeed;
        }
        else
        {
            initialReturnspeed = 9.0f;
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
        mouseX = Input.GetAxis("Mouse X");

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

        if (Input.GetMouseButton(0) && canShoot && bulletsInMag > 0)
        {
            if (Time.time - latestTime > fireRate)
            {
                if (gunViewAnimation)
                {
                    gunViewAnimation.disableViewAnimation();
                }
                latestTime = Time.time;
                canShoot = false;
                Shoot();
            }
        }

        targetRotation = Vector3.Lerp(targetRotation, new Vector3(0, 0, 0), Time.fixedDeltaTime * initialReturnspeed);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snapiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
        
        targetMovement = Vector3.Lerp(targetMovement, initialPosition, Time.deltaTime * aimPositionSpeed);
        transform.localPosition = targetMovement;

        camSen += new Vector3(-mouseY * sensitivity, 0, 0);
        camRecoil = camSen + (targetRotation * camShake) + peekDegree.eulerAngles;

        transform.parent.localRotation = Quaternion.Euler(camRecoil);
    }

    void Shoot() {
       RaycastHit hit;
       if(Physics.Raycast(gunMouth.transform.position, gunMouth.transform.TransformDirection(Vector3.forward) , out hit, Mathf.Infinity)) {
            Debug.DrawRay(gunMouth.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            GameObject obj = Instantiate(debugBall, hit.point, Quaternion.identity);
            obj.transform.SetParent(hit.transform, true);
            ImpactBearer impactBearer = hit.transform.GetComponent<ImpactBearer>();

            if (impactBearer != null)
            {
                float distanceBetweenGunAndObject = Geometry.GetDistance(gunMouth.transform.position, hit.point);
                Dummy parent = impactBearer.parent;

                if(parent != null)
                {
                    parent.damage(getDamageByDistance(damage, distanceBetweenGunAndObject), impactBearer.impact);
                }

                // To Calculate damage stats, can be erased again.
                switch (impactBearer.impact)
                {
                    case Impact.Head:
                        spawnSystem.damageStatistics.isHeadShot();
                        break;

                    case Impact.Body:
                        spawnSystem.damageStatistics.isBodyShot();
                        break;

                    case Impact.Foot:
                        spawnSystem.damageStatistics.isFootShot();
                        break;
                    case Impact.InAccuracy:
                        spawnSystem.damageStatistics.hitNone();
                        break;
                }
            }
        }

        float yRecoil = UnityEngine.Random.Range(-recoilY, recoilY) ;
        float zRecoil = UnityEngine.Random.Range(-recoilZ, recoilZ) ;
        targetRotation += new Vector3(recoilX , yRecoil, zRecoil) * Time.fixedDeltaTime;

        Vector3 kickBack = new Vector3(0f, 0, -kickBackPower);
        targetMovement += kickBack  * Time.fixedDeltaTime;
         //targetMovement.z = Mathf.Clamp(targetMovement.z,kickBackPower , 0.7f);
        bulletsInMag--;
        canShoot = true;
    }

    private float getDamageByDistance(float damage, float distance)
    {
        if (distance < distanceUnwrap.distance(this.closeRange))
        {
            return damage - ImpactWithinCloseRange;

        }
        else if (distance >= distanceUnwrap.distance(this.closeRange) && distance < distanceUnwrap.distance(this.midRange)) {
            return damage - ImpactWithinMidRange;

        } else {
            return damage - ImpactWithinFarRange;
        }
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

    //private void controlGunMechanicsByFrameRate() {        
    //    currentFPSOffset = frameRate - baseFPS;
    //    currentFPSOffset = Mathf.Clamp(currentFPSOffset, 0 , Int32.MaxValue);
    //    Debug.Log("frameRate: " + frameRate);
    //    float requiredFireRate = currentFPSOffset * 0.000100f;
    //    //float requiredReturnspeed = currentFPSOffset * 0.150f;

    //    fireRate = initialCoolDownTime + requiredFireRate;
    //    //returnspeed = initialReturnspeed - requiredReturnspeed;
    //    //returnspeed = Mathf.Clamp(returnspeed, 2.0f , Int32.MaxValue);
    //}
    //
    //private void getFrameRates() {
    //    frameRate = ApplicationFrameRate.GetCurrentFrameRate(0.1f);
    //}
    //
    //private void controlRecoilByFrameRateDepndent()
    //{
    //    if(frameRate >= 30 && frameRate < 60) 9 || 37 - 50 = 6.5 || 53 - 65 = 5.2 || 70 - 83 = 4.2 || 81 = 3.8{
    //        returnspeed = 9.0f;
    //    }
    //    else if(frameRate >= 60 && frameRate < 90) = 5.0f
    //}
}
