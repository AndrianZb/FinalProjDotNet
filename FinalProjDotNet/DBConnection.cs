using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FinalProjDotNet
{
    class DBConnection
    {
        DBConnection() { }

        static readonly DBConnection DBC = new DBConnection();

        public static DBConnection instance
        {
            get { return DBC;  }
        }

        public List<ContactsCreator> getData()
        {
            List<ContactsCreator> dataList = new List<ContactsCreator>();

            var con = new SqlConnection(@"data source=localhost\SQLEXPRESS;database = ContactDatabase;Trusted_Connection = True");

            SqlCommand sc = new SqlCommand("Select ID, First_Name, Last_Name, Address, Phone_Num, email from Contact", con);

            con.Open();

            SqlDataReader sdr = sc.ExecuteReader();

            while (sdr.Read())
            {
                dataList.Add(new ContactsCreator()
                {
                    Id = (int)sdr["Id"],
                    FirstName = checkIfNull(sdr, "FirstName"),
                    LastName = checkIfNull(sdr, "LastName"),
                    PhoneNum = checkIfNull(sdr, "PhoneNum"),
                    Email = checkIfNull(sdr, "Email")
                });
            }

                return dataList;
            
        }

        public string checkIfNull(SqlDataReader reader, string column)
        {
            if (reader[column].GetType() == typeof(DBNull))
            {
                return String.Empty;
            }
            else
            {
                return (string)reader[column];
            }
        }
    }
}
