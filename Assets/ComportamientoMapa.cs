using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoMapa : MonoBehaviour
{
    public static int mapasGanados;
    public static int cinematica;
    GameObject Nubeslvl2;
    public GameObject OraculoRef;
    bool nubeAfuera;
    void Start()
    {
        if(cinematica == 1)
        {
            OraculoRef.SetActive(false);
        }
        mapasGanados = 1;
        nubeAfuera = false;
        Nubeslvl2 = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(!OraculoRef.activeSelf && !nubeAfuera)
        {
            StartCoroutine(pararNube());
        }
    }

    IEnumerator pararNube()
    {
        Nubeslvl2.transform.position += new Vector3(3,0,0) * Time.deltaTime;
        yield return new WaitForSeconds(5.5f);
        nubeAfuera = true;
    }

    public void SalirDelJuego()
    {
        mapasGanados = 0;
    }
}
