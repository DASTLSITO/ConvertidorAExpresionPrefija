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
        PilaEstatica<string> PilaExpresionInfija;
        PilaEstatica<string> PilaExpresionPrefija;
        PilaEstatica<string> PilaSimbolos;
        PilaEstatica<char> pila;
        List<char> variables = new List<char>();
        List<double> valoresParaLasVariables = new List<double>();
        string expresionPrefijaTemporal = "";
        string simbolosTemporal = "";
        string resultadoTemporal = "";
        string expresionPrefija = "";
        int indice = 0;

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
                PilaExpresionPrefija = new PilaEstatica<string>(PilaExpresionInfija.max);
                PilaSimbolos = new PilaEstatica<string>(PilaExpresionInfija.max);
                pila = new PilaEstatica<char>(PilaExpresionInfija.max);
                ConvertirDeInfijaAPrefija();
                button2.Enabled = true;
            }
        }

        private void obtenerPilaExpresionInfija()
        {

            sb = Regex.Replace(textBox1.Text, @"\s", "");
            PilaExpresionInfija = new PilaEstatica<string>(sb.Length);
            for (int i = 0; i < sb.Length; i++)
            {
                PilaExpresionInfija.Push(sb[i].ToString());
            }
        }

        public void ConvertirDeInfijaAPrefija()
        {
            do
            {
                if (PilaExpresionInfija.elementos[PilaExpresionInfija.tope].Equals(")"))
                {
                    PilaSimbolos.Push(PilaExpresionInfija.Pop());
                }
                else if (Regex.IsMatch(PilaExpresionInfija.elementos[PilaExpresionInfija.tope].ToString(), "^[a-zA-Z]*$"))
                {
                    if (!variables.Contains(Convert.ToChar(PilaExpresionInfija.elementos[PilaExpresionInfija.tope])))
                        variables.Add(Convert.ToChar(PilaExpresionInfija.elementos[PilaExpresionInfija.tope]));

                    PilaExpresionPrefija.Push(PilaExpresionInfija.Pop());
                }
                else if (PilaExpresionInfija.elementos[PilaExpresionInfija.tope].Equals("+")
                        || PilaExpresionInfija.elementos[PilaExpresionInfija.tope].Equals("-")
                        || PilaExpresionInfija.elementos[PilaExpresionInfija.tope].Equals("*")
                        || PilaExpresionInfija.elementos[PilaExpresionInfija.tope].Equals("/")
                        || PilaExpresionInfija.elementos[PilaExpresionInfija.tope].Equals("^"))
                {
                    if (PilaSimbolos.EstaVacia())
                        PilaSimbolos.Push(PilaExpresionInfija.Pop());
                    else if (PilaSimbolos.elementos[PilaSimbolos.tope].Equals(")"))
                        PilaSimbolos.Push(PilaExpresionInfija.Pop());
                    else if (prioridad[PilaExpresionInfija.Peek()] >= prioridad[PilaSimbolos.elementos[PilaSimbolos.tope]])
                        PilaSimbolos.Push(PilaExpresionInfija.Pop());
                    else if (prioridad[PilaExpresionInfija.Peek()] < prioridad[PilaSimbolos.elementos[PilaSimbolos.tope]])
                    {
                        PilaExpresionPrefija.Push(PilaSimbolos.Pop());
                        PilaSimbolos.Push(PilaExpresionInfija.Pop());
                    }
                }
                else if (PilaExpresionInfija.elementos[PilaExpresionInfija.tope].Equals("("))
                {
                    indice = PilaSimbolos.tope;

                    while (!PilaSimbolos.elementos[indice].Equals(")"))
                    {
                        PilaExpresionPrefija.Push(PilaSimbolos.Pop());
                        indice--;
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
                for (int i = PilaSimbolos.tope; i >= 0; i--)
                {
                    simbolosTemporal += PilaSimbolos.elementos[i];
                }

                for (int i = PilaExpresionPrefija.tope; i >= 0; i--)
                {
                    expresionPrefijaTemporal += PilaExpresionPrefija.elementos[i];
                }
                dataGridView1.Rows.Add(simbolosTemporal);
                dataGridView2.Rows.Add(expresionPrefijaTemporal);
            } while (!PilaExpresionInfija.EstaVacia());

            for (int i = PilaSimbolos.tope; i >= 0; i--)
            {
                PilaExpresionPrefija.Push(PilaSimbolos.Pop());
            }

            label2.Text = "Expresión prefija: ";
            indice = PilaExpresionPrefija.tope;

            while (indice >= 0)
            {
                label2.Text += PilaExpresionPrefija.elementos[indice].ToString();
                indice--;
            }

            expresionPrefija = "";
            for (int i = PilaExpresionPrefija.tope; i >= 0; i--)
            {
                expresionPrefija += PilaExpresionPrefija.elementos[i].ToString();
            }
        }

        public double CalcularExpresionPrefija()
        {
            Stack<double> pila = new Stack<double>();

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
                    double operando1 = pila.Pop();
                    double operando2 = pila.Pop();

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

            return pila.Pop();
        }
        public bool esOperador(char c)
        {
            return (c == '+' || c == '-' || c == '*' || c == '/' || c == '^');
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AsignarValores();
            label3.Text = $"Expresión evaluada: {CalcularExpresionPrefija()}";
        }

        public void AsignarValores()
        {
            foreach (var letra in variables)
            {
                valoresParaLasVariables.Add(Convert.ToDouble(Microsoft.VisualBasic.Interaction
                    .InputBox("Valor para la variable " + letra.ToString(), "Peticion de valor", "")));
            }
        }
    }
}