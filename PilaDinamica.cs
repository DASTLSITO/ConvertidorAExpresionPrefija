using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P322310540TM
{
    public class PilaDinamica<T>
    {
        public Nodo<T> tope;

        public PilaDinamica()
        {
            tope = null;
        }

        public void Push(T valor)
        {
            Nodo<T> nuevoNodo = new Nodo<T> { Dato = valor, Siguiente = tope };
            tope = nuevoNodo;
        }

        public Nodo<T> Pop()
        {
            if (!EstaVacia())
            {
                Nodo<T> temp = tope;
                tope = tope.Siguiente;
                return temp;
            }
            else
            {
                Console.WriteLine("La pila está vacía.");
                return default;
            }
        }

        public bool EstaVacia()
        {
            return tope == null;
        }

        public Nodo<T> Peek()
        {
            if (!EstaVacia())
            {
                return tope;
            }
            else
            {
                Console.WriteLine("La pila está vacía.");
                return default;
            }
        }
    }
}
