using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RepresentVotingSystem : MonoBehaviour
{
    // Start is called before the first frame update
    List<int> devotes = new List<int>();
   public int biggestfound = 0;
    public int winnerint;
   // public GameObject winner;
   //  public Quaternion objectModification;
    int margin;
    //void Start()
    //{
    //    devotes.Add(0);
    //    devotes.Add(2);
    //    devotes.Add(1);
    //    devotes.Add(3);
    //    devotes.Add(3);
    //    devotes.Add(2);
    //    devotes.Add(2);
    //    devotes.Add(4);
    //    votearraytovisual(devotes);
    //}

    // Update is called once per frame
    public void votearraytovisual(List<int>votes, GameObject winner, Quaternion objectModification)
    {
        var g = votes.GroupBy(i => i);

        foreach (var grp in g)
        {
            Debug.LogFormat("{0} {1}", grp.Key, grp.Count()); //display in console the results
           
            if (grp.Count() >= biggestfound)
            {
                biggestfound = grp.Count();
            }
           
        }
        Debug.Log(biggestfound);

        foreach (var grp in g) //look at all votes
        {
            for (int i = 0; i < grp.Count(); i++) //see howmany players have been voted on
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                var cubeRenderer = cube.GetComponent<Renderer>();
                cubeRenderer.material.SetColor("_Color", Color.black);
                cube.transform.position = new Vector3(grp.Key,i, 0); //location of the cube

                if (cube.transform.position.x > 0)
                {
                     margin = (int)cube.transform.position.x;
                    cube.transform.position = new Vector3(grp.Key+ margin, i, 0); //location of the cube
                }
                    if (cube.transform.position.y > 0)
                {
                   
                    Color32 cubecolor = new Color32(0, (byte)(40* cube.transform.position.y), 0, 100);
                    cubeRenderer.material.SetColor("_Color", cubecolor);
                }
            } 
            if(grp.Count() == biggestfound)
            {
                
                GameObject win = Instantiate(winner);
                win.transform.localScale *=2;
                win.transform.position = new Vector3(grp.Key + margin, 0, -1);
                if (win.transform.position.x == 0) { win.transform.position = new Vector3(grp.Key, 0, -1); }
                win.transform.rotation = objectModification;
            }
            winnerint = grp.Key;
        }
       

    }
}
