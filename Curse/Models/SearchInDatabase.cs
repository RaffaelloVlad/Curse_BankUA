using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;

namespace Curse.Models
{
    public static class SearchInDatabase
    {
        public static string TempStadSearch { get; set; }
        public static string TempTempStadSearch;
        public static string TempBankSearch { get; set; }
        public static string TempTempBankSearch;
        public static string TempRating { get; set; }
        public static string TempAddsSelectedBank { get; set; }
        public static string TempAddSelectedStad { get; set; }
        public static string TempNumTel { get; set; }
        public static string TempAdres { get; set; }
        public static string TempNumFil { get; set; }
        public static string TempTimeArbait { get; set; }
        //payment
        //Update
        public static string NameDeposit { get; set; }
        public static string TempUpdateBank { get; set; }
        public static string TerminPayment { get; set; }
        public static string PayProcent { get; set; }
        public static string YearRate { get; set; }
        public static string EndBegin { get; set; }
        public static string DocumetPay { get; set; }
        //Add + Delete
        public static string AddNamePaymant { get; set; }
        public static string TempAddNamePaymant { get; set; }
        public static string TempAddsSelectedBankDeposit { get; set; }
        public static string DeletSelectedPaymant { get; set; }
        // credit
        // Add 
        public static string NameCredit { get; set; }
        public static string TempAddsSelectedBankCredit { get; set; }
        public static string MaxSumCredit { get; set; }
        public static string ProcentCredit { get; set; }
        public static string TempContract { get; set; }
        public static string RealProcentCredit { get; set; }
        public static string ErstPay { get; set; }
        public static string DocumentCredit { get; set; }
        public static string Description_ { get; set; }
        public static string Commision { get; set; }
        //
        public static string TempNameCredit;
        public static string TempTempContract;



        public static List<string> GetBankName
        {
            get
            {
                return ConnectorDB.QueryForList("Select NameB from Bank;");
            }
        }
        public static List<string> GetStadName
        {
            get
            {
                return ConnectorDB.QueryForList("Select NameStad from Stad;");
            }
        }

        public static void ForGetStad()
        {
            TempTempStadSearch = TempStadSearch;
            
        }
        public static void ForGetBank()
        {
            TempTempBankSearch = TempBankSearch;
        }
        public static DataTable GetStadPlusBank
        {
            get
            {
                if (TempStadSearch != null || TempBankSearch != null)
                {
                    ForGetStad();
                    ForGetBank();
                }
                return ConnectorDB.QueryForDataTable($"Select NameStad,Adres,NumFil from Bank join filial using (id_Bank) join Stad on filial.id_Stad=Stad.id_Stad where NameStad='{TempTempStadSearch}' and NameB = '{TempTempBankSearch}';");
                //return ConnectorDB.QueryForList($"select filial.id_Stad,Adres,NumFil from Bank join filial using (id_Bank) join Stad on filial.id_Stad=Stad.id_Stad where NameStad='{TempStadSearch}' and NameB = '{TempBankSearch}';\r\n");
            }
        }


        public static DataTable GetInfoCredit
        {
            get
            {
                if (TempBankSearch != null) ForGetBank();
                return ConnectorDB.QueryForDataTable($"select NameCredit as 'Назва Кредиту',MaxSumCredit as 'Макс Сумм (грн.)',ProcentCredit as 'Процент місяць %',TempContract as 'На скільки Кредит (міс.)',RealProcentCredit as 'Реальна процентна ставка %', ErstPay as 'Перший платіж (грн.)', DocumentCredit as 'Документи для оформлення',Description_ as 'Опис',Commision as 'Комісія %' from credit where id_Bank=(select id_Bank from bank where NameB='{TempTempBankSearch}');");
                //return ConnectorDB.QueryForList($"select filial.id_Stad,Adres,NumFil from Bank join filial using (id_Bank) join Stad on filial.id_Stad=Stad.id_Stad where NameStad='{TempStadSearch}' and NameB = '{TempBankSearch}';\r\n");
            }
        }


        public static DataTable GetInfoDeposit
        {
            get
            {
                if (TempBankSearch != null) ForGetBank();
                return ConnectorDB.QueryForDataTable($"select NameDeposit, TerminPayment, PayProcent, YearRate, EndBegin, DocumetPay from payment where id_Bank=(select id_Bank from bank where NameB='{TempTempBankSearch}');\r\n");
            }
        }
        //public static List<string> AddFilialInData
        //{
        //    get
        //    {
        //        return ConnectorDB.QueryForList($"INSERT INTO filial(id_Bank,id_Stad,NumTel,Adres,NumFil,TimeArbait,rating) VALUES((select id_Stad from stad where NameStad='{TempAddSelectedStad}'),(Select id_Bank from Bank where NameB='{TempAddsSelectedBank}'),'{TempNumTel}','{TempAdres}','{TempNumFil}','{TempTimeArbait}',{TempRating});");
        //    }

        //}
        public static void AddFilialInData() 
        {
            ConnectorDB.AddInDataBase($"INSERT INTO filial(id_Bank,id_Stad,NumTel,Adres,NumFil,TimeArbait,rating) VALUES((Select id_Bank from Bank where NameB='{TempAddsSelectedBank}'),(select id_Stad from stad where NameStad='{TempAddSelectedStad}'),'{TempNumTel}','{TempAdres}','{TempNumFil}','{TempTimeArbait}',{TempRating});");
        }


        public static string GetStadBankCreate
        {
            get
            {
                if (TempBankSearch != null) ForGetBank();
                return ConnectorDB.QueryForString($"select NameStad from bank join  stad using (id_Stad) where NameB='{TempTempBankSearch}';");
            }
        }



        public static string GetManBankCreate
        {
            get
            {
                if (TempBankSearch != null) ForGetBank();
                return ConnectorDB.QueryForString($"select ChairmanBoard from bank where NameB='{TempTempBankSearch}';");
            }
        }


        public static string GetTellNummBank
        {
            get
            {
                if (TempBankSearch != null) ForGetBank();
                return ConnectorDB.QueryForString($"select TellNum from bank where NameB='{TempTempBankSearch}';");
            }
        }

        public static List<string> SelectPaymant
        {
            get
            {
                return ConnectorDB.QueryForList($"select NameDeposit from payment ;");
            }
        }

        public static void UpdatePayment()
        {
            // ConnectorDB.QueryForString($"UPDATE payment SET {TerminPayment}{PayProcent}{YearRate}{EndBegin}{DocumetPay} where id_payment='(select id_payment from payment where '{NameDeposit}')';");
            ConnectorDB.QueryForString($"UPDATE ukrbank.payment AS p1 JOIN ( SELECT id_payment FROM ukrbank.payment WHERE NameDeposit = '{NameDeposit}' ) AS p2 ON p1.id_payment = p2.id_payment SET {TerminPayment}{PayProcent}{YearRate}{EndBegin}{DocumetPay};");
        }

        public static void AddPayment()
        {
            ConnectorDB.QueryForString($"insert into payment(NameDeposit,id_Bank,TerminPayment,PayProcent,YearRate,EndBegin,DocumetPay) value ('{TempAddNamePaymant}',(Select id_Bank from Bank where NameB='{TempAddsSelectedBankDeposit}'),'{TerminPayment}','{PayProcent}','{YearRate}',{EndBegin},'{DocumetPay}');");
        }

        public static void DeletePayment()
        {
            ConnectorDB.QueryForString($"DELETE payment FROM payment INNER JOIN (SELECT id_payment FROM payment WHERE NameDeposit='{DeletSelectedPaymant}') AS temp ON payment.id_payment = temp.id_payment;");
        }

        public static void AddCredit()
        {
            ConnectorDB.QueryForString($"insert into credit(NameCredit,id_Bank,MaxSumCredit,ProcentCredit,TempContract,RealProcentCredit,ErstPay,DocumentCredit,Description_,Commision) values('{NameCredit}',(Select id_Bank from Bank where NameB='{TempAddsSelectedBankCredit}'),'{MaxSumCredit}','{ProcentCredit}','{TempContract}','{RealProcentCredit}','{ErstPay}','{DocumentCredit}','{Description_}','{Commision}');");
        }

        public static void UpdateCredit()
        {
            ConnectorDB.QueryForString($"UPDATE ukrbank.credit AS p1 JOIN ( SELECT id_credit FROM ukrbank.credit WHERE NameCredit='asdsa' ) AS p2 ON p1.id_credit = p2.id_credit SET p1.MaxSumCredit = '12',p1.ProcentCredit = 1,p1.TempContract = 12.5,p1.RealProcentCredit = 123,p1.ErstPay = 12312 ,p1.DocumentCredit='паспорт та ідентифікаційний код',p1.Description_='asdas', p1.Commision=1;");
        }

        public static void DeleteCredit()
        {
            ConnectorDB.QueryForString($"DELETE credit FROM credit INNER JOIN(SELECT id_credit FROM credit WHERE NameCredit = 'asdasd') AS temp ON credit.id_credit = temp.id_credit;");
        }
        public static List<string> SelectCreditNameCredit
        {
            get
            {
                return (ConnectorDB.QueryForList($"select distinct NameCredit from credit;"));
            }
        }

        public static List<string> SelectCreditTempContract
        {
            get
            {
                return (ConnectorDB.QueryForList($"select distinct TempContract from credit ORDER BY TempContract;"));
            }
        }

        public static void TempTempContract1()
        {
            TempTempContract = TempContract;
        }

        public static void TempNameCredit1()
        {
            TempNameCredit = NameCredit;
        }

        public static DataTable GetInfoCreditDop
        {
            get
            {
                if (!string.IsNullOrEmpty(TempContract) && !string.IsNullOrEmpty(NameCredit)) { TempNameCredit1(); TempTempContract1(); }
                return ConnectorDB.QueryForDataTable($"select (select NameB from bank where id_Bank=credit.id_Bank), NameCredit, MaxSumCredit, ProcentCredit, TempContract, RealProcentCredit, ErstPay, DocumentCredit, Description_ , Commision from credit where NameCredit = '{TempNameCredit}' and TempContract <='{TempTempContract}' ;");
            }
        }

    }
}
