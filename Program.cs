using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using ClosedXML.Excel;
using UseriosTeste;
public class Program
{
    static string nomeRemove = "";
    static string EmialRemove = "";
    static string nome = "";
    static string email = "";
    static List<Usuarios> user = new List<Usuarios>();

    public static void Main()
    {
        Console.WriteLine("Welcome to cliente");
        Thread.Sleep(2000);
        Console.WriteLine("Quer Salvar um novo usuario, digite 1");
        Console.WriteLine("Quer remover usuario, digite 2");
        int opcao = int.Parse(Console.ReadLine());

        Console.WriteLine("Digite um nome:");
        nome = Console.ReadLine();
        Console.WriteLine("Digite um email:");
        email = Console.ReadLine();

        if (opcao == 1)
        {
            if (!ValidarEmail(email))
            {
                throw new Exception("Email inválido");
            }

            CriarUsuario();
            buildExcel();
        }
        else if (opcao == 2)
        {
            Console.WriteLine("Digite o nome que quer remover");
            nomeRemove = Console.ReadLine();
            Console.WriteLine("Digite o email que quer remover");
            EmialRemove = Console.ReadLine();

            RemoverUser();
        }
    }


    public static void buildExcel(string filename = "resultado.xlsx")
    {
        Console.WriteLine("Escrevendo no arquivo Excel.");


        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Usuarios");


            worksheet.Cell(1, 1).Value = "Nome";
            worksheet.Cell(1, 2).Value = "Email";


            int row = 2;
            foreach (var item in user)
            {
                worksheet.Cell(row, 1).Value = item.Nome;
                worksheet.Cell(row, 2).Value = item.Email;
                row++;
            }


            workbook.SaveAs(filename);
        }
    }


    public static void CriarUsuario()
    {
        Usuarios userr = new Usuarios(nome, email);
        user.Add(userr);
    }


    static bool ValidarEmail(string email)
    {
        string padrao = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, padrao);
    }

    public static void RemoverUser()
    {
        var usuarioRemover = user.FirstOrDefault(u => u.Nome == nomeRemove && u.Email == EmialRemove);

        if (usuarioRemover != null)
        {
            user.Remove(usuarioRemover);
            Console.WriteLine("Usuário removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Usuário não encontrado.");
        }
    }
}

public class Usuarios
{
    public string Nome { get; set; }
    public string Email { get; set; }

    // Construtor
    public Usuarios(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
}