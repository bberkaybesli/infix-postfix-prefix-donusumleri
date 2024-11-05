using System;
using System.Collections.Generic;

class Program
{
    // Operatör önceliğini belirleyen fonksiyon
    static int Oncelik(char op)
    {
        if (op == '+' || op == '-') return 1; // Toplama ve çıkarma önceliği
        if (op == '*' || op == '/') return 2; // Çarpma ve bölme önceliği
        if (op == '^') return 3; // Üs alma önceliği
        return 0;
    }

    // Infix ifadeyi postfix'e dönüştüren fonksiyon
    static string InfixToPostfix(string infix)
    {
        Stack<char> yigin = new Stack<char>(); // Operatörler için yığın
        string sonuc = ""; // Postfix sonucu

        foreach (char c in infix)
        {
            if (char.IsLetterOrDigit(c))
                sonuc += c; // Harf veya rakam ise sonuca ekle
            else if (c == '(')
                yigin.Push(c); // Açık parantez
            else if (c == ')') // Kapalı parantez
            {
                while (yigin.Count > 0 && yigin.Peek() != '(')
                    sonuc += yigin.Pop(); // Yığın boşalana kadar ekle
                if (yigin.Count > 0) yigin.Pop(); // Açık parantezi çıkar
            }
            else // Operatör
            {
                while (yigin.Count > 0 && Oncelik(yigin.Peek()) >= Oncelik(c))
                    sonuc += yigin.Pop(); // Yığın boşalana kadar ekle
                yigin.Push(c); // Operatörü yığına ekle
            }
        }

        // Kalan operatörleri ekle
        while (yigin.Count > 0)
            sonuc += yigin.Pop();

        return sonuc;
    }

    // Infix ifadeyi prefix'e dönüştüren fonksiyon
    static string InfixToPrefix(string infix)
    {
        char[] tersInfix = infix.ToCharArray(); // Karakter dizisi
        Array.Reverse(tersInfix); // Ters çevir
        string tersStr = new string(tersInfix);
        string postfix = InfixToPostfix(tersStr); // Ters ifadeyi postfix'e çevir
        char[] tersPostfix = postfix.ToCharArray();
        Array.Reverse(tersPostfix); // Postfix sonucu ters çevir
        return new string(tersPostfix);
    }

    // Main fonksiyonu
    static void Main(string[] args)
    {
        string infix = "a+b*(c^d-e)^(f+g*h)-i"; // Örnek infix ifade
        string postfix = InfixToPostfix(infix); // Postfix'e çevir
        string prefix = InfixToPrefix(infix); // Prefix'e çevir

        // Sonuçları yaz
        Console.WriteLine("Infix: " + infix);
        Console.WriteLine("Postfix: " + postfix);
        Console.WriteLine("Prefix: " + prefix);
    }
}
