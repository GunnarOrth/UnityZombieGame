using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Guns : MonoBehaviour
{
    public GameObject bullet;

    public float shootForce, upwardForce;

    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots, ADSspread;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    public int bulletsLeft, bulletsShot;
    private bool shooting, readyToShoot, reloading;

    public Camera fpsCam;
    public Transform attackPoint;

    public bool allowInvoke = true;

    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    //public Rigidbody playerRb;
    public float recoilForce;

    public float recoilAmountY;
    public float recoilAmountX;

    //public float currentrecoilXPos;
    //public float currentrecoilYPos;

    public CamMove mls;

    public Crosshair crosshair;
    public float gunMultiplier = 1;

    public int maxAmmo = 300;
    public TextMeshProUGUI maxAmmoDisplay;

    [SerializeField] private AudioSource shootSoundEffect;
    [SerializeField] private AudioSource reloadSoundEffect;

    public int damage = 50;
    //public float damage = 20;

    private void Awake()
    {
        crosshair.gunMultiplier = gunMultiplier;
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();

        if (ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " ");
        }
        if( maxAmmoDisplay != null)
        {
            maxAmmoDisplay.SetText(maxAmmo/bulletsPerTap + " ");
        }
    }

    private void MyInput()
    {
        if(allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading && (maxAmmo != 0)) Reload();
        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0 && (maxAmmo != 0)) Reload();

        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        
        readyToShoot = false;
        shootSoundEffect.Play();
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit)) targetPoint = hit.point;
        else targetPoint = ray.GetPoint(75);

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x, y;

        if(Input.GetKey(KeyCode.Mouse1))
        {
            x = Random.Range(-ADSspread, ADSspread);
            y = Random.Range(-ADSspread, ADSspread);
        }
        else{
            x = Random.Range(-spread, spread);
            y = Random.Range(-spread, spread);
        }

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x,y, 0);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.GetComponent<CustomBullet>().damage = damage;
        currentBullet.transform.forward = directionWithSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        if(muzzleFlash != null)
        {
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }

        bulletsLeft--;
        bulletsShot++;

        if(allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
            
            //playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
        }

        if(bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

        recoilMath();
    }
    
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;

    }

    private void Reload()
    {
        reloading = true;
        reloadSoundEffect.Play();
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        if(maxAmmo > magazineSize)
        {
            bulletsLeft = magazineSize;
            maxAmmo -= magazineSize;
        }
        else{
            bulletsLeft = maxAmmo;
            maxAmmo = 0;
        }   
        reloading = false;
    }

    public void recoilMath(){
        float currentrecoilXPos = ((Random.value - .5f)/2) * recoilAmountX;
        float currentrecoilYPos = ((Random.value - .5f)/2) * recoilAmountY;
        
        mls.cameraVerticalRotation -= Mathf.Abs(currentrecoilYPos);
        mls.player.Rotate(Vector3.up*currentrecoilXPos);
    }
}
