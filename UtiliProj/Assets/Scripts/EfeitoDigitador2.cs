using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EfeitoDigitador2 : MonoBehaviour
{
    private TextMeshProUGUI compTexto;
    private string mensagemOriginal;
    public float tempoPadrao = 0.04f;
    public float pausaBreve = 0.09f;
    public float pausaLonga = 0.2f;

    private AudioSource _audioSource;

    private void Awake()
    {
        TryGetComponent(out compTexto);
        TryGetComponent(out _audioSource);
        mensagemOriginal = compTexto.text;
        compTexto.text = "";
    }

    public void ExibirMesagem(string msg)
    {
        StartCoroutine(LetraPorLetra(msg));
    }
    IEnumerator LetraPorLetra(string mensagem)
    {
        string msg = "";
        foreach (var letra in mensagem)
        {
            msg += letra;
            compTexto.text = msg;
            if (_audioSource.isPlaying == false)
            {
                _audioSource.Play();
            }
            if (letra == ',')
            {
                yield return new WaitForSeconds(pausaBreve);
            }
            else if (letra is '.' or '?' or '!')
            {
                yield return new WaitForSeconds(pausaLonga);
            }
            else
            {
                yield return new WaitForSeconds(tempoPadrao);
            }
            
        }
        StopCoroutine(LetraPorLetra(mensagem));
    }
}
