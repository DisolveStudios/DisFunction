using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunMechanicsTwo : MonoBehaviour
{
    public Image crosshair;
    public Vector3 targetRotation;
    public Vector3 currentRotation;
    
    public Vector3 initialPosition;
    public Vector3 targetMovement;
    public float recoilX;
    public float recoilY;
    public float recoilZ;

    public float returnspeed;
    private float initialReturnspeed;
    public float snapiness;

    public float fireRate;

    public float kickBackPower;

    public Vector3 camChange;

    public bool cooldown;

    float yRot;

    public float sensitivity = 1;

    public int bulletsInMag = 40;
    private int initialBulletsInMag;

    float mouseY;

    Vector3 camRot;
    Vector3 camCurr;

    public Vector3 camSen;
    public Vector3 camRecoil;

    Vector3 prevPos;

    public GameObject gunMouth;

    public GameObject debugBall;

    public bool isSingleClick;

    public Vector3 defaultAimPosition;

    public Vector3 aimPosition;

    public Vector3 currentTargetAim;

    public bool isAiming;

    public bool canShoot;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        initialPosition = transform.localPosition;
        targetMovement = initialPosition;
        cooldown = false;
        defaultAimPosition = transform.localPosition;
        currentTargetAim = defaultAimPosition;

        aimPosition = new Vector3(-0.167f,-0.105f, 0.100f);

        initialBulletsInMag = bulletsInMag;
        initialCoolDownTime = coolDownTime;
        initialReturnspeed = returnspeed;
    }

    public bool isLock = false;

    public float coolDownTime = 0.1f;
    private float initialCoolDownTime;
    private float latestTime;

    // Update is called once per frame
    void Update()
    {
        DetermineAim();
        controlGunMechanicsByFrameRate();
        getFramRatePerSecond();
        mouseY = Input.GetAxis("Mouse Y");
        if(Input.GetMouseButton(0) && canShoot && bulletsInMag > 0) {
           if(Time.time - latestTime > coolDownTime) {
             latestTime = Time.time;
             canShoot = false;
             Shoot();
           }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            bulletsInMag = initialBulletsInMag;
        }

        // just for fixing crosshair
        if(Input.GetKeyDown(KeyCode.F)) {
            if (isLock) {
                isLock = false;
            }
            else {
                isLock = true;
            }
        }

        if (isLock) {
            sensitivity = 0;
            PlayerRotation.mouseSensitivity = 0;
        }else{
            sensitivity = 1;
             PlayerRotation.mouseSensitivity = 1;
        }

        targetRotation = Vector3.Lerp(targetRotation, new Vector3(0,0,0), Time.fixedDeltaTime * returnspeed);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snapiness * Time.fixedDeltaTime);

        transform.localRotation = Quaternion.Euler(targetRotation);
        
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
        // camRecoil += new Vector3(recoilX, yRecoil, zRecoil);

        Vector3 kickBack = new Vector3(0f, 0, -0.2f);
        targetMovement += kickBack  * Time.fixedDeltaTime * 50.0f;
        targetMovement.z = Mathf.Clamp(targetMovement.z,kickBackPower , 0.7f);
        bulletsInMag--;

        // yield return new WaitForSeconds(fireRate * Time.fixedDeltaTime);
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
                isAiming = false;
                // crosshair.GetComponent<RectTransform>().anchoredPosition = new Vector2(-33.1f, 11.3f);
            }
            else {
                initialPosition = aimPosition;
                isAiming = true;
                // crosshair.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2.5752f, -8.1848f);
            }
        }
    }

    void HoldToAim() {
         initialPosition = defaultAimPosition;
         if(Input.GetMouseButton(1)) {
            initialPosition = aimPosition;
            // crosshair.GetComponent<RectTransform>().anchoredPosition = new Vector2(-33.1f, 11.3f);
        } else {
            // crosshair.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2.5752f, -8.1848f);
        }
    }

    private float currentFPSOffset;
    private float baseFPS = 30;

    private float pollingTime = 0.2f;
    private float time;
    private int frameCount;

    private int frameRate;

    private void controlGunMechanicsByFrameRate() {
       
        currentFPSOffset = frameRate - baseFPS;
        currentFPSOffset = Mathf.Clamp(currentFPSOffset, 0 , Int32.MaxValue);
        Debug.Log("frameRate: " + frameRate);
        // Debug.Log(currentFPSOffset);
        float requiredFireRate = currentFPSOffset * 0.000070f;
        float requiredReturnspeed = currentFPSOffset * 0.1f;

        coolDownTime = initialCoolDownTime + requiredFireRate;
        returnspeed = initialReturnspeed - requiredReturnspeed;
        returnspeed = Mathf.Clamp(returnspeed, 2.0f , Int32.MaxValue);
    }

    private void getFramRatePerSecond() {
        time += Time.deltaTime;
        frameCount++;
        // Debug.Log("Yes");
        if(time >= pollingTime) {
            // Debug.Log("And Yes");
            frameRate = Mathf.RoundToInt(frameCount / time);
            time -= pollingTime;
            frameCount = 0;
        }
     }
}
