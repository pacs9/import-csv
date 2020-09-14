using import_csv.Models;
using importar_motoristas.Data;
using SqlBulkTools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace importar_motoristas.Repository
{
    public class MotoristaRepository
    {
        private ADOContext Context;

        public MotoristaRepository()
        {
            Context = new ADOContext();
        }

        public void Insert(IList<Motorista> motoristas)
        {
            var bulk = new BulkOperations();

            using (TransactionScope trans = new TransactionScope())
            {
                //using (SqlConnection conn = Context.Open())
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionMotoristas"].ConnectionString))
                {
                    bulk.Setup()
                        .ForCollection(motoristas)
                        .WithTable("Motoristas")
                        .AddColumn(x => x.Id)
                        .AddColumn(x => x.CEP)
                        .AddColumn(x => x.CPFCNPJ)
                        .AddColumn(x => x.Email)
                        .AddColumn(x => x.Nome)
                        .AddColumn(x => x.Telefone)
                        .BulkInsert()
                        .Commit(conn);

                    trans.Complete();
                }
            }
        }
    }
}
