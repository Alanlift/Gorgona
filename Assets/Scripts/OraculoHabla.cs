using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OraculoHabla : MonoBehaviour
{
    string description;
    public Text textoOraculo;
    public GameObject OraculoAdios;
    public bool tieneArma;
    bool ahoraAtaca;
    // Start is called before the first frame update
    void Start()
    {
        tieneArma = false;
        StartCoroutine(cambiarTexto());
    }

    // Update is called once per frame
    void Update()
    {
        if(tieneArma)
        {
            textoOraculo.text = "Prueba atacar a estos enemigos";
            tieneArma = false;
            ahoraAtaca = true;
            StartCoroutine(cambiarTexto2());
        }
    }

    IEnumerator cambiarTexto()
    {
        yield return new WaitForSeconds(3f);
        if(!tieneArma && !ahoraAtaca)
        {
            textoOraculo.text = "Bueno, no tan vida aún; este es un sueño lúcido";
            yield return new WaitForSeconds(4f);
            if(!tieneArma && !ahoraAtaca)
            {
            textoOraculo.text = "Sé que aún no me conoces, pero ahora mismo soy tu mejor opción";
            yield return new WaitForSeconds(5f);
            }
            if(!tieneArma && !ahoraAtaca)
            {
            textoOraculo.text = "Recuerdas como moverte, verdad? (Utiliza W,A,S,D)";
            yield return new WaitForSeconds(6f);
            }
            if(!tieneArma && !ahoraAtaca)
            {
            textoOraculo.text = "Muy bien, ahora, ¿Recuerdas tu arco?";
            yield return new WaitForSeconds(4f);
            }
            if(!tieneArma && !ahoraAtaca)
        {
            textoOraculo.text = "Ya no lo necesitarás, Los rayos que te ha otorgado Zeus lo reemplazarán (Recoge el item)";
        }
        }
        //changeTarget(player);   
            //Debug.Log(estaActivoAtaque); 
        //isWaveActive = true; // set waveStarted to true to prevent the coroutine from starting multiple times
        //Wave para "waveInterval" segundos antes de que inicie la siguiente wave     
        
    }

    IEnumerator cambiarTexto2()
    {
        yield return new WaitForSeconds(3f);
        if(ahoraAtaca && !tieneArma)
        {
            textoOraculo.text = "Algunas veces, tus viejos poderes de Medusa vuelven a ti; cuando derrotes rivales, dejarán piedras.";
            yield return new WaitForSeconds(6f);
            textoOraculo.text = "Cuando tengas suficiente poder, podrás mejorarte de alguna manera";
            //OraculoAdios.SetActive(false);
            //TutorialManager.instance.StartLevelUp();
            yield return new WaitForSeconds(5f);
            textoOraculo.text = "Creo que eso es todo lo que necesitas por ahora.";
            yield return new WaitForSeconds(3.5f);
            textoOraculo.text = "Ya nos encontraremos, recuerda que eres parte de algo mucho más grande";
            yield return new WaitForSeconds(5f);
            textoOraculo.text = "Una cosa más, si en algún momento necesitas descansar, llamame (Aprieta el Espacio/Barra Espaciadora)";
            yield return new WaitForSeconds(6f);
            textoOraculo.text = "Ahora... ¡Despierta Gorgona!";
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Partida");
        }
    }
}
