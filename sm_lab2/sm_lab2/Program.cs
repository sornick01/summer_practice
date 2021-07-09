using System;
using logger;

namespace sm_lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger log = new Logger("logsfile.txt");
            log.Log(Severity.Trace, "Транзакция проведена");
            log.Log(Severity.Debug, "Запрос в базу занял 0.02 секунды, извлечено 1000 записей");
            log.Log(Severity.Information, " Проведена транзакция по счёту 11111 (Vasiliy Pupkin), получено $2000");
            log.Log(Severity.Error, "Ошибка при сохранении транзакции 123");
            log.Log(Severity.Warning, "Отклонена транзакция с суммой платежа 0");
            log.Log(Severity.Critical, "Ошибка конфигурации модуля. Транзакции не будут обрабатываться");
            Console.WriteLine("Успешно создан файл логгера logsfile.txt");
        }
    }
}