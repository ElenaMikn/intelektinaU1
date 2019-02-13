using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    private int sms_norimas, min_norimas, gb_norimas, eur_norimas, sms_prioritetas, min_prioritetas, gb_prioritetas, eur_prioritetas;
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
   
        skaiciuoti();
    }
    private void skaiciuoti()
    {
        //string connectionString = @"server=ELENOSHP\SQLEXPRESS; database=Parduotuve; User ID=dbuser; pwd=dbuser"; // const
        string connectionString = @"Server=DESKTOP-4JKI6TF\SQLEXPRESS; Database=planai; User Id=emuser; Password=emuser;";// Properties.Settings.Default.ConnectionString;

        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        SqlCommand cmd = new SqlCommand("select * from galimiPlanai", conn);// traukia duomenis is DB
        SqlDataReader eiluteIsDB = cmd.ExecuteReader();
        int i = 0;
        try
        {


            sms_norimas = int.Parse(sms.Text);// is stringo i int
            sms_norimas = sms_norimas > 0 ? sms_norimas : int.MaxValue; // jei ivesta mažiau nei nulis priskirima max INT
            min_norimas = int.Parse(min.Text);
            min_norimas = min_norimas > 0 ? min_norimas : int.MaxValue;
            gb_norimas = int.Parse(gb.Text);
            gb_norimas = gb_norimas > 0 ? gb_norimas : int.MaxValue;

            eur_norimas = int.Parse(eur.Text);

            sms_prioritetas = int.Parse(sms_s.Text);
            min_prioritetas = int.Parse(min_s.Text);
            gb_prioritetas = int.Parse(gb_s.Text);
            eur_prioritetas = int.Parse(eur_s.Text);
        }
        catch (Exception ex)
        {
            return;
        }
        double min_d = double.MaxValue;

        DataTable dataTable = new DataTable(); // kurima duomenu lentale atvaizdavima
        dataTable.Columns.Add("SMS", typeof(int));
        dataTable.Columns.Add("MIN", typeof(int));
        dataTable.Columns.Add("GB", typeof(int));
        dataTable.Columns.Add("EUR", typeof(double));
        dataTable.Columns.Add("Pavadinimas", typeof(string));
        dataTable.Columns.Add("Operatorius", typeof(string));
        dataTable.Columns.Add("Reitingas", typeof(double));
        DataRow dr;

        string pavadinimas = ""; // raudona Bite
        while (eiluteIsDB.Read()) // skatame is DB
        {
            dr = dataTable.NewRow(); //sukuriame viena duomenu eilute (tuscia)
            //kviečiam skaičiuoti
            double reitingas = formula(int.Parse(eiluteIsDB["sms"].ToString()), int.Parse(eiluteIsDB["min"].ToString()), int.Parse(eiluteIsDB["gb"].ToString()), int.Parse(eiluteIsDB["eur"].ToString())); // suskaiciojame reitinga
            if(reitingas<min_d) // ar cia geresnis, nei buvo pries tai surastas 
            {
                min_d = reitingas;
                pavadinimas =  eiluteIsDB["pavadinimas"] + " " + eiluteIsDB["operatorius"];
            }
            //užpildome lenteles viena eilute
            dr[0] = eiluteIsDB["sms"];
            dr[1] = eiluteIsDB["min"];
            dr[2] = eiluteIsDB["gb"];
            dr[3] = eiluteIsDB["eur"];
            dr[4] = eiluteIsDB["pavadinimas"];
            dr[5] = eiluteIsDB["operatorius"];
            dr[6] = reitingas;

            dataTable.Rows.Add(dr); // duomenu eilute idejome i duomenu lentele

        }
        dataTable.DefaultView.Sort = "Reitingas";// pasakome pagal koki stulpeli reikia suruoti ."Reitingas desc"
        dataTable = dataTable.DefaultView.ToTable();//surušiuoja

        GridView1.DataSource = dataTable; // priskiriam web elematui lenteles duomenis
        GridView1.DataBind();

        Planas.Text = pavadinimas;// tekstas koks panas geriausias
        conn.Close();
    }
    //cia viskas aišku.
    private double formula(int sms, int min, int gb, int eur)
    {
        sms = sms > 0 ? sms : int.MaxValue; // if (sms > 0 ) {sms = sms;} else {sms=int.maxvalue}
        min = min > 0 ? min : int.MaxValue;
        gb = gb > 0 ? gb : int.MaxValue;

        double s = Math.Pow(sms - sms_norimas, 2) * sms_prioritetas;
        double m = Math.Pow(min - min_norimas, 2) * min_prioritetas;
        double g = Math.Pow(gb - gb_norimas, 2) * gb_prioritetas;
        double e = Math.Pow(eur - eur_norimas, 2) * eur_prioritetas;
        
        return Math.Pow(s + m + g + e, (1 / 2.0));
    }
}