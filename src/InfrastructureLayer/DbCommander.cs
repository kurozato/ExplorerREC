using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using BlackSugar.Entity;
using BlackSugar.Repository;
using Dapper;

namespace BlackSugar.Repository
{
    public interface IDbCommander
    {
        int Execute(string commandText);

        IEnumerable<TModel> Get<TModel>(string commandText, object param);
    }

    public class DbCommander : IDbCommander
    {
        private IGeneralSetting _generalSetting;

        private string _connectionString;

        public DbCommander(IGeneralSetting generalSetting)
        {
            _generalSetting = generalSetting ?? throw new ArgumentNullException(nameof(generalSetting)); 
            _connectionString = new SQLiteConnectionStringBuilder { DataSource = _generalSetting.RecodePath }.ToString();
        }

        public int Execute(string commandText)
        {
            int result;
            using (var cn = new SQLiteConnection(_connectionString))
            {
                cn.Open();
                using (var tx = cn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {

                    using (var cmd = new SQLiteCommand(cn))
                    {
                        cmd.CommandText = commandText;
                        result = cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                }

            }

            return result;
        }

        public IEnumerable<TModel> Get<TModel>(string commandText, object param)
        {
            IEnumerable<TModel> results = null;

            using (var cn = new SQLiteConnection(_connectionString))
            {
                cn.Open();
                using (var tx = cn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {

                    results = cn.Query<TModel>(commandText, param);

                    tx.Commit();
                }

            }
            return results;
        }

    }
}
