using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampeonAnimator : MonoBehaviour
{
    Animator campeonAM;
    GameObject parentRef;
    CampeonBehaviour campeonBehaviourRef;
    // Start is called before the first frame update
    void Start()
    {
        parentRef =  transform.parent.gameObject;
        campeonBehaviourRef = GetComponent<CampeonBehaviour>();
        campeonAM = parentRef.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!campeonBehaviourRef.estaActivoAtaqueFase1 && !campeonBehaviourRef.estaActivoAtaqueFase2)
        {
            Debug.Log("Nada");
            campeonAM.SetBool("Think", false);
            campeonAM.SetBool("Round", false);
        }
        else if(campeonBehaviourRef.estaActivoAtaqueFase1 && !campeonBehaviourRef.estaActivoAtaqueFase2)
        {
            Debug.Log("Round");
            campeonAM.SetBool("Round", true);
        }
        else if(campeonBehaviourRef.estaActivoAtaqueFase1 && campeonBehaviourRef.estaActivoAtaqueFase2)
        {
            Debug.Log("Think");
            campeonAM.SetBool("Think", true);
            campeonAM.SetBool("Round", false);
        }
        
    }
}
