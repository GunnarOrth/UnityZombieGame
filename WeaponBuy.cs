using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuy : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public int cost = 500;
    public string InteractionPrompt => _prompt;
    public int getCost => cost;

    public GameObject gun;
    public GameObject parent;
    public Transform gunPosition;

    public bool bought = false;

    public Guns gunScript;

    public GameObject bullet;
    public float shootForce, upwardForce;
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots, ADSspread;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    //private int bulletsLeft, bulletsShot;
    //private bool shooting, readyToShoot, reloading;
    public bool allowInvoke = true;
    public float recoilForce;
    public float recoilAmountY;
    public float recoilAmountX;
    public float gunMultiplier = 1;
    public int maxAmmo = 300;
    public int damage = 30;

    //public MeshFilter modelYouWanttoChange;
    //public Mesh modelYouWantToUse;

    public bool Interact(Interactor interactor)
    {
        if(bought)
        {
            gunScript.maxAmmo = maxAmmo;
        }
        else{
            bought = true;
            //gunScript.bullet = bullet;
            gunScript.shootForce = shootForce;
            gunScript.upwardForce = upwardForce;
            gunScript.timeBetweenShooting = timeBetweenShooting;
            gunScript.spread = spread;
            gunScript.reloadTime = reloadTime;
            gunScript.timeBetweenShots = timeBetweenShots;
            gunScript.ADSspread = ADSspread;
            gunScript.magazineSize = magazineSize;
            gunScript.bulletsLeft = magazineSize;
            gunScript.bulletsPerTap = bulletsPerTap;
            gunScript.allowButtonHold = allowButtonHold;
            gunScript.allowInvoke = allowInvoke;
            gunScript.recoilForce = recoilForce;
            gunScript.recoilAmountY = recoilAmountY;
            gunScript.recoilAmountX = recoilAmountX;
            gunScript.gunMultiplier = gunMultiplier;
            gunScript.maxAmmo = maxAmmo;
            gunScript.damage = damage;
            cost = cost/2;
            //modelYouWanttoChange.mesh = modelYouWantToUse;
            GameObject gun2 = Instantiate(gun, gunPosition.position, Quaternion.identity);
            Object.Destroy(parent.transform.GetChild(2).gameObject);
            gun2.transform.parent = parent.transform;
            gun2.transform.localScale = gunPosition.localScale;
            gun2.transform.rotation = gunPosition.rotation;
        }
        Debug.Log(message:"bought gun");
        return true;
    }
}