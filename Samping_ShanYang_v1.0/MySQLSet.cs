using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Samping_ShanYang_v1._0
{
    internal class MySQLSet
    {
        private MySqlConnection connection;
        string connectionString;

        public MySQLSet(string IP, string port, string user, string pass)
        {
            connectionString = string.Format("server={0};port={1};uid={2};pwd={3};", IP, port, user, pass);
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        public void CheckMysqlConnection()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                    Console.WriteLine("数据库连接已重新建立");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("数据库连接异常：" + ex.Message);
                try
                {
                    connection.Close();
                    connection.Open();
                    Console.WriteLine("数据库连接已重新建立");
                }
                catch (SqlException ex2)
                {
                    Console.WriteLine("无法重新连接数据库：" + ex2.Message);
                }
            }
        }

        //public bool WriteData(int key, double[][] channels, DateTime startDate)
        //{
        //    string insertQuery;

        //    // 根据传入的 key 决定插入哪张表
        //    if (key == 0)
        //    {
        //        insertQuery = "INSERT INTO `zsh`.`maincurvedata`(`StartDate`, `CH1`, `CH2`, `CH3`, `CH4`, `CH5`, `CH6`, `CH7`, `CH8`) " +
        //                      "VALUES(@StartDate, @CH1, @CH2, @CH3, @CH4, @CH5, @CH6, @CH7, @CH8)";
        //    }
        //    else if (key == 1)
        //    {
        //        insertQuery = "INSERT INTO `zsh`.`mainmotordata`(`StartDate`, `CH1`, `CH2`, `CH3`, `CH4`, `CH5`, `CH6`, `CH7`, `CH8`) " +
        //                      "VALUES(@StartDate, @CH1, @CH2, @CH3, @CH4, @CH5, @CH6, @CH7, @CH8)";
        //    }
        //    else
        //    {
        //        return false; // Invalid key
        //    }

        //    // 使用 MySQL 命令对象执行插入
        //    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
        //    {
        //        MySqlTransaction transaction = connection.BeginTransaction();  // 开始事务
        //        command.Transaction = transaction;

        //        try
        //        {
        //            // 添加开始时间
        //            command.Parameters.AddWithValue("@StartDate", startDate);

        //            // 遍历 8 个通道，插入每个通道的数据
        //            for (int i = 0; i < 8; i++)
        //            {
        //                if (channels[i].Length == 1)
        //                {
        //                    // 如果每个通道只有一个值，直接使用该值
        //                    command.Parameters.AddWithValue($"@CH{i + 1}", channels[i][0]);
        //                }
        //                else
        //                {
        //                    // 如果每个通道有多个值，使用逗号连接多个值
        //                    command.Parameters.AddWithValue($"@CH{i + 1}", string.Join(",", channels[i]));
        //                }
        //            }

        //            // 执行插入操作
        //            command.ExecuteNonQuery();

        //            // 提交事务
        //            transaction.Commit();
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            // 回滚事务
        //            transaction.Rollback();
        //            Console.WriteLine("WriteData error: " + ex.Message);
        //            return false;
        //        }
        //    }
        //}

        //public bool WriteData(int port, double[][] channels, DateTime startDate)
        //{
        //    string insertQuery;
        //    // 根据传入的端口号选择不同的表
        //    if (port == 9001)
        //    {
        //        insertQuery = "INSERT INTO `zsh`.`maincurvedata`(`StartDate`, `CH1`, `CH2`, `CH3`, `CH4`, `CH5`, `CH6`, `CH7`, `CH8`) " +
        //                      "VALUES(@StartDate, @CH1, @CH2, @CH3, @CH4, @CH5, @CH6, @CH7, @CH8)";
        //    }
        //    else if (port == 9002)
        //    {
        //        insertQuery = "INSERT INTO `zsh`.`maincurvedata2`(`StartDate`, `CH1`, `CH2`, `CH3`, `CH4`, `CH5`, `CH6`, `CH7`, `CH8`) " +
        //                      "VALUES(@StartDate, @CH1, @CH2, @CH3, @CH4, @CH5, @CH6, @CH7, @CH8)";
        //    }
        //    else
        //    {
        //        return false; // 无效端口号
        //    }

        //    // 使用 MySQL 命令对象执行插入
        //    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
        //    {
        //        MySqlTransaction transaction = connection.BeginTransaction();  // 开始事务
        //        command.Transaction = transaction;

        //        try
        //        {
        //            // 遍历每个数据点，并为每个数据点插入一行
        //            for (int i = 0; i < channels[0].Length; i++) // channels[0].Length 代表数据点的数量
        //            {
        //                // 插入每一行的数据
        //                command.Parameters.Clear(); // 清除之前的参数
        //                command.Parameters.AddWithValue("@StartDate", startDate);
        //                for (int ch = 0; ch < 8; ch++)
        //                {
        //                    command.Parameters.AddWithValue($"@CH{ch + 1}", channels[ch][i]);
        //                }

        //                // 执行插入操作
        //                command.ExecuteNonQuery();
        //            }

        //            // 提交事务
        //            transaction.Commit();
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            // 回滚事务
        //            transaction.Rollback();
        //            Console.WriteLine("WriteData error: " + ex.Message);
        //            return false;
        //        }
        //    }
        //}





    }
}
