using System;
using FirebirdSql.Data.FirebirdClient;

namespace Sistema
{
    internal class Conexao : IDisposable
    {
        public string conec = @"User=SYSDBA;Password=masterkey;Database=C:\Users\caio.santos\source\repos\sistemaPDV\Banco\PDV.fdb;DataSource=localhost;Port=3050;Dialect=3;";

        private FbConnection _Connection;
        private bool _IsDisposed;

        public Conexao()
        {
            this._Connection = new FbConnection(conec);
        }

        public void Open()
        {
            _Connection.Open();
        }

        public FbConnection GetConnection()
        {
            return _Connection;
        }

        public void Dispose()
        {
            if (!_IsDisposed)
            {
                _Connection.Dispose();
                _IsDisposed = true;
            }
        }
    }
}
