using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask destroyableLayer;

    [SerializeField] float shootRange = 10f;

    [SerializeField] Enemy enemy;

    [SerializeField] UIController uiController;

    private void Awake() 
    {
        
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    void Update()
    {
        Shoot();   
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            uiController.FlashRedCrosshair();

            if (Physics.Raycast(ray, out RaycastHit hit, shootRange, destroyableLayer))
            {
                Enemy hitEnemy = hit.collider.GetComponentInParent<Enemy>();

                if (hitEnemy != null)
                {
                    hitEnemy.TakeDamage(1);
                }
            }
        }
    }


}
