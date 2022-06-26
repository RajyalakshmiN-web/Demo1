using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TechTalk.SpecFlow;

namespace SpecFlowProject2.StepDefinitions
{
    [Binding]
    public class DemoStepDefinitions
    {
        public DataTable DataTable2 { get; set; }
        public DataTable DataTable1 { get; set; }

        //public static readonly string TestConnectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        [Given(@"Connect to MS SQL Database and extract the source table records")]
        public void GivenConnectToMSSQLDatabaseAndExtractTheSourceTableRecords()
        {
            SqlConnection ThisConnection = new SqlConnection("Data Source =LAPTOP-PG8E18BR; Initial Catalog = SampleTest; Integrated Security = true");
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select * from CustomerTable1";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            this.DataTable1 =new DataTable();
            //DataTable dataTable = new DataTable();
            this.DataTable1.Load(thisReader);
            foreach (DataRow item in this.DataTable1.Rows)
            {
                Console.WriteLine(Convert.ToString(item["Customer_ID"]) + "\t" + Convert.ToString(item["Customer_Name"]) + "\t" + Convert.ToString(item["Customer_City"]) + "\t" + Convert.ToString(item["Customer_Postcode"]));
            }
            thisReader.Close();
            ThisConnection.Close();
        }

        [Given(@"Connect to MS SQL Database and extract the target table records")]
        public void GivenConnectToMSSQLDatabaseAndExtractTheTargetTableRecords()
        {
            SqlConnection ThisConnection = new SqlConnection("Data Source =LAPTOP-PG8E18BR; Initial Catalog =Demo; Integrated Security = true");
            ThisConnection.Open();
            SqlCommand thisCommand = ThisConnection.CreateCommand();
            thisCommand.CommandText = "Select * from CustomerTable2";
            SqlDataReader thisReader = thisCommand.ExecuteReader();
            this.DataTable2 = new DataTable();
            // DataTable dataTable = new DataTable();
            this.DataTable2.Load(thisReader);
            foreach (DataRow item in this.DataTable2.Rows)
            {
                Console.WriteLine(Convert.ToString(item["ID"]) + "\t" + Convert.ToString(item["Name"]) + "\t" + Convert.ToString(item["City"]) + "\t" + Convert.ToString(item["Postcode"]));
            }
            thisReader.Close();
            ThisConnection.Close();
        }

        [When(@"Compare both table extracts and generate results")]
        public void WhenCompareBothTableExtractsAndGenerateResults()
        {
            DataTable dtMerged = (from a in this.DataTable1.AsEnumerable()
                                  join b in this.DataTable2.AsEnumerable()
                                  on a["Customer_Name"].ToString() equals b["Name"].ToString()
                                  into g
                                  select a).CopyToDataTable();
            if (dtMerged != null && dtMerged.Rows.Count > 0)
            {
                foreach (DataRow item in dtMerged.Rows)
                {
                    Console.WriteLine(Convert.ToString(item["Customer_ID"]) + "\t" + Convert.ToString(item["Customer_Name"]) + "\t" +
 Convert.ToString(item["Customer_City"]) + "\t" + Convert.ToString(item["Customer_Postcode"]));
                }
            }
            //throw new PendingStepException();
        }


    }
}


