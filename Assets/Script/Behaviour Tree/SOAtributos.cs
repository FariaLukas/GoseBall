using UnityEngine;

[CreateAssetMenu(menuName = "SO/Atributos")]
public class SOAtributos : ScriptableObject
{
    public Color color = Color.white;
    public float velocidade = 3;
    public float projetilVel = 100;
    public float alcance = 5;
    public string lable = "Bola";
    public MoveTarget moveTarget;
    public GameObject projetil;
}


