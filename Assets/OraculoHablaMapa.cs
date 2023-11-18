using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OraculoHablaMapa : MonoBehaviour
{
    string description;
    public Text textoOraculo;
    bool ahoraAtaca;
    GameObject parentRef;
    // Start is called before the first frame update
    void Start()
    {
        parentRef =  transform.parent.gameObject;
        StartCoroutine(cambiarTexto());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator cambiarTexto()
    {
            textoOraculo.text = "Bienvenida otra vez Gorgona, gran trabajo con el minotauro";
        yield return new WaitForSeconds(4f);
            textoOraculo.text = "Me presento, soy el oráculo, tu único aliado en este momento";
        yield return new WaitForSeconds(4.25f);
            textoOraculo.text = "Tal vez te preguntes que haces aquí luego del combate";
        yield return new WaitForSeconds(4f);
            textoOraculo.text = "¿Recuerdas de esas habilidades que usaste?";
        yield return new WaitForSeconds(3.5f);
            textoOraculo.text = "Bueno, tu cuerpo no puede resistirlo...";
        yield return new WaitForSeconds(3f);
            textoOraculo.text = "por lo que después de cada combate tendremos que revivirte...";
        yield return new WaitForSeconds(4f);
            textoOraculo.text = "Ya estamos tardándonos, tenemos que ir a Perkonos, el primer paso hacia tu venganza";
        yield return new WaitForSeconds(5.75f);
            ComportamientoMapa.cinematica = 1;
            parentRef.SetActive(false);
        //changeTarget(player);   
            //Debug.Log(estaActivoAtaque); 
        //isWaveActive = true; // set waveStarted to true to prevent the coroutine from starting multiple times
        //Wave para "waveInterval" segundos antes de que inicie la siguiente wave     
        
    }

    IEnumerator cambiarTexto2()
    {
        yield return new WaitForSeconds(3f);
        if(ahoraAtaca)
        {
            textoOraculo.text = "Algunas veces, tus viejos poderes de Medusa vuelven a ti; cuando derrotes rivales, dejarán piedras.";
            yield return new WaitForSeconds(4f);
            textoOraculo.text = "Cuando tengas suficiente poder, podrás mejorarte de alguna manera";
            //OraculoAdios.SetActive(false);
            //TutorialManager.instance.StartLevelUp();
            yield return new WaitForSeconds(3f);
            textoOraculo.text = "Creo que eso es todo lo que necesitas por ahora. Ya nos encontraremos, recuerda que eres parte de algo mucho más grande";
            yield return new WaitForSeconds(4f);
            textoOraculo.text = "Ahora... ¡Despierta Gorgona!";
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Partida2");
        }
    }
}

