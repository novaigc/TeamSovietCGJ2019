using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class myCamera : MonoBehaviour
{
    public Shader curshader;
    private Material curmaterial;
    [Range(0,1)]
    float edgeonly=1;
    Material material
    {
        get
        {
            if(curmaterial==null)
            {
                curmaterial = new Material(curshader);
                curmaterial.hideFlags = HideFlags.HideAndDontSave;               
            }
            return curmaterial;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
        if (!curshader && !curshader.isSupported)
            enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (edgeonly > 0)
            edgeonly = GameManager.gameManager.edgeonly;           
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(curshader!=null)
        {
            material.SetFloat("_Edgeonly", edgeonly);         
            Graphics.Blit(source, destination, material);
        }
        else
            Graphics.Blit(source, destination);
    }
   
}
