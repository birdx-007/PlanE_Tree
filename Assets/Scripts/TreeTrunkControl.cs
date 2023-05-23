using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTrunkControl : MonoBehaviour
{
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void GrowSeed()
    {
        animator.SetTrigger("growSeed");
        RootTreeControl.seedLeftNumber += 1;
        GameObject parentPlanet = gameObject.transform.parent.gameObject;
        if (parentPlanet != null)
        {
            PlanetControl planetControl = parentPlanet.GetComponent<PlanetControl>();
            if(planetControl != null)
            {
                planetControl.plantedTreeNumber += 1;
            }
        }
        Debug.Log("leftSeed:" + RootTreeControl.seedLeftNumber);
    }
}
