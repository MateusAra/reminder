using entity.model;
using utils.exceptions;

internal class Program
{
    protected static List<Reminder> _reminders = new List<Reminder>();

    public static void Main(string[] args)
    {
        try
        {
            char opcao = '0';
            while (opcao != '6')
            {
                Console.Clear();
                Console.WriteLine("----Atendimento----");
                Console.WriteLine("1. Cadastrar Lembrete");
                Console.WriteLine("2. Listar Lembretes ");
                Console.WriteLine("3. Remover Lembrete ");
                Console.WriteLine("4. Sair ");
                Console.WriteLine("\n");
                Console.WriteLine("Digite a opcao desejada: ");
                try
                {
                    opcao = Console.ReadLine()[0];
                    switch (opcao)
                    {
                        case '1':
                            RegisterReminder();
                            Console.WriteLine("teste");
                            break;
                        case '2':
                            ListReminders();
                            break;
                        case '3':
                            RemoveReminder();
                            break;
                        case '4':
                            Console.Clear();
                            Console.WriteLine("Programa encerrado.");
                            return;
                        default:
                            Console.WriteLine("Opção não implementada.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw new ReminderException(ex.Message);
                }
            }
        }
        catch (ReminderException ex)
        {
            Console.WriteLine(ex.Message);
        }


    }

    private static void RemoveReminder()
    {
        Console.Clear();
        Console.WriteLine("===============================");
        Console.WriteLine("==     REMOVER LEMBRETE      ==");
        Console.WriteLine("===============================");
        Console.WriteLine("\n");

        if (_reminders.Count <= 0)
        {
            Console.WriteLine("... Não há lembretes cadastrados! ...");
            Console.ReadKey();
            return;
        }
        foreach (Reminder item in _reminders)
        {
            Console.WriteLine($"{item.Id}. {item.Name} - {item.Date.ToShortDateString()}");
        }

        Console.WriteLine();
        Console.Write("Informe o número do lembrete: ");
        string id = Console.ReadLine();
        Reminder? deletedItem = null;
        foreach (Reminder item in _reminders)
        {
            if (item.Id.Equals(int.Parse(id)))
            {
                deletedItem = item;
            }
        }
        if (deletedItem is null)
            Console.WriteLine(" ... Lembrete não encontrado ...");
        else
        {
            _reminders.Remove(deletedItem);
            Console.WriteLine("... Lembrete removido! ...");
        }

        Console.ReadKey();
    }

    public static void RegisterReminder()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("==  CADASTRO DE LEMBRETES    ==");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");

            Reminder reminder = new Reminder();

            while (string.IsNullOrEmpty(reminder.Name))
            {
                Console.Write("Informe o nome: ");
                reminder.Name = Console.ReadLine();
            }

            bool isDateValid = false;
            while (!isDateValid)
            {
                Console.Write("Infome a Data (DD/MM/AAAA): ");
                string date = Console.ReadLine();
                DateTime dateTime;
                isDateValid = DateTime.TryParse(date, out dateTime);
                if(isDateValid)
                    reminder.Date = dateTime;
                else
                    Console.WriteLine("Data inválida, tente novamente...");
            }
            
            reminder.Id = _reminders.Count() + 1;

            _reminders.Add(reminder);

            Console.WriteLine("... Lembrete cadastrado com sucesso! ...");
            Console.WriteLine("...digite qualquer tecla pra voltar para o menu.");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            throw new ReminderException(ex.Message, ex);
        }
    }

    public static void ListReminders()
    {
        Console.Clear();
        Console.WriteLine("===============================");
        Console.WriteLine("==    LISTA DE LEMBRETES     ==");
        Console.WriteLine("===============================");
        Console.WriteLine("\n");
        if (_reminders.Count <= 0)
        {
            Console.WriteLine("... Não há lembretes cadastrados! ...");
            Console.ReadKey();
            return;
        }

        var dates = _reminders.GroupBy(x=> x.Date);

        foreach (var listRemindersByDate in dates)
        {
            Console.WriteLine(listRemindersByDate.Key.ToShortDateString());
            Console.WriteLine("-----------");
            foreach (var item in listRemindersByDate)
            {
                Console.WriteLine($"{item.Name}");
            }
            Console.WriteLine();
        }
        Console.ReadKey();
    }
}