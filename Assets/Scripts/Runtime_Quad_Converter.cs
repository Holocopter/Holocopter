using UnityEngine;
using System.Collections;
using VacuumShaders.TheAmazingWireframeShader;

using System.Collections.Generic;




public class Runtime_Quad_Converter : MonoBehaviour
{
    [Range(0, 1)]
    public float normalCoef = 1;
    float normalCoef_current = 0;

    [Range(0, 1)]
    public float angleCoef = 1;
    float angleCoef_current = 0;

    [Range(0, 1)]
    public float areaCoef = 1;
    float areaCoef_current = 0;


    public  Material[] material = new Material[] { };
   // public Mesh[] originalMesh;
    public MeshFilter[] quadMesh = new MeshFilter[] { }  ;
    public Renderer [] rend = new Renderer[] { };
    int i = 0;

    public Component[] components ;


    // Use this for initialization
    void Start ()
    {
        components = GetComponentsInChildren<MeshFilter>();
        rend = GetComponentsInChildren<Renderer>();
        if (gameObject.isStatic)
        {
            enabled = false;

            Debug.Log("Static mesh convertion is not possible");

            return;
        }
    ;
        quadMesh = GetComponentsInChildren<MeshFilter>();
        foreach (Renderer red in rend)
        {
            material = red.sharedMaterials;
        }

        if (quadMesh != null)

            /* foreach (MeshFilter quad in quadMesh)
             {
                 DestroyImmediate(quad.sharedMesh);
             }*/
            // DestroyImmediate(quadMesh[i]);



            //Generate new quad mesh based on 'Coef' parameters

            foreach (MeshFilter mf in components)
            {

                Mesh originalMesh = mf.sharedMesh;


                quadMesh[i].sharedMesh = WireframeGenerator.GenerateQuads(originalMesh, normalCoef, angleCoef, areaCoef);

                i++;
            }
        i = 0;

        /* if (originalMesh == null)
         {
             Debug.LogWarning("No mesh data.");

             enabled = false;
         }*/
        /*else if(originalMesh.triangles.Length / 3 > 21000)
        {
            Debug.LogWarning("Can not convert mesh with more then 21000 triangles.");

            originalMesh = null;
            enabled = false;
        }*/

        i = 0;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (originalMesh == null)
          //  return;

        //If any of the properties has changed, than generate new mesh
        if ((normalCoef_current != normalCoef) ||
           (angleCoef_current != angleCoef) ||
           (areaCoef_current != areaCoef))
        {
            normalCoef_current = normalCoef;
            angleCoef_current = angleCoef;
            areaCoef_current = areaCoef;


            //Do not forget to delete previously generated quad mesh
            if (quadMesh != null)

               /* foreach (MeshFilter quad in quadMesh)
                {
                    DestroyImmediate(quad.sharedMesh);
                }*/
                    // DestroyImmediate(quadMesh[i]);



                    //Generate new quad mesh based on 'Coef' parameters

                    foreach (MeshFilter mf in components)
            {
              
                Mesh originalMesh = mf.sharedMesh;

           
                //quadMesh[i].sharedMesh = WireframeGenerator.GenerateQuads(originalMesh, normalCoef, angleCoef, areaCoef);
         
                i++;
            }
            i = 0;
            
            //Assign new mesh
           /* if (quadMesh != null)
            {
                components = GetComponentsInChildren<MeshFilter>();

                foreach (MeshFilter mf in components)
                {
                    mf.sharedMesh = quadMesh[i].sharedMesh;
                    i++;
                }
                i = 0;
                //GetComponent<MeshFilter>().sharedMesh = quadMesh;


                //Just make wireframe visible
                foreach (Material mat in material)
                {
                   // mat.SetColor("_V_WIRE_Color", new Color(1.0f, 1.0f, 1.0f, 0.5f));
                    
                }
               
            }
            else
            {
                Debug.Log("houston we have a problem");
            }*/

        }
    }
}