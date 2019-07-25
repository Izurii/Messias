using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CalculadoradoMessias
{

    public partial class Form1 : Form
    {

        public double Round(double x)
        {
            double y = Math.Round((x * 2), MidpointRounding.AwayFromZero);
            x = y / 2;
            return x;
        }

        public Form1()
        {

            InitializeComponent(); this.Icon = new Icon("if_Jesus_362064.ico");

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                //GERALZ
                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Por favor digite algum número.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {

                    Double[] numbers = textBox3.Text.Split('-', ',', ' ').Select(s => Double.Parse(s)).ToArray(); int inferno = 1;//Separador do rol
                    textBox3.Text = String.Join(",", numbers.OrderBy(c => c)); //Bota a array ordenada no textbox.
                    double n = numbers.Length; int n1 = Convert.ToInt32(n); int tabela = 1;//Total de números no rol
                    double min = numbers.Min(); /* Menor número do rol */
                    double max = numbers.Max(); //Maior número do rolba
                    double mm = max - min; //É a amplitude total 
                    double nc = Math.Floor(Math.Sqrt(n)); /* Número de classes */
                    double ampcla = Round((mm / nc)); //Amplitude das classes

                    // Média
                    //Se quiser a conta manual sem usar a classe seria isso aqui: double b = Math.Round(numbers.Sum() / n, 2); porém eu quis usar o MathNet porque sim.
                    double xa = MathNet.Numerics.Statistics.ArrayStatistics.Mean(numbers); //Calcula a média sem arredondar para fins seila....
                    double ba = Math.Round(xa, 2); //Média arredondada
                    label1.Text = "Média: " + Convert.ToString(ba); //Exibe a média

                    // Mediana
                    double med = MathNet.Numerics.Statistics.ArrayStatistics.MedianInplace(numbers); //Calcula a Mediana
                    label2.Text = "Mediana: " + Convert.ToString(med); //Exibe a mediana

                    // Moda
                    var md = numbers.GroupBy(x => x).Select(m => new { Value = m.Key, Count = m.Count() }).ToList(); //Acha
                    double mc = md.Max(m => m.Count); //a
                    double[] mode = md.Where(m => m.Count == mc).Select(m => m.Value).ToArray(); //tal da
                    if (mode.Length == n) { label3.Text = "Moda: N/A"; } else { label3.Text = "Moda: " + String.Join(", ", mode); } //Moda

                    //Amplitude Total
                    label26.Text = "Amplitude Total: " + Convert.ToString(mm); //Exibe a amplitude total, anteriormente calculada

                    //Total de dados
                    label27.Text = "Total de Dados: " + Convert.ToString(n); //Exibe o total de coisas no rol, previamente calculado

                    //Desvio padrão
                    /* Começa aqui */
                    double[] t = new double[n1]; //Array
                    for (int i = 0; i < n; i++) //Calcula o desvio
                    {

                        double var = (Math.Pow((numbers[i] - ba), 2.00)) / (n - 1); t[i] = var;

                    }


                    /* Acaba aqui os calculos do dp */
                    //O resto abaixo é só arredondamento, então não vou explicar.
                    //Isso é a variância xD, é colocar uma raiz quadrada e vira desvio padrão, então deixei aqui
                    double vr = Math.Round(t.Sum(), 2); //Arredonda e soma os valores da array criada etc...
                    label25.Text = "Desvio padrão: " + Convert.ToString(Math.Round(Math.Sqrt(vr), 2)); //Exibe o desvio padrão que acabou de ser calculado

                    //Variância
                    label28.Text = "Variância: " + Convert.ToString(vr); //Exibe a variância, previamente...

                    //Coeficiente
                    label4.Text = "Coeficiente: " + Math.Round((Math.Round(Math.Sqrt(vr), 2) / xa) * 100, 2) + "%";

                    //Tabela DF


                    data.ReadOnly = true; data.Columns.Clear(); data.Rows.Clear(); //Deixa a table read-only e limpa ela.
                    data.Columns.Add("Cl", "Classes"); data.Columns.Add("Fr", "Fi"); data.Columns.Add("Xi", "Xi"); data.Columns.Add("fa", "Fac"); data.Columns.Add("frr", "Fr %"); //Adiciona colunas na table

                    //Classes
                    Double[] arra1 = new double[(int)nc + 1]; //Array
                    Double[] arra2 = new double[(int)nc + 1]; //Array

                    for (int i = 0; i <= nc; i++) //Faz a classes xD
                    {

                        double x = Math.Round((min + (ampcla * (i))), 1); double y = Math.Round((min + (ampcla * (i + 1))), 1); //Calcula a divisão de classes, bota numa array
                        data.Rows.Add(x + " |- " + y); //Gera as rows na tabela referente as classes
                        arra1[i] = x; arra2[i] = y; //Bota os calculos nas arrays para serem usados somente depois pela freque...

                    }

                    //Freque
                    for (int i = 0; i <= nc; i++) //Calcula e coloca as freque em rows  //loop
                    {

                        data.Rows[i].Cells[1].Value = numbers.Count(p => (p >= arra1[i]) && (p < (arra2[i])));

                    }
                    data.Rows[data.Rows.Count - 2].Cells[1].Value = numbers.Count(p => (p >= arra2[arra2.Length - 2]) && (p <= max)); //Correção do último valor, que pode vir a dar problema.

                    if (data.Rows[data.Rows.Count - 2].Cells[1].Value.ToString() == "0") //Remove a última classe se ela tiver 0 de frequência
                    {

                        data.Rows.Remove(data.Rows[data.Rows.Count - 2]);

                    }

                    //Moda
                    String lmif = (from DataGridViewRow row in data.Rows where row.Cells[1].FormattedValue.ToString() != string.Empty select Convert.ToInt32(row.Cells[1].FormattedValue)).Max().ToString(); //Pega a maior frequência
                    int rind = 0;
                    foreach (DataGridViewRow row in data.Rows) //Acha a index da row onde está a maior frequência
                    {
                        if (row.Cells[1].Value.ToString().Equals(lmif))
                        {
                            rind = row.Index;
                            break;
                        }
                    }
                    String lmi = arra1[Convert.ToInt32(rind)].ToString(); //Defini o limite inferior da classe modal

                    decimal d1 = 0;
                    if (data.Rows[rind].Cells[1].Value == data.Rows[0].Cells[1].Value)
                    {

                        d1 = Convert.ToDecimal(data.Rows[rind].Cells[1].Value);

                    }
                    else
                    {

                        d1 = Convert.ToDecimal(data.Rows[rind].Cells[1].Value) - Convert.ToDecimal(data.Rows[rind - 1].Cells[1].Value);

                    }

                    decimal d2 = 0;
                    if (data.Rows[rind].Cells[1].Value == data.Rows[data.Rows.Count - 3].Cells[1].Value)
                    {

                        d2 = Convert.ToDecimal(data.Rows[rind].Cells[1].Value);

                    }
                    else
                    {

                        d2 = Convert.ToDecimal(data.Rows[rind].Cells[1].Value) - Convert.ToDecimal(data.Rows[rind + 1].Cells[1].Value);

                    }

                    decimal continha = Convert.ToDecimal(lmi) + ((d1 / (d1 + d2)) * Convert.ToDecimal(ampcla)); //Fórmula da moda sendo aplicada
                    label3.Text = label3.Text + " (Moda via Fórmula: " + Math.Round(continha, 3) + ")"; //Exibe o valor da moda

                    //Fac
                    data.Rows[0].Cells[3].Value = data.Rows[0].Cells[1].Value; //Primeiro valor idêntico ao da freque
                    for (int i = 1; i <= nc; i++) //Calcula o fac e bota ele nas rows //loop
                    {

                        data.Rows[i].Cells[3].Value = Convert.ToInt32(data.Rows[i].Cells[1].Value) + Convert.ToInt32(data.Rows[i - 1].Cells[3].Value);

                    }

                    //Fr %
                    for (int i = 0; i <= nc; i++) //Calcula a fre em % e inserta na rows //loop
                    {

                        data.Rows[i].Cells[4].Value = Math.Round(((numbers.Count(p => (p >= arra1[i]) && (p < (arra2[i])))) * 100 / n), 2, MidpointRounding.AwayFromZero);

                    }
                    //data.Rows[data.Rows.Count-2].Cells[4].Value = Round(((numbers.Count(p => (p >= arra2[arra2.Length-2]) && (p <= max))) * 100 / n)); //Correção do último valor. Após alguns testes aparentemente não há mais necessidade.      

                    //Xi
                    for (int i = 0; i <= nc; i++)
                    {

                        data.Rows[i].Cells[2].Value = (arra2[i] + arra1[i]) / 2; /* Calcula o Xi e já o coloca na tabela*/

                    }


                    //Totais de tudo
                    data.Rows[data.Rows.Count - 1].Cells[0].Value = "Σ";

                    //Fre em %
                    decimal sumfr = 0; //Decimal
                    for (int i = 0; i < data.Rows.Count; i++) //loop
                    {

                        sumfr += Convert.ToDecimal(data.Rows[i].Cells[4].Value); /* Soma os valores das fre % */
                        data.Rows[i].Cells[4].Value = data.Rows[i].Cells[4].Value + "%"; /* Coloca os % nas freq em % */

                    }
                    data.Rows.Add(); data.Rows[data.Rows.Count - 1].Cells[4].Value = Round((double)sumfr) + "%"; //Exibe o valor total da Fre em %

                    //Xi total
                    decimal sumx = 0; //Decimal
                    for (int i = 0; i < data.Rows.Count - 1; i++) //loop
                    {

                        sumx += Convert.ToDecimal(data.Rows[i].Cells[2].Value); /* Soma os valores do Xi */

                    }
                    data.Rows[data.Rows.Count - 1].Cells[2].Value = sumx.ToString(); //Exibe o valor total do Xi

                    //Fac total
                    data.Rows[data.Rows.Count - 1].Cells[3].Value = data.Rows[data.Rows.Count - 3].Cells[3].Value;

                    //Freq total
                    decimal sumf = 0; //Decimal
                    for (int i = 0; i < data.Rows.Count; i++) //loop
                    {

                        sumf += Convert.ToDecimal(data.Rows[i].Cells[1].Value); /* Soma os valores das fre */

                    }
                    data.Rows[data.Rows.Count - 1].Cells[1].Value = sumf.ToString(); //Exibe o valor total da Fre

                    //Fim da tabela Dos infernos

                    //Assimetria
                    double assi = (xa - (double)continha) / Math.Round(Math.Sqrt(vr), 2); //Calcula a assimetria
                    if (assi != 0)
                    {

                        label5.Text = "Assimetria: " + Math.Round(assi, 3).ToString(); //Exibe se a assimetria existir

                    }
                    else
                    {

                        label5.Text = "Assimetria: N/A"; //Coloca um N/A já que não tem moda

                    }

                }

            }
            catch (Exception)
            {

                MessageBox.Show("Verifique se você não digitou algo errado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); /* Mostra o erro */

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            textBox3.Clear(); data.Columns.Clear(); /* LIMPA TUDO */
            label1.Text = "Média"; label26.Text = "Amplitude Total"; label4.Text = "Coeficiente";
            label2.Text = "Mediana"; label25.Text = "Desvio Padrão"; label3.Text = "Moda";
            label27.Text = "Total de dados"; label28.Text = "Variância"; label5.Text = "Assimetria";
            MessageBox.Show("Dados limpos com sucesso.", "Limpa tudo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            About f1 = new About();
            f1.Show(); //Abre novo form.
        }

        private void button4_Click(object sender, EventArgs e)
        {
            report rr = new report();
            rr.Show(); //Abre novo form.
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}