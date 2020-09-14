using import_csv.Models;
using importar_motoristas.Repository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace importar_motoristas.Business
{
    public class ImportarMotoristas
    {
        public void Importar(string path)
        {                        
            try
            {
                string cabecalho = "Nome,Telefones,E-mail,cpf-cpnf,cep";
                StreamReader reader = new StreamReader(File.OpenRead(path));

                ConcurrentBag<Motorista> motoristas = new ConcurrentBag<Motorista>();


                Stopwatch timerParallelForeach = new Stopwatch();
                timerParallelForeach.Start();

                Parallel.ForEach(LerLinha(reader), linhaAtual =>
                {
                    var valores = linhaAtual.Split(',');

                    if (linhaAtual != cabecalho)
                    {
                        motoristas.Add(new Motorista
                        {
                            Nome = valores[0],
                            Telefone = valores[1],
                            Email = valores[2],
                            CPFCNPJ = valores[3],
                            CEP = valores[4]
                        });
                    }
                });

                MotoristaRepository motoristaRepository = new MotoristaRepository();

                motoristaRepository.Insert(motoristas.ToList());

                timerParallelForeach.Stop();
                MessageBox.Show(string.Concat("Importado ", motoristas.Count(), " em: ", timerParallelForeach.Elapsed), "Importação realizada com sucesso");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao realizar a importação dos registros: " + ex.ToString());
                return;
            }
        }

        public static IEnumerable<string> LerLinha(StreamReader sr)
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}
