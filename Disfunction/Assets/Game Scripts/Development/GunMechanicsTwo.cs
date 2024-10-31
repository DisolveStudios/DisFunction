using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMechanicsTwo : MonoBehaviour
{
    [Header("Gun Factors")]
    public float fireRate = 0.1f;

    public float reloadTime = 1.0f;
    public int ammoLimit = 40;
    public int clipSize = 20;


    [Header("Current Variables")]
    public bool canShoot;
    public int ammoInClip;
    public int ammoLeft;

    [Header("Aiming factors")]
    public Vector3 defaultAimPosition;
    public Vector3 aimPosition;
    public Vector3 currentTargetAim;
    public float aimDelay = 15f;
    public bool isAiming = false;
    public bool isSingleClick = false;

    [Header("Mouse Rotation Factors")]
    public Vector2 currentMouseRotation;
    public float mouseSensitivity = 500f;

    [Header("Gun Recoil")]
    public Vector2[] recoilPattern;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canShoot = true;
        ammoInClip = clipSize;
        ammoLeft = ammoLimit;
        defaultAimPosition = new Vector3(0.24f, -0.24f, 0.6009998f);
        currentTargetAim = defaultAimPosition;
        aimPosition = new Vector3(0.03f, -0.2f, 0.6009998f);

        // Recoil Pattern
        recoilPattern = new Vector2[3];
        recoilPattern[0] = new Vector3(-0.25f, 0.25f);
        recoilPattern[1] = new Vector2(0.35f, 0.35f);
        recoilPattern[2] = new Vector2(0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        DetermineAim();
        MouseRotation();
        if(Input.GetMouseButton(0) && canShoot && ammoInClip > 0) {
            DetrermineRecoil();
            canShoot = false;
            ammoInClip--;
            StartCoroutine(ShootGun());
        }
        else if (Input.GetKeyDown(KeyCode.R) && (ammoInClip < clipSize) && (ammoLeft > 0 ) ) {
            canShoot = false;
            int requiredAmmo = clipSize - ammoInClip;
            if(ammoLeft > requiredAmmo) {
                ammoLeft -= requiredAmmo;
                ammoInClip += requiredAmmo;
            }
            else {
                ammoInClip += ammoLeft;
                ammoLeft = 0;
            }
            canShoot = true;
        }
    }

    void DetrermineRecoil() {
        transform.localPosition -= Vector3.forward * 0.1f;

        int currStep =  clipSize + 1 - ammoInClip;
        currStep = Mathf.Clamp(currStep ,0, recoilPattern.Length - 1);

        currentMouseRotation += recoilPattern[currStep];
    }
    void DetermineAim() {
        if(isSingleClick) {
            SingleClickAim();
        }
        else {
            HoldToAim();
        }
         Vector3 currentPosition = Vector3.Lerp(transform.localPosition, currentTargetAim, Time.deltaTime * aimDelay);
         transform.localPosition = currentPosition;
    }

    void SingleClickAim() {
        if(Input.GetMouseButtonDown(1)) {
            if (isAiming) {
                currentTargetAim = defaultAimPosition;
                isAiming = false;
            }
            else {
                currentTargetAim = aimPosition;
                isAiming = true;
            }
        }
    }

    void HoldToAim() {
         currentTargetAim = defaultAimPosition;
         if(Input.GetMouseButton(1)) {
            currentTargetAim = aimPosition;
        }
    }

    void MouseRotation() {
        float MouseY = Input.GetAxis("Mouse Y");
        // Vector2 rotation = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        // rotation *=  Time.deltaTime * mouseSensitivity;
        // currentMouseRotation += rotation;
        // currentMouseRotation.y = Mathf.Clamp(currentMouseRotation.y, -90, 90);
        // currentMouseRotation += new Vector2(-MouseY,0,0)

        transform.parent.transform.Rotate(-currentMouseRotation.y, 0 ,0);
    }

    IEnumerator ShootGun() {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
