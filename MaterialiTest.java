/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package materialitest;

import java.util.ArrayList;
import java.util.Random;

/**
 *
 * @author bazza
 */
public class MaterialiTest {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        Random random = new Random();


	int baseMateriali = 0;
	int tier = 5;
	int numTypeMateriali = 6;
	int numTypeArmi = 4;
        String[] nomiArmi = new String[numTypeArmi];
        nomiArmi[0] = "spada";
        nomiArmi[1] = "picca";
        nomiArmi[2] = "martello";
        nomiArmi[3] = "lancia";
        
        String[] colori = new String[numTypeMateriali];
        colori[0] = "rosso";
        colori[1] = "blu";
        colori[2] = "giallo";
        colori[3] = "viola";
        colori[4] = "verde";
        colori[5] = "magenta";
	
	ArrayList<TypeArmi> armi = new ArrayList();
        
        
        ArrayList<Combinazione> combinazioni = new ArrayList();
       
        int cont=0;
	for(int i=1;i<=tier;i++){
            baseMateriali = baseMateriali+25;
		for(int j=0;j<numTypeArmi;j++){
                    System.out.println("tier: "+i);
                    TypeArmi arma = new TypeArmi(nomiArmi[j], i);
                    Combinazione comb = arma.setMateriale(colori,i+1, baseMateriali, combinazioni);
                    combinazioni.add(cont, comb);
                    armi.add(cont,arma);
                    cont++;
		}
	}
        
        for(TypeArmi temp: armi){
            System.out.println("Tier: " + temp.tier+ "TypeArma: "+temp.tipoArma);
            for(TypeMateriali matTemp: temp.materiali){
                System.out.println("TypeMateriale: " + matTemp.typeMateriale + "Num: " + matTemp.num);
            }
            
        }

	

    }
    
}
