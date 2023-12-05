using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "novoPerfil", menuName = "Scriptable/Perfil", order = 0)]
public class PerfilPersonagem : ScriptableObject
{
    public string nome;
    public Sprite sprite;
    public AudioClip voz;

    public void DebugarQuemTaFalando()
    {
        Debug.Log($"{nome} está falando");
    }
}
