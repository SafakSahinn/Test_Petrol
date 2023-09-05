﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Test_Petrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-S866UD2;Initial Catalog=TestBenzin;Integrated Security=True");

        void listele()
        {
            //Kurşunsuz95
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLBENZIN WHERE PETROLTUR='Kurşunsuz95'", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblKursunsuz95.Text = dr[3].ToString();
                progressBar1.Value = int.Parse(dr[4].ToString());
                LblKursunsuz95Litre.Text = dr[4].ToString();
            }
            baglanti.Close();

            //Kurşunsuz97
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("SELECT * FROM TBLBENZIN WHERE PETROLTUR='Kurşunsuz97'", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblKursunsuz97.Text = dr2[3].ToString();
                progressBar2.Value = int.Parse(dr2[4].ToString());
                LblKursunsuz97Litre.Text = dr2[4].ToString();
            }
            baglanti.Close();

            //Euro Dizel 10
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("SELECT * FROM TBLBENZIN WHERE PETROLTUR='EuroDizel10'", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblEuroDizel10.Text = dr3[3].ToString();
                progressBar3.Value = int.Parse(dr3[4].ToString());
                LblEuroDizel10Litre.Text = dr3[4].ToString();
            }
            baglanti.Close();

            //Yeni Pro Dizel
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("SELECT * FROM TBLBENZIN WHERE PETROLTUR='YeniProDizel'", baglanti);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                LblYeniProDizel.Text = dr4[3].ToString();
                progressBar4.Value = int.Parse(dr4[4].ToString());
                LblYeniProDizelLitre.Text = dr4[4].ToString();
            }
            baglanti.Close();

            //Gaz
            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("SELECT * FROM TBLBENZIN WHERE PETROLTUR='Gaz'", baglanti);
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                LblGaz.Text = dr5[3].ToString();
                progressBar5.Value = int.Parse(dr5[4].ToString());
                LblGazLitre.Text = dr5[4].ToString();
            }
            baglanti.Close();

            //
            baglanti.Open();
            SqlCommand komut6 = new SqlCommand("SELECT * FROM TBLKASA",baglanti);
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                LblKasa.Text = dr6[0].ToString();
            }
            baglanti.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz95, litre, tutar;
            kursunsuz95 = Convert.ToDouble(LblKursunsuz95.Text);
            litre = Convert.ToDouble(numericUpDown1.Value);
            tutar = kursunsuz95 * litre;
            TxtKursunsuz95Fiyat.Text = tutar.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            double kursunsuz97, litre, tutar;
            kursunsuz97 = Convert.ToDouble(LblKursunsuz97.Text);
            litre = Convert.ToDouble(numericUpDown2.Value);
            tutar = kursunsuz97 * litre;
            TxtKursunsuz97Fiyat.Text = tutar.ToString();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            double euroDizel, litre, tutar;
            euroDizel = Convert.ToDouble(LblEuroDizel10.Text);
            litre = Convert.ToDouble(numericUpDown3.Value);
            tutar = euroDizel * litre;
            TxtEuroDizel10Fiyat.Text = tutar.ToString();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            double proDizel, litre, tutar;
            proDizel = Convert.ToDouble(LblYeniProDizel.Text);
            litre = Convert.ToDouble(numericUpDown4.Value);
            tutar = proDizel * litre;
            TxtYeniProDizelFiyat.Text = tutar.ToString();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            double gaz, litre, tutar;
            gaz = Convert.ToDouble(LblGaz.Text);
            litre = Convert.ToDouble(numericUpDown5.Value);
            tutar = gaz * litre;
            TxtGazFiyat.Text = tutar.ToString();
        }

        private void BtnDepoDoldur_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO TBLHAREKET (PLAKA,BENZINTURU,LITRE,FIYAT) VALUES (@P1,@P2,@P3,@P4)", baglanti);
                komut.Parameters.AddWithValue("@P1", TxtPlaka.Text);
                komut.Parameters.AddWithValue("@P2", "Kurşunsuz95");
                komut.Parameters.AddWithValue("@P3", numericUpDown1.Value);
                komut.Parameters.AddWithValue("@P4", decimal.Parse(TxtKursunsuz95Fiyat.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("UPDATE TBLKASA SET MIKTAR=MIKTAR+@P1", baglanti);
                komut2.Parameters.AddWithValue("@P1", decimal.Parse(TxtKursunsuz95Fiyat.Text));
                komut2.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("UPDATE TBLBENZIN SET STOK=STOK-@P1 WHERE PETROLTUR='Kurşunsuz95'",baglanti);
                komut3.Parameters.AddWithValue("@P1", numericUpDown1.Value);
                komut3.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Yakıt SAtın alımı Gerçekleşti!");
                listele();


            }
        }
    }
}
