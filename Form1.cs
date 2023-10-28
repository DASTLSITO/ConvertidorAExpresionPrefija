using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic;


namespace P322310540TM
{
    public partial class Form1 : Form
    {
        string sb = "";
        PilaDinamica<string> PilaExpresionInfija;
        PilaDinamica<string> PilaExpresionPrefija;
        PilaDinamica<string> PilaSimbolos;
        PilaDinamica<char> pila;
        List<char> variables = new List<char>();
        List<double> valoresParaLasVariables = new List<double>();
        string expresionPrefijaTemporal = "";
        string simbolosTemporal = "";
        string resultadoTemporal = "";
        string expresionPrefija = "";
        int cantidadVariables = 0;
        int cantidadOperadores = 0;

        Dictionary<string, int> prioridad = new Dictionary<string, int>()
        {
            { "^", 3 },
            { "*", 2 },
            { "/", 2 },
            { "+", 1 },
            { "-", 1 }
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                cantidadOperadores = 0;
                cantidadVariables = 0;
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                dataGridView3.Rows.Clear();
                variables.Clear();
                valoresParaLasVariables.Clear();
                obtenerPilaExpresionInfija();
                PilaExpresionPrefija = new PilaDinamica<string>();
                PilaSimbolos = new PilaDinamica<string>();
                pila = new PilaDinamica<char>();
                label2.Text = "Expresión prefija: ";
                label3.Text = "Expresión evaluada: ";
                ConvertirDeInfijaAPrefija();
                button2.Enabled = true;
            }
        }

        private void obtenerPilaExpresionInfija()
        {

            sb = Regex.Replace(textBox1.Text, @"\s", "");
            PilaExpresionInfija = new PilaDinamica<string>();
            for (int i = 0; i < sb.Length; i++)
            {
                PilaExpresionInfija.Push(sb[i].ToString());
            }
        }

        public void ConvertirDeInfijaAPrefija()
        {
            try
            {
                do
                {
                    if (PilaExpresionInfija.Peek().Dato.Equals(")"))
                    {
                        PilaSimbolos.Push(PilaExpresionInfija.Pop().Dato);
                    }
                    else if (Regex.IsMatch(PilaExpresionInfija.Peek().Dato.ToString(), "^[a-zA-Z]*$"))
                    {
                        if (!variables.Contains(Convert.ToChar(PilaExpresionInfija.Peek().Dato)))
                            variables.Add(Convert.ToChar(PilaExpresionInfija.Peek().Dato));

                        PilaExpresionPrefija.Push(PilaExpresionInfija.Pop().Dato);
                        cantidadVariables++;
                    }
                    else if (PilaExpresionInfija.Peek().Dato.Equals("+")
                            || PilaExpresionInfija.Peek().Dato.Equals("-")
                            || PilaExpresionInfija.Peek().Dato.Equals("*")
                            || PilaExpresionInfija.Peek().Dato.Equals("/")
                            || PilaExpresionInfija.Peek().Dato.Equals("^"))
                    {
                        if (PilaSimbolos.EstaVacia() || PilaSimbolos.Peek().Dato.Equals(")") || prioridad[PilaExpresionInfija.Peek().Dato] >= prioridad[PilaSimbolos.Peek().Dato])
                            PilaSimbolos.Push(PilaExpresionInfija.Pop().Dato);
                        else if (prioridad[PilaExpresionInfija.Peek().Dato] < prioridad[PilaSimbolos.Peek().Dato])
                        {
                            PilaExpresionPrefija.Push(PilaSimbolos.Pop().Dato);
                            PilaSimbolos.Push(PilaExpresionInfija.Pop().Dato);
                        }

                        cantidadOperadores++;
                    }
                    else if (PilaExpresionInfija.Peek().Dato.Equals("("))
                    {
                        while (!PilaSimbolos.tope.Dato.Equals(")") && PilaSimbolos.tope != null)
                        {
                            PilaExpresionPrefija.Push(PilaSimbolos.Pop().Dato);
                        }

                        var rest = PilaSimbolos.Pop();
                        PilaExpresionInfija.Pop();
                    }
                    else
                    {
                        throw new Exception();
                    }

                    MostrarProcesoDeConversion();
                } while (!PilaExpresionInfija.EstaVacia());

                if (cantidadVariables - 1 != cantidadOperadores)
                    throw new Exception();

                MostrarResultadosDeConversion();
            }
            catch 
            {
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                dataGridView3.Rows.Clear();
                variables.Clear();
                valoresParaLasVariables.Clear();
                MessageBox.Show("Ingrese una expresión infija valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void MostrarProcesoDeConversion()
        {
            simbolosTemporal = "";
            expresionPrefijaTemporal = "";

            Nodo<string> temp = PilaSimbolos.tope;

            while (temp != null)
            {
                simbolosTemporal += temp.Dato;
                temp = temp.Siguiente;
            }

            temp = PilaExpresionPrefija.tope;

            while (temp != null)
            {
                expresionPrefijaTemporal += temp.Dato;
                temp = temp.Siguiente;
            }
            dataGridView1.Rows.Add(simbolosTemporal);
            dataGridView2.Rows.Add(expresionPrefijaTemporal);
        }

        public void MostrarResultadosDeConversion()
        {
            Nodo<string> temp = PilaSimbolos.tope;

            while (temp != null)
            {
                PilaExpresionPrefija.Push(temp.Dato);
                temp = temp.Siguiente;
            }

            temp = PilaExpresionPrefija.tope;

            expresionPrefija = "";

            while (temp != null)
            {
                expresionPrefija += temp.Dato;
                temp = temp.Siguiente;
            }
            label2.Text += expresionPrefija;

            dataGridView2.Rows.Add(expresionPrefija);
        }

        public double CalcularExpresionPrefija()
        {
            try
            {
                PilaDinamica<double> pila = new PilaDinamica<double>();

                for (int i = expresionPrefija.Length - 1; i >= 0; i--)
                {
                    char caracter = expresionPrefija[i];

                    if (Char.IsLetter(caracter))
                    {
                        double valor = valoresParaLasVariables[variables.IndexOf(caracter)];
                        pila.Push(valor);
                    }
                    else if (Char.IsDigit(caracter))
                    {
                        pila.Push(Convert.ToInt32(caracter.ToString()));
                    }
                    else if (esOperador(caracter))
                    {
                        double operando1 = pila.Pop().Dato;
                        double operando2 = pila.Pop().Dato;

                        switch (caracter)
                        {
                            case '+':
                                pila.Push(operando1 + operando2);
                                break;
                            case '-':
                                pila.Push(operando1 - operando2);
                                break;
                            case '*':
                                pila.Push(operando1 * operando2);
                                break;
                            case '/':
                                pila.Push(operando1 / operando2);
                                break;
                            case '^':
                                pila.Push(Math.Pow(operando1, operando2));
                                break;
                            default:
                                throw new ArgumentException("Operador no válido: " + caracter);
                        }
                        resultadoTemporal = $"{operando1} {caracter} {operando2}";
                        dataGridView3.Rows.Add(resultadoTemporal);
                    }
                    else
                    {
                        throw new ArgumentException("Caracter no reconocido: " + caracter);
                    }
                }

                dataGridView3.Rows.Add(pila.Peek().Dato);

                return pila.Pop().Dato;
            }
            catch
            {
                MessageBox.Show("Ingrese una expresión infija valida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default;
            }
        }
        public bool esOperador(char c)
        {
            return (c == '+' || c == '-' || c == '*' || c == '/' || c == '^');
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AsignarValores();
            if (valoresParaLasVariables.Count == variables.Count)
                label3.Text = $"Expresión evaluada: {CalcularExpresionPrefija()}";
        }

        public void AsignarValores()
        {
            valoresParaLasVariables.Clear();
            dataGridView3.Rows.Clear();
            foreach (var letra in variables)
            {
                var valor = Interaction.InputBox("Valor para la variable " + letra.ToString(), "Peticion de valor", "");

                if (valor.Length > 0)
                    if (Char.IsNumber(Convert.ToChar(valor)))
                        valoresParaLasVariables.Add(Convert.ToDouble(valor));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}