using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{

    private void TryCatch()
    {
        int[] array = new int[5];

        try
        {
            Debug.Log("A");
            array[6]=0;
        }
        catch
        {
            Debug.Log("B");
        }
       /*  catch 
        {   
            Debug.Log("c");
        } */
        finally
        {
            Debug.Log("D");
        }

    }
    void Awake()
    {
        var cam = Camera.main;
        cam.transform.position = Vector3.one * 5;
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = Vector3.one;

        cube.transform.SetParent(cam.transform,worldPositionStays:true);
        Debug.Log(cube.transform.localPosition);
    }
    public string GetA()
    {
        return "BASE A";
    }
    public virtual string GetB()
    {
        return "BASE B";
    }

    public class Inherit: Base
    {
        public new string GetA()
        {
            return "inherit A";
        }

        public override string GetB()
        {
            return "inherit B";
        }
    }

    void Start()
    {
       var list = new List<int>() {1,2,2,3,3,3,4,5};
       var set = new HashSet<int>(list);
       Debug.Log(set.Count);
    }
}
