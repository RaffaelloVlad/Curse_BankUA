using Curse.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Microsoft.VisualBasic;
using System.Data;


namespace Curse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index(IFormCollection form)
        {
            if (form.ContainsKey("submit"))
            {
                
            }
            if (form.ContainsKey("submit2"))
            {
                return RedirectToAction("AdminPanel");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Dop(string SelectCreditTempContract, string SelectCreditNameCredit)
        {
            SearchInDatabase.TempContract = SelectCreditTempContract;
            SearchInDatabase.NameCredit = SelectCreditNameCredit;

            if (SelectCreditNameCredit != null)
                SearchInDatabase.TempNameCredit1();




            return View();
        }


        public IActionResult Admin(string login, string password)
        {
            string connectionString = $"Server=127.0.0.1;Database=Ukrbank;port=3306;User Id={login};password={password}";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                // Подключение прошло успешно, перенаправляем на другую страницу
                return RedirectToAction("AdminPanel");
            }
            catch (Exception ex)
            {
                // Ошибка подключения, обработка и вывод сообщения пользователю
                return View("Admin", ex);
            }
            //return View();
        }

        public IActionResult AdminPanel(string AddsSelectedBankCredit, string NameCredit, string Commision, string Description_, string DocumentCredit,string ErstPay, string RealProcentCredit, string TempContract, string ProcentCredit, string MaxSumCredit, string DeletSelectedPaymant, string DocumetPay2, string YearRate2, string PayProcent2, string TerminPayment2, string AddsSelectedBankDeposit, string AddNamePaymant, string DocumetPay, string YearRate, string EndBegin, string rating, string PayProcent, string AddsSelectedBank, string AddSelectedStad,string NumTel, string Adres, string NumFil, string TimeArbait,string UpdateSelectedBank,string TerminPayment, IFormCollection form)
        {
            try
            {
                if (form.ContainsKey("submit"))
                {
                    if (AddSelectedStad != null && AddsSelectedBank != null && NumTel != null && NumFil != null && TimeArbait != null && rating != null && Adres != null)
                    {
                        SearchInDatabase.TempAddSelectedStad = AddSelectedStad;
                        SearchInDatabase.TempAddsSelectedBank = AddsSelectedBank;
                        SearchInDatabase.TempNumTel = NumTel;
                        SearchInDatabase.TempNumFil = NumFil;
                        SearchInDatabase.TempTimeArbait = TimeArbait;
                        SearchInDatabase.TempRating = rating;
                        SearchInDatabase.TempAdres = Adres;
                        SearchInDatabase.AddFilialInData();
                    }
                }
                if (form.ContainsKey("submit2"))
                {
                    SearchInDatabase.NameDeposit = UpdateSelectedBank;
                    if (string.IsNullOrEmpty(PayProcent) && string.IsNullOrEmpty(YearRate) && string.IsNullOrEmpty(EndBegin) && string.IsNullOrEmpty(DocumetPay))
                        SearchInDatabase.TerminPayment = $"p1.TerminPayment = '{TerminPayment}'" ?? "";
                    else
                        SearchInDatabase.TerminPayment = $"p1.TerminPayment = '{TerminPayment}'," ?? "";
                    
                    if (string.IsNullOrEmpty(YearRate) && string.IsNullOrEmpty(EndBegin) && string.IsNullOrEmpty(DocumetPay))
                        SearchInDatabase.PayProcent = $"p1.PayProcent = {PayProcent}" ?? "";
                    else
                        SearchInDatabase.PayProcent = $"p1.PayProcent = {PayProcent}," ?? "";

                    if (string.IsNullOrEmpty(EndBegin) && string.IsNullOrEmpty(DocumetPay))
                        SearchInDatabase.YearRate = $"p1.YearRate = {YearRate}" ?? "";
                    else
                        SearchInDatabase.YearRate = $"p1.YearRate = {YearRate}," ?? "";

                    if (string.IsNullOrEmpty(DocumetPay))
                        SearchInDatabase.EndBegin = $"p1.EndBegin = {EndBegin}" ?? "";
                    else
                        SearchInDatabase.EndBegin = $"p1.EndBegin = {EndBegin}," ?? "";

                    SearchInDatabase.DocumetPay = $"p1.DocumetPay = '{DocumetPay}' " ?? "";
                    SearchInDatabase.UpdatePayment();

                }
                if (form.ContainsKey("submit3"))
                {
                    if (!string.IsNullOrEmpty(TerminPayment2) && !string.IsNullOrEmpty(PayProcent2) && !string.IsNullOrEmpty(YearRate2) && !string.IsNullOrEmpty(EndBegin) && !string.IsNullOrEmpty(DocumetPay2) && !string.IsNullOrEmpty(AddNamePaymant) && !string.IsNullOrEmpty(AddsSelectedBankDeposit))
                    {
                        SearchInDatabase.TempAddNamePaymant = AddNamePaymant;
                        SearchInDatabase.TempAddsSelectedBankDeposit = AddsSelectedBankDeposit;
                        SearchInDatabase.TerminPayment = TerminPayment2;
                        SearchInDatabase.PayProcent = PayProcent2;
                        SearchInDatabase.YearRate = YearRate2;
                        SearchInDatabase.EndBegin = EndBegin;
                        SearchInDatabase.DocumetPay = DocumetPay2;
                        SearchInDatabase.AddPayment();
                    }
                }
                if (form.ContainsKey("submit4"))
                {
                    SearchInDatabase.DeletSelectedPaymant = DeletSelectedPaymant;
                    SearchInDatabase.DeletePayment();
                }
                if (form.ContainsKey("submit5"))
                {
                    if(!string.IsNullOrEmpty(NameCredit) && !string.IsNullOrEmpty(MaxSumCredit) && !string.IsNullOrEmpty(ProcentCredit) && !string.IsNullOrEmpty(TempContract) && !string.IsNullOrEmpty(RealProcentCredit) && !string.IsNullOrEmpty(ErstPay) && !string.IsNullOrEmpty(DocumentCredit) && !string.IsNullOrEmpty(Description_) && !string.IsNullOrEmpty(Commision))
                    {
                        SearchInDatabase.TempAddsSelectedBankCredit = AddsSelectedBankCredit;
                        SearchInDatabase.NameCredit = NameCredit;
                        SearchInDatabase.MaxSumCredit = MaxSumCredit;
                        SearchInDatabase.ProcentCredit = ProcentCredit;
                        SearchInDatabase.TempContract = TempContract;
                        SearchInDatabase.RealProcentCredit = RealProcentCredit;
                        SearchInDatabase.ErstPay = ErstPay;
                        SearchInDatabase.DocumentCredit = DocumentCredit;
                        SearchInDatabase.Description_ = Description_;
                        SearchInDatabase.Commision = Commision;
                        SearchInDatabase.AddCredit();
                    }
                }
                if (form.ContainsKey("submit6"))
                {

                }
                if (form.ContainsKey("submit7"))
                {

                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }


            
        }


        public IActionResult Resultat(string selectedStad, string selectedBank, IFormCollection form)
        {
            if (selectedStad != null && selectedBank != null)
            {
                SearchInDatabase.TempStadSearch = selectedStad;
                SearchInDatabase.TempBankSearch = selectedBank;
            }

            if (selectedBank != null)
                SearchInDatabase.ForGetBank();

            try
            {
                if (form.ContainsKey("PrintPDF"))
                {
                    PrintPDF.GeneratePdf(SearchInDatabase.GetInfoCredit, $"послуги {SearchInDatabase.TempTempBankSearch.Replace('"',' ')} {DateAndTime.Now.ToString().Replace(':', '.')}.pdf");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при создании PDF-файла: " + ex.Message);
                
            }

            return View();
        }

        public IActionResult Credit()
        {
            return View();
        }

        public IActionResult Deposit()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}