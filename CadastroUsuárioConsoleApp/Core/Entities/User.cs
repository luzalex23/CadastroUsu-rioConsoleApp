using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroUsuarioConsoleApp.Core.Entities;

public class User
{
    public string Name { get; }
    public string Email { get; }
    public int Age { get; }

    public User(string name, string email, int age)
    {
        Name = name;
        Email = email;
        Age = age;
    }

    public override string ToString()
    {
        return $"Nome: {Name}, E-mail: {Email}, Idade: {Age}";
    }
}
