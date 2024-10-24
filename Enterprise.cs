using Incapsulation.EnterpriseTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.EnterpriseTask {
    public class Enterprise { // класс Предприятие

        private Guid guid; // уникальный идентификатор предприятия
        private string inn; // ИНН предприятия
        private DateTime establishDate; // дата основания предприятия

        public Guid Guid { // публичное свойство
            get { return guid; } // доступ к закрытому полю guid
        }

        public string Name { get; set; } // публичное свойство

        public string Inn { // публичное свойство
            get { return inn; } // возвращает значение закрытого поля inn
            set { // устанавливает значение закрытого поля inn
                if (value.Length != 10 || !value.All(z => char.IsDigit(z))) // проверяем, что ИНН состоит из 10 цифр
                    throw new ArgumentException(); // выбрасываем исключение при нарушении условия
                inn = value;
            }
        }

        public DateTime EstablishDate { get; set; } // публичное свойство

        public TimeSpan ActiveTimeSpan { // публичное свойство 
            get { return DateTime.Now - establishDate; } // время существования предприятия
        }

        public double GetTotalTransactionsAmount() { // публичное свойство 
            DataBase.OpenConnection(); // открываем соединение с базой данных
            var amount = 0.0; // общая сумма проведенных транзакций предприятия
            foreach (Transaction t in DataBase.Transactions().Where(z => z.EnterpriseGuid == guid))  // перебираем все транзакции предприятия
                amount += t.Amount; // суммируем суммы всех проведенных транзакций
            return amount;
        }

        public Enterprise(Guid guid) { // публичный конструктор
            this.guid = guid; // значение закрытого поля равное переданному значению
        }
    }
}
