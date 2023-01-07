using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gerador_Dot : MonoBehaviour
{
    public GameObject DotOriginal;
    public bool accepted = false;
    public GameObject SphereOriginal;
    public ArrayList tree_nodes = new ArrayList();
    float[] posicaoX;
    float[] posicaoY;
    // public ArrayList posicaoX = new ArrayList();
    // public ArrayList posicaoY = new ArrayList();

    BinaryHeap heap;
    [Header("parametros")]
    [SerializeField]float razao = 0.5f;
    [SerializeField]float altura = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        float Largura = 32.0f;
        int num = 20;
        heap = new BinaryHeap(num, razao);
        CreateMarbles(num);

        

        // float razao = 0.5f
        float x = (float)((razao * Largura) / (1.0f - Math.Pow(razao, heap.AlturaHeap())));
        
        heap.Tree(1, 1, 0.0f, 0.0f, x, altura);
        
        posicaoX = heap.posx();
        posicaoY = heap.posy();

        createDots(num);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(transform.childCount);
        // var child_1 = transform.GetChild(0).gameObject;
        // accepted = child_1.GetComponent<Dot_Dad>().verifica_sub;                
        // if(accepted==true){
        //     Debug.Log("Accepted!");
        // }else{
        //     Debug.Log("WA!");
        // }
        
        
    }

    class BinaryHeap{
        int[] tree;
        int TamanhoHeap;
        int Altura;
        float razao;
        float[] posicaoX;
        float[] posicaoY;

        public BinaryHeap(int size, float razao){
            tree = new int [size+1];
            this.TamanhoHeap = 0;
            this.razao = razao;
            this.posicaoX = new float[size+1];
            this.posicaoY = new float[size+1];
            this.Altura = 1;
        }

        public float[] posx(){
            return this.posicaoX;
        }
        public float[] posy(){
            return this.posicaoY;
        }

        public int TamanhoDaHeap(){
            return this.TamanhoHeap;
        }

        public int AlturaHeap(){
            return this.Altura;
        }
        public void AdicionarElementoHeap(int elemento){
            tree[this.TamanhoHeap+1] = elemento;
            this.TamanhoHeap++;
            Heapfy(this.TamanhoHeap);
            int v = this.TamanhoHeap;  
            v--; v |= v >> 1; v |= v >> 2; v |= v >> 4; v |= v >> 8; v |= v >> 16; v++;
            this.Altura = v;
            
        }

        public void Heapfy(int indice){
            int pai = indice/2;        
            if(indice <= 1) return;
            if(tree[indice] < tree[pai]){
                int temp = tree[indice];
                tree[indice] = tree[pai];
                tree[pai] = temp;
            }

            Heapfy(pai);
        }

        public void Tree(int indice, int nivel, float posx, float posy, float rx, float ry){
            if(indice > this.TamanhoHeap)return;

            this.posicaoX[indice] = posx;
            this.posicaoY[indice] = posy;
            Tree(2*indice, nivel + 1, posx - rx, posy + ry, rx*razao, ry);
            Tree(2*indice + 1, nivel + 1, posx + rx, posy + ry, rx*razao, ry);
        }
    }

    void createDots(int qtd){
        for(int i=1;i<=qtd;i++){
            Debug.Log(posicaoX[i]);
            Debug.Log(posicaoY[i]);
            GameObject DotClone = Instantiate(DotOriginal, new Vector3(posicaoY[i], DotOriginal.transform.position.y, posicaoX[i]), DotOriginal.transform.rotation);
            DotClone.transform.parent = transform;
            GameObject SphereClone = Instantiate(SphereOriginal, new Vector3(posicaoY[i], SphereOriginal.transform.position.y, posicaoX[i]), SphereOriginal.transform.rotation);
            DotClone.SendMessage("getIndex", i,SendMessageOptions.DontRequireReceiver);
            DotClone.SendMessage("getValor", tree_nodes[i-1],SendMessageOptions.DontRequireReceiver);
        }
    }

    void CreateMarbles(int qtd)
    {
        
        for(int i=0;i<qtd;i++){  
            int valor = UnityEngine.Random.Range(1, 100);
            heap.AdicionarElementoHeap(valor);
            tree_nodes.Add(valor);
            // GameObject SphereClone = Instantiate(SphereOriginal, new Vector3(i*0.6f, SphereOriginal.transform.position.y, i*0.75f), SphereOriginal.transform.rotation);
            // SphereClone.SendMessage("getVal", valor,SendMessageOptions.DontRequireReceiver);
        }
    }
}
