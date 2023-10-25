using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


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
                }
                else if (PilaExpresionInfija.Peek().Dato.Equals("("))
                {
                    Nodo<string> temp = PilaSimbolos.tope;
                    while (temp.Dato.Equals(")") && temp != null)
                    {
                        PilaExpresionPrefija.Push(temp.Dato);
                        temp = temp.Siguiente;
                    }

                    PilaSimbolos.Pop();
                    PilaExpresionInfija.Pop();
                }
                else
                {
                    MessageBox.Show("Solo se permiten letras y signos", "Error");
                    return;
                }


                simbolosTemporal = "";
                expresionPrefijaTemporal = "";

                Nodo<string> temp2 = PilaSimbolos.tope;

                while (temp2 != null)
                {
                    simbolosTemporal += temp2.Dato;
                    temp2 = temp2.Siguiente;
                }

                temp2 = PilaExpresionPrefija.tope;

                while (temp2 != null)
                {
                    expresionPrefijaTemporal += temp2.Dato;
                    temp2 = temp2.Siguiente;
                }
                dataGridView1.Rows.Add(simbolosTemporal);
                dataGridView2.Rows.Add(expresionPrefijaTemporal);
            } while (!PilaExpresionInfija.EstaVacia());

            Nodo<string> temp3 = PilaSimbolos.tope;

            while (temp3 != null)
            {
                PilaExpresionPrefija.Push(temp3.Dato);
                temp3 = temp3.Siguiente;
            }

            temp3 = PilaExpresionPrefija.tope;

            expresionPrefija = "";

            while (temp3 != null)
            {
                expresionPrefija += temp3.Dato;
                temp3 = temp3.Siguiente;
            }
            label2.Text += expresionPrefija;
        }

        public double CalcularExpresionPrefija()
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

            return pila.Pop().Dato;
        }
        public bool esOperador(char c)
        {
            return (c == '+' || c == '-' || c == '*' || c == '/' || c == '^');
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AsignarValores();
            if(valoresParaLasVariables.Count == variables.Count)
                label3.Text = $"Expresión evaluada: {CalcularExpresionPrefija()}";
        }

        public void AsignarValores()
        {
            valoresParaLasVariables.Clear();
            dataGridView3.Rows.Clear();
            foreach (var letra in variables)
            {
                var valor = Microsoft.VisualBasic.Interaction
                    .InputBox("Valor para la variable " + letra.ToString(), "Peticion de valor", "");
                
                if(valor.Length > 0)
                    if(Char.IsNumber(Convert.ToChar(valor)))
                        valoresParaLasVariables.Add(Convert.ToDouble(valor));
            }
        }
    }
}