﻿using DapperExtensions;
using DapperExtensions.Sql;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Infra.Data.Database
{
    public class DatabaseSqlConnection
    {

        //public static NpgsqlConnection GetConnection(string connectionString)
        //{
        //    return new NpgsqlConnection(connectionString);

        //}

        public static NpgsqlConnection GetConnection(string connectionString, bool dev)
        {
            //if (dev)
            //    return new SqlConnection(connectionString);
            DapperExtensions.DapperExtensions.SqlDialect = new PostgreSqlDialect();
            DapperAsyncExtensions.SqlDialect = new PostgreSqlDialect();
            return new NpgsqlConnection(connectionString);
        }


    }
}
