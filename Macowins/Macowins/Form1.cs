using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Macowins
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Tienda T = new Tienda(); //Instancio la clase Tienda

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach(Vestimenta v in T.ListVest)  //Imprimo en el Listbox cada prenda que hay en la lista
            {
                listBox1.Items.Add(v.Prenda +"  "+ v.Precio);
            }

            textBox1.Text = "0";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(T.CobrarEfec(listBox1.SelectedIndex)); // Llamo a la funcion para cobrar en efectivo pasandole el indice del listbox y el textbox
            // donde guardo las ganancias del dia
            textBox1.Text = T.Ganancia.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(T.CobrarTarj(listBox1.SelectedIndex)); // Llamo a la funcion para cobrar con tarjeta pasandole el indice del listbox y el textbox
            //donde guardo las ganancias del dia
            textBox1.Text = T.Ganancia.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Añado las ganancias del dia en en listbox y limpio las ventas del dia y el ingreso del dia.
            listBox3.Items.Add(textBox1.Text + "  "+ DateAndTime.DateString);
            listBox2.Items.Clear();
            textBox1.Text = "0";
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public class Tienda //Clase tienda que contiene una lista de Vestimentas
    {
        public List<Vestimenta> ListVest;
        public double Ganancia;

        public Tienda()
        {
         ListVest = new List<Vestimenta>();  //Instancio la lista de vestimentas y añado un par a la lista.
         Vestimenta A = new Vestimenta("Saco", 1000);
         Vestimenta B = new Vestimenta("Camisa", 1000);
         Vestimenta C = new Vestimenta("Pantalon", 1000);

         ListVest.Add(A);
         ListVest.Add(B);
         ListVest.Add(C);

         Ganancia = 0;
        }

        public string CobrarEfec (int index) //Funcion para cobrar en efectivo
        {
            int estado;
            try
            {
                estado = int.Parse(Interaction.InputBox("Ingrese el numero 1 si es Nuevo, el 2 si esta en Promoción y 3 si esta en Liquidación: "));
                double Vendido;


                if (estado == 1)
                {
                    Vendido = ListVest[index].Precio;  //Me fijo el precio en la lista
                    Ganancia = Ganancia + Vendido;  //Sumo en la variable ganancia las ventas del dia
                    return (ListVest[index].Prenda + "  " + Vendido + "  Nuevo  " + DateAndTime.Now + "  Efectivo");
                    
                }
                else if (estado == 2)
                {
                    Vendido = (ListVest[index].Precio - int.Parse(Interaction.InputBox("Ingrese el descuento por estar en promoción: ")));
                    Ganancia = Ganancia + Vendido;
                    return (ListVest[index].Prenda + "  " + Vendido + "  Promocion  " + DateAndTime.Now + "  Efectivo");
                }

                else if (estado == 3)
                {
                    Vendido = (ListVest[index].Precio / 2);
                    Ganancia = Ganancia + Vendido;
                    return (ListVest[index].Prenda + "  " + Vendido + "  Liquidación  " + "  " + DateAndTime.Now + "  Efectivo");
                }
                else
                {
                    MessageBox.Show("Ese estado no existe");
                }
            }
            catch (Exception ex) { }

            return "";
        }

        public string CobrarTarj (int index) // Funcion para pagar con tarjeta
        {
            try
            {
                int CantCuot;
                CantCuot = int.Parse(Interaction.InputBox("Ingrese la cantidad de cuotas que desea realizar: "));
                int estado;
                estado = int.Parse(Interaction.InputBox("Ingrese el numero 1 si es Nuevo, el 2 si esta en Promoción y 3 si esta en Liquidación: "));
                double Vendido;


                if (estado == 1)
                {
                    Vendido = (ListVest[index].Precio + (CantCuot * 1.05 + ListVest[index].Precio / 100));  //Me fijo el precio en la lista y le agrego el recargo por usar tarjeta
                    Ganancia = Ganancia + Vendido;
                    return (ListVest[index].Prenda + "  " + Vendido + "  Nuevo  " + DateAndTime.Now + "  Tarjeta");
                }
                else if (estado == 2)
                {
                    double desc = int.Parse(Interaction.InputBox("Ingrese el descuento por estar en promoción: "));
                    Vendido = ((ListVest[index].Precio - desc) + CantCuot * 1.05 + ((ListVest[index].Precio - desc) / 100));
                    Ganancia = Ganancia + Vendido;
                    return(ListVest[index].Prenda + "  " + Vendido + "  Promocion  " + DateAndTime.Now + "  Tarjeta");
                }

                else if (estado == 3)
                {

                    Vendido = ((ListVest[index].Precio / 2) + CantCuot * 1.05 + (ListVest[index].Precio / 2) / 100);
                    Ganancia = Ganancia + Vendido;
                    return (ListVest[index].Prenda + "  " + Vendido + "  Liquidación  " + "  " + DateAndTime.Now + "  Tarjeta");
                }
                else
                {
                    MessageBox.Show("Ese estado no existe");
                }
            }
            catch (Exception ex) { }
            return "";
        }
    }
    public class Vestimenta // Clase vestimenta donde se especifica el tipo de la prenda y su precio
    {
        public string Prenda;
        public double Precio;



        public Vestimenta(string prend, double prec) //Constructor que obliga a mandar el tipo de prenda y el precio para poder instanciar una vestimenta
        {
            Prenda = prend;
            Precio = prec;
        }
    }
}
