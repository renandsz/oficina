using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EfeitoDigitadorB : MonoBehaviour
{
    private TextMeshProUGUI compTexto;
    public float tempoPadrao = 0.04f;
    public float pausaBreve = 0.08f;
    public float pausaLonga = 0.1f;
    public bool imprimindo = false;
    private void Awake()
    {
        TryGetComponent(out compTexto);
        Limpar();
    }

    public void ImprimirMensagem(string mensagem)
    {
        if (imprimindo) return;
        imprimindo = true;
        StartCoroutine(LetraPorLetra(mensagem));
    }
    IEnumerator LetraPorLetra(string mensagem)
    {
        string msg = "";
        foreach (var letra in mensagem)
        {
            msg += letra;
            compTexto.text = msg;
            if (letra == ',')
            {
                yield return new WaitForSeconds(pausaBreve);
            }
            else if (letra is '.' or '!' or '?')
            {
                yield return new WaitForSeconds(pausaLonga);
            }
            else
            {
                yield return new WaitForSeconds(tempoPadrao);
            }
            
        }
        imprimindo = false;
        StopCoroutine(LetraPorLetra(mensagem));
    }
    
    public void Limpar()
    {
        compTexto.text = "";
    }


    
}
