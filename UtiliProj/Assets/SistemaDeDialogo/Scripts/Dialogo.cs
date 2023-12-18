using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoDialogo
{
    pararAqui,
    passarDireto,
    mostrarRespostas
}
[CreateAssetMenu(fileName = "novoDialogo",menuName = "Scriptable/Dialogo",order = 0)]
public class Dialogo : ScriptableObject
{
    public PerfilPersonagem personagem;
    public List<string> lista;
    [Tooltip("se parar aqui ignora todo o resto das configurações")]
    public TipoDialogo tipo = TipoDialogo.pararAqui;

    [Tooltip("dialogo se for passar direto")]
    public Dialogo proximo;

    public bool botao1;
    public string msgBotao1;
    public Dialogo diagBotao1;
    public bool botao2;
    public string msgBotao2;
    public Dialogo diagBotao2;


}
