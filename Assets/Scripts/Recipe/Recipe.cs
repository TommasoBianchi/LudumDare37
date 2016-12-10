using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe {
 public List<TypeArmi> createListArmiRecipe() {
        Random random = new Random();


	int baseMateriali = 0;
	int tier = 5;
	int numTypeMateriali = 6;
	int numTypeArmi = 4;
        string[] nomiArmi = new string[numTypeArmi];
        nomiArmi[0] = "spada";
        nomiArmi[1] = "picca";
        nomiArmi[2] = "martello";
        nomiArmi[3] = "lancia";
        
        string[] colori = new string[numTypeMateriali];
        colori[0] = "rosso";
        colori[1] = "blu";
        colori[2] = "giallo";
        colori[3] = "viola";
        colori[4] = "verde";
        colori[5] = "magenta";
	
	    List<TypeArmi> armi = new List<TypeArmi>();
        
        
        List<Combinazioni> combinazioni = new List<Combinazioni>();
       
        int cont=0;
	for(int i=1;i<=tier;i++){
            baseMateriali = baseMateriali+25;
		for(int j=0;j<numTypeArmi;j++){
                    //System.out.println("tier: "+i);
                    TypeArmi arma = new TypeArmi(nomiArmi[j], i);
                    Combinazioni comb = arma.setMateriale(colori,i+1, baseMateriali, combinazioni);
                    combinazioni.Add(comb);
                    armi.Add(arma);
                    cont++;
		}
	}
    return armi;
    }       

     /*foreach(TypeArmi temp in *nome della lista){
            temp.tier;
            temp.tipoArma;
            foreach(TypeMateriali matTemp in temp.materiali){
                matTemp.typeMateriale
                matTemp.num
            }
            
        }*/

   
}
