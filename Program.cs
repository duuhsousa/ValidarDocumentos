using System;
using System.Text.RegularExpressions;

namespace ValidarCPF
{
    class Program
    {
        static int[] chaveCPF = {10,9,8,7,6,5,4,3,2};
        static int[] chaveCPF2 = {11,10,9,8,7,6,5,4,3,2};
        static int[] chaveCNPJ = {5,4,3,2,9,8,7,6,5,4,3,2};
        static int[] chaveCNPJ2 = {6,5,4,3,2,9,8,7,6,5,4,3,2};
        static int[] chaveCredito = {2,1,2,1,2,1,2,1,2,1,2,1,2,1,2};
        static int[] chaveRG = {9,8,7,6,5,4,3,2};
        static int[] chaveTitulo = {4,5,6,7,8,9};
        static int[] chaveTitulo2 = {7,8,9};
        static string doc, op2;
        static string tempdoc;
        static int soma = 0, resto = 0;
        static Regex rgx = new Regex(@"^\d*$");
        static Regex rgxRG = new Regex(@"^\d+?(x|X|\d+)$");

        static string primeiroDigito, segundoDigito;

        static void Main(string[] args)
        {
            do
            {
                
                Console.WriteLine("\nEscolha uma das opções abaixo\n1 - Validar CPF\n2 - Validar CNPJ\n3 - Validar Cartão de Crédito\n4 - Validar RG\n5 - Validar Título Eleitoral\n0 - Sair");
                do
                {
                    op2 = Console.ReadLine();
                } while (op2 != "1" && op2 != "2" && op2 != "3" && op2 != "4" && op2 != "5" && op2 != "0");

                switch (op2)
                {
                    case "0": Environment.Exit(0); break;
                    case "1": ValidarCPF(); break;
                    case "2": ValidarCNPJ(); break;
                    case "3": ValidarCredito(); break;
                    case "4": ValidarRG(); break;
                    case "5": ValidarTitulo(); break;
                }
            } while (op2 != "0");
        }

        private static void ValidarTitulo()
        {
            do
            {
                Console.Write("Digite seu Titulo Eleitoral: ");
                doc = Console.ReadLine();
            } while (doc.Length != 10 || !rgx.IsMatch(doc));

            primeiroDigito = ValidaDigito(chaveTitulo,5);
            Console.WriteLine(primeiroDigito);

            if (primeiroDigito != doc.Substring(8, 1))
            {
                Console.WriteLine("Titulo Eleitoral inválido!1");
            }
            else
            {
                segundoDigito = ValidaDigito(chaveTitulo2,6);
                if (doc.EndsWith(segundoDigito) == true)
                {
                    Console.WriteLine("Titulo Eleitoral válido!");
                }
                else
                {
                    Console.WriteLine("Titulo Eleitoral inválido!2");
                }
            }
        }

        private static void ValidarRG()
        {
            do
            {
                Console.Write("Digite seu RG: ");
                doc = Console.ReadLine();
            } while (doc.Length != 9 || !rgxRG.IsMatch(doc));

            primeiroDigito = ValidaDigito(chaveRG,4);
            if (doc.ToUpper().EndsWith(primeiroDigito))
            {
                Console.Clear();
                Console.WriteLine("\n\nRG válido!");
            }
            else{
                Console.Clear();
                Console.WriteLine("\n\nRG inválido!");
            }
        }

        private static void ValidarCredito()
        {
            do
            {
                Console.Write("Digite o numero do seu Cartao: ");
                doc = Console.ReadLine();
            } while (doc.Length != 16 || !rgx.IsMatch(doc));

            primeiroDigito = ValidaDigito(chaveCredito,3);

            if (doc.EndsWith(primeiroDigito))
            {
                Console.WriteLine("Cartão válido!");
            }
            else{
                Console.WriteLine("Cartão inválido!");
            }
        }

        private static void ValidarCNPJ()
        {

            do
            {
                Console.Write("Digite seu CNPJ: ");
                doc = Console.ReadLine();
            } while (doc.Length != 14 || !rgx.IsMatch(doc));

            primeiroDigito = ValidaDigito(chaveCNPJ,2);

            if (primeiroDigito != doc.Substring(12, 1))
            {
                Console.WriteLine("CNPJ inválido!");
            }
            else
            {
                segundoDigito = ValidaDigito(chaveCNPJ2,2);
                if (doc.EndsWith(segundoDigito) == true)
                {
                    Console.WriteLine("CNPJ válido!");
                }
                else
                {
                    Console.WriteLine("CNPJ inválido!");
                }
            }
        }

        private static string ValidaDigito(int[] chave, int tipoDoc)
        {
           soma = 0;
           resto = 0;
           if(tipoDoc==6)
           {
               tempdoc = doc.Substring(6,chave.Length);
           }
           else
                tempdoc = doc.Substring(0,chave.Length);

            for(int i=0;i<chave.Length;i++){
                if(tipoDoc==1 || tipoDoc==2 || tipoDoc==4 || tipoDoc==5 || tipoDoc==6){
                    soma += Convert.ToInt16(tempdoc[i].ToString())*chave[i];
                }else if(tipoDoc==3){
                    if(Convert.ToInt16(tempdoc[i].ToString())*chave[i]>9){
                        soma += (Convert.ToInt16(tempdoc[i].ToString())*chave[i])-9;
                    }
                    else
                        soma += Convert.ToInt16(tempdoc[i].ToString())*chave[i];
                }
            }

            if(tipoDoc==1 || tipoDoc==2 || tipoDoc==4 || tipoDoc==5 || tipoDoc==6){
                resto = soma % 11;
                if(resto == 0 && (doc.Substring(6,2)=="01" || doc.Substring(6,2)=="02")){
                    return "1";
                }
                else if(resto < 10 && (tipoDoc==5 || tipoDoc==6)){
                    return resto.ToString();
                }
                else if(resto == 10 && (tipoDoc==5 || tipoDoc==6)){
                    return "0";
                }
                else if (resto == 10 && tipoDoc==4){
                    return "X";
                }
                else if(resto < 10 && tipoDoc==4){
                    return resto.ToString();
                }
                else if(resto<2)
                {
                    return "0";        
                }
                else
                {
                    return (11-resto).ToString();
                }
            } else
                if(tipoDoc==3){
                    int somatemp=soma;
                    while(somatemp % 10 != 0){
                        somatemp++;
                    }
                    return (somatemp-soma).ToString();
                }else
                    return "0";           
        }

        private static void ValidarCPF()
        {
            do
            {
                Console.Write("Digite seu CPF: ");
                doc = Console.ReadLine();
            } while (doc.Length != 11 || !rgx.IsMatch(doc));

            primeiroDigito = ValidaDigito(chaveCPF,1);

            if (primeiroDigito != doc.Substring(9, 1))
            {
                Console.WriteLine("CPF inválido!");
            }
            else
            {
                segundoDigito = ValidaDigito(chaveCPF2,1);
                if (doc.EndsWith(segundoDigito) == true)
                {
                    Console.WriteLine("CPF válido!");
                }
                else
                {
                    Console.WriteLine("CPF inválido!");
                }
            }
        }
    }
}
