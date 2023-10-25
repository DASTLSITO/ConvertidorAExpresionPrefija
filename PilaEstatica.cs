using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P322310540TM
{
    public class PilaEstatica<T>
    {
        public T[] elementos;
        public int tope;
        public int max;

        public PilaEstatica(int capacidad)
        {
            max = capacidad;
            elementos = new T[capacidad];
            tope = -1;
        }

        public void Push(T elemento)
        {
            if (tope == elementos.Length - 1)
            {
                Console.WriteLine("La pila está llena, no se pueden agregar más elementos.");
                return;
            }

            tope++;
            elementos[tope] = elemento;
        }

        public T Pop()
        {
            if (tope == -1)
            {
                Console.WriteLine("La pila está vacía, no se pueden extraer más elementos.");
                return default;
            }

            T elemento = elementos[tope];
            tope--;
            return elemento;
        }

        public T Peek()
        {
            if (tope == -1)
            {
                Console.WriteLine("La pila está vacía, no hay elementos para consultar.");
                return default;
            }

            return elementos[tope];
        }

        public bool EstaVacia()
        {
            return tope == -1;
        }
    }
}
