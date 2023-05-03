using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVOPz.Module;

namespace DVOPz
{
    public class App
    {
        private readonly InputValidation _inputValidation;
        private readonly Commands _commands;
        private User _currentUser;

        public App(InputValidation inputValidation, Commands commands)
        {
            _inputValidation = inputValidation;
            _commands = commands;
        }

        public void Application()
        {
            string comm;
            string comm2;
            string promt;
            var commandDictionary = new Dictionary<string, Action<string>>
            {
                { "AUTH", email => _currentUser = _commands.AUTH(email) },
                { "WHOAMI",_commands.WHOAMI },
                { "STATUS", _commands.STATUS },
                { "ADD", input => _commands.ADD(input) },
                { "PROCESS", _commands.PROCESS },
                { "LOGOUT", _commands.LOGOUT }
            };

            while (true)
            {
                if (_currentUser != null)
                {
                    Console.Write(_currentUser.Token + " ");
                }
                var input = Console.ReadLine();
                comm = input.Split(' ')[0];
                if (comm == "AUTH" || comm == "ADD")
                {
                    if (input.Split(' ').Length < 2 || string.IsNullOrWhiteSpace(input.Split(' ')[1]))
                    {
                        _inputValidation.Unauthenticated();
                        continue;
                    }
                    promt = input.Split(' ')[1];
                }
                else if (comm == "ADD")
                {
                    if (input.Split(' ').Length < 3 || string.IsNullOrWhiteSpace(input.Split(' ')[2]))
                    {
                        _inputValidation.SynError();
                        continue;
                    }
                    comm2= input.Split(' ')[1];
                    promt = input.Split(' ')[2];
                }
                else
                {
                    promt = null;
                }
                if (commandDictionary.TryGetValue(comm, out var command))
                {
                    command(promt);
                }
                else
                {
                    _inputValidation.SynError();
                }


            }
        }
    }
}
