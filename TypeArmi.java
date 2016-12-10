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
public class TypeArmi {
    String tipoArma;
    TypeMateriali materiali[];
    int tier;
    Random random = new Random();
    
    TypeArmi(String tipoArma, int tier){
        this.tipoArma = tipoArma;
        System.out.println("tier in construct: "+tier);
        this.tier = tier;
        materiali = new TypeMateriali[tier+1];
    }
    
    Combinazione setMateriale(String colori[],int numColori, int num, ArrayList <Combinazione> combinazioni){
        System.out.println("Num totali: "+num);
        int rand;
        boolean res = true;
        Combinazione comb = null;
        while(res){
            for(int i=0;i<numColori;i++){
                if(i<numColori-1){
                    System.out.println("Siamo nel ciclo " + i);
                    rand = random.nextInt(num);
                    System.out.println("rand: "+rand);
                    TypeMateriali materiale = new TypeMateriali(colori[i],rand);
                    System.out.println("Tipo Arma: "+this.tipoArma+ " colore: "+ materiale.typeMateriale + " num: "+materiale.num);
                    materiali[i] = materiale;
                    num = num - rand;
                    System.out.println(num);
                }
                else{
                    TypeMateriali materiale = new TypeMateriali(colori[i],num);
                    materiali[i] = materiale;
                    System.out.println("Tipo Arma: "+this.tipoArma+ " colore: "+ materiale.typeMateriale + " num: "+materiale.num);
                }
            }
            comb = new Combinazione(materiali);
            boolean exist = false;
            if(combinazioni.isEmpty()) res=false;
            
            else {
                for(Combinazione temp: combinazioni){
                    if(comb.equals(temp)) exist = true;
               
                }
            if(!exist) res=false;
            }
            
        }
       return comb;
    }
    

}

    
    

