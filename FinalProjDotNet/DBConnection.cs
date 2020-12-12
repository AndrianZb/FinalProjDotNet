using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace FinalProjDotNet
{
    sealed class DBConnection
    {
        DBConnection() { }

        static readonly DBConnection DBC = new DBConnection();

        public static DBConnection instance
        {
            get { return DBC;  }
        }

        private string conString = ConfigurationManager.ConnectionStrings["ContactConn"].ConnectionString;
        public List<ContactsCreator> getData()
        {
            List<ContactsCreator> dataList = new List<ContactsCreator>();

            using (var con = new SqlConnection(conString))
            {
                con.Open();
                SqlCommand sc = new SqlCommand("Select ID, FirstName, LastName, PhoneNum, Email from Contacts", con);

                using (SqlDataReader sdr = sc.ExecuteReader())
                {
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
                }
            }
            return dataList;
        }
        public void Delete(int id)
        {
            using (var con = new SqlConnection(conString))
            {
                con.Open();
                SqlCommand sc;
                using (sc = new SqlCommand("Delete from Contacts where id=" + id, con))
                {
                    sc.ExecuteNonQuery();
                }
            }
        }
        public void DeleteAndUpdate(List<ContactsCreator> dataList)
        {
            using (var con = new SqlConnection(conString))
            {
                con.Open();
                SqlCommand sc;
                using (sc = new SqlCommand("Delete from Contacts", con))
                {
                    sc.ExecuteNonQuery();
                }
                
                SqlCommand sc2;
                foreach (var x in dataList) {
                    using (sc2 = new SqlCommand("Insert Into Contacts(FirstName, LastName, PhoneNum, Email) Values(@FirstName, @LastName, @PhoneNum, @Email)", con))
                    {
                        sc2.Parameters.AddWithValue("@FirstName", x.FirstName.ToString());
                        sc2.Parameters.AddWithValue("@LastName", x.LastName.ToString());
                        sc2.Parameters.AddWithValue("@PhoneNum", x.PhoneNum.ToString());
                        sc2.Parameters.AddWithValue("@Email", x.Email.ToString());
                        sc2.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Add(string[] list)
        {
            using (var con = new SqlConnection(conString))
            {
                con.Open();
                SqlCommand sc;

                using (sc = new SqlCommand("Insert Into Contacts(FirstName, LastName, PhoneNum, Email) Values(@FirstName, @LastName, @PhoneNum, @Email)", con))
                {
                    sc.Parameters.AddWithValue("@FirstName", list[0].ToString());
                    sc.Parameters.AddWithValue("@LastName", list[1].ToString());
                    sc.Parameters.AddWithValue("@PhoneNum", list[2].ToString());
                    sc.Parameters.AddWithValue("@Email", list[3].ToString());
                    sc.ExecuteNonQuery();
                }
            }
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
