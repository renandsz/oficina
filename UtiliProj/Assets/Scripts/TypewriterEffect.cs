using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    private string textoOriginal;
    private TextMeshProUGUI compTexto;

    public float tempoPadrao = 0.04f;
    public float pausaBreve = 0.075f;
    public float pausaLonga = 0.1f;

    public bool imprimindo = false;

    private void Awake()
    {
        TryGetComponent(out compTexto);
        textoOriginal = compTexto.text;
        Limpar();
    }

    void OnEnable()
    {
        //StartCoroutine(LetraPorLetra(textoOriginal));
    }

    public void ExibirMensagem(string msg)
    {
        Limpar();
        imprimindo = true;
        StartCoroutine(LetraPorLetra(msg));
    }

    IEnumerator LetraPorLetra(string msgParaExibir)
    {
        while (imprimindo)
        {
            string msg = "";
            foreach (var caractere in msgParaExibir)
            {
                msg += caractere;
                compTexto.text = msg;
                if (caractere == ',')
                    yield return new WaitForSeconds(pausaBreve);
                else if (caractere is '!' or '.' or '?')
                    yield return new WaitForSeconds(pausaLonga);
                else
                    yield return new WaitForSeconds(tempoPadrao);
            }
            imprimindo = false;
        }
        StopCoroutine(LetraPorLetra(msgParaExibir));
    }

    public void Limpar()
    {
        compTexto.text = "";
    }
}
