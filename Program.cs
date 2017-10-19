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
        static string doc, op2;
        static string tempdoc;
        static int soma = 0, resto = 0;
        static Regex rgx = new Regex(@"^\d*$");

        static string primeiroDigito, segundoDigito;

        static void Main(string[] args)
        {
            do
            {

                Console.WriteLine("\n\nEscolha uma das opções abaixo\n1 - Validar CPF\n2 - Validar CNPJ\n3 - Validar Cartão de Crédito\n4 - Validar RG\n5 - Validar Título Eleitoral\n0 - Sair");

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
                    case "4": //ListarRespostas(); break;
                    case "5": Environment.Exit(0); break;
                }
            } while (op2 != "0");
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

            tempdoc = doc.Substring(0,chave.Length);

            for(int i=0;i<chave.Length;i++){
                if(tipoDoc==1 || tipoDoc==2){
                    soma += Convert.ToInt16(tempdoc[i].ToString())*chave[i];
                }else
                if(tipoDoc==3){
                    if(Convert.ToInt16(tempdoc[i].ToString())*chave[i]>9){
                        soma += (Convert.ToInt16(tempdoc[i].ToString())*chave[i])-9;
                    }
                    else
                        soma += Convert.ToInt16(tempdoc[i].ToString())*chave[i];
                }
            }
            if(tipoDoc==1 || tipoDoc==2){
                resto = soma % 11;
                if(resto<2){
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
