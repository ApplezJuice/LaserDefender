using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Projectile")]
public class Projectile : ScriptableObject
{
    [SerializeField] GameObject projPrefab;
    [SerializeField] int projDMG = 1;
    [SerializeField] int projNum = 1;
    [SerializeField] float projSpeed = 20f;
    [SerializeField] float projFiringPeriod = .1f;

    public GameObject GetProjPrefab() { return projPrefab; }
    public int GetProjDMG() { return projDMG; }
    public int GetProjNum() { return projNum; }
    public float GetProjSpeed() { return projSpeed; }
    public float GetProjFiringPeriod() { return projFiringPeriod; }


}
