using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;
using System.IO;
using System.Globalization;

namespace InvoiceApp
{
    public partial class MainWindow : Window
    {
        private static string folderPath = @"D:\C#\InvoiceApp";
        private string filePath = Path.Combine(folderPath, "invoices.xml");

        private List<Invoice> invoices = new List<Invoice>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddInvoice_Click(object sender, RoutedEventArgs e)
        {

            if (ValidateInputs())
            {
                Invoice invoice = new Invoice
                {
                    ClientName = txtClientName.Text,
                    ClientStreet = txtClientStreet.Text,
                    ClientCity = txtClientCity.Text,
                    ClientCountry = txtClientCountry.Text,
                    ClientZipCode = int.Parse(txtClientZipCode.Text),
                    ClientCompanyNumber = int.Parse(txtClientCompanyNumber.Text),
                    CompanyName = txtCompanyName.Text,
                    CompanyStreet = txtCompanyStreet.Text,
                    CompanyCity = txtCompanyCity.Text,
                    CompanyCountry = txtCompanyCountry.Text,
                    CompanyZipCode = int.Parse(txtCompanyZipCode.Text),
                    CompanyNumber = int.Parse(txtCompanyNumber.Text),
                    InvoiceDate = DateTime.Parse(txtInvoiceDate.Text),
                    InvoiceItem = txtInvoiceName.Text,
                    InvoiceQuantity = int.Parse(txtInvoiceQuantity.Text),
                    InvoicePrice = float.Parse(txtInvoicePrice.Text),
                    InvoiceTax = float.Parse(txtInvoiceTax.Text),
                    InvoiceAmount = float.Parse(txtInvoiceAmount.Text)
                };

                invoices.Add(invoice);
                SerializeInvoices();

                MessageBox.Show("Faktura została dodana");
                ClearAllInputFields();
            }
        }

        private void ViewInvoices_Click(object sender, RoutedEventArgs e)
        {
            ViewInvoices();
        }

        private void SerializeInvoices()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Invoice>));

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, invoices);
            }
        }

        private void DeserializeInvoices()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Invoice>));

            using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                if (stream.Length > 0)
                {
                    invoices = (List<Invoice>)serializer.Deserialize(stream);
                }
            }
        }

        private void ViewInvoices()
        {
            DeserializeInvoices();

            dgInvoices.ItemsSource = invoices;
        }

        public void ClearAllInputFields()
        {
            txtClientName.Text = "";
            txtClientStreet.Text = "";
            txtClientCity.Text = "";
            txtClientCountry.Text = "";
            txtClientZipCode.Text = "";
            txtClientCompanyNumber.Text = "";
            txtCompanyName.Text = "";
            txtCompanyStreet.Text = "";
            txtCompanyCity.Text = "";
            txtCompanyCountry.Text = "";
            txtCompanyZipCode.Text = "";
            txtCompanyNumber.Text = "";
            txtInvoiceDate.Text = "";
            txtInvoiceName.Text = "";
            txtInvoiceQuantity.Text = "";
            txtInvoicePrice.Text = "";
            txtInvoiceTax.Text = "";
            txtInvoiceAmount.Text = "";
        }

        private void DeleteAllInvoices_Click(object sender, RoutedEventArgs e)
        {
            DeserializeInvoices();

            invoices.Clear();

            SerializeInvoices();

            ViewInvoices();
        }

        private void DeleteLastInvoice_Click(object sender, RoutedEventArgs e)
        {
            DeserializeInvoices();

            if (invoices.Count != 0)
            {
                invoices.RemoveAt(invoices.Count - 1);
            }

            SerializeInvoices();

            ViewInvoices();
        }


        private bool ValidateInputs()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(txtClientName.Text))
            {
                MessageBox.Show("Client Name field is required.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtClientStreet.Text))
            {
                MessageBox.Show("Client Street field is required.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtClientCity.Text))
            {
                MessageBox.Show("Client City field is required.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtClientCountry.Text))
            {
                MessageBox.Show("Client Country field is required.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtClientZipCode.Text) || !int.TryParse(txtClientZipCode.Text, out int clientZipCode) || clientZipCode < 0)
            {
                MessageBox.Show("Client Zip Code field is required, must be a valid integer and greater than 0");
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtClientCompanyNumber.Text) || !int.TryParse(txtClientCompanyNumber.Text, out int clientCompanyNumber) || clientCompanyNumber < 0)
            {
                MessageBox.Show("Client Company Number field is required, must be a valid integer and greater than 0");
                isValid = false;
            }

            // Validate Company inputs
            if (string.IsNullOrEmpty(txtCompanyName.Text))
            {
                MessageBox.Show("Company Name field is required.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtCompanyStreet.Text))
            {
                MessageBox.Show("Company Street field is required.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtCompanyCity.Text))
            {
                MessageBox.Show("Company City field is required.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtCompanyCountry.Text))
            {
                MessageBox.Show("Company Country field is required.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtCompanyZipCode.Text) || !int.TryParse(txtCompanyZipCode.Text, out int companyZipCode) || companyZipCode < 0)
            {
                MessageBox.Show("Company Zip Code field is required, must be a valid integer and greater than 0");
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtCompanyNumber.Text) || !int.TryParse(txtCompanyNumber.Text, out int companyNumber) || companyNumber < 0)
            {
                MessageBox.Show("Company Number field is required, must be a valid integer and greater than 0");
                isValid = false;
            }

            if (!DateTime.TryParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime invoiceDate))
            {
                MessageBox.Show("Please enter a valid date (dd/MM/yyyy).");
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtInvoiceName.Text))
            {
                MessageBox.Show("Invoice Item field is required.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtInvoiceQuantity.Text) || !int.TryParse(txtInvoiceQuantity.Text, out int invoiceQuantity) || invoiceQuantity < 0)
            {
                MessageBox.Show("Invoice Quantity field is required, must be a valid integer and greater than 0");
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtInvoicePrice.Text) || !float.TryParse(txtInvoicePrice.Text, out float invoicePrice) || invoicePrice <= 0)
            {
                MessageBox.Show("Invoice Price field is required, must be a valid float and equal or greater than 0.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtInvoiceTax.Text) || !float.TryParse(txtInvoiceTax.Text, out float invoiceTax) || invoiceTax <= 0)
            {
                MessageBox.Show("Invoice Tax field is required, must be a valid float and equal or greater than 0.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtInvoiceAmount.Text) || !float.TryParse(txtInvoiceAmount.Text, out float invoiceAmount) || invoiceAmount <= 0)
            {
                MessageBox.Show("Invoice Amount field is required, must be a valid float and equal or greater than 0.");
                isValid = false;
            }

            return isValid;
        }

        public class Invoice
        {
            public string ClientName { get; set; }
            public string ClientStreet { get; set; }
            public string ClientCity { get; set; }
            public string ClientCountry { get; set; }
            public int ClientZipCode { get; set; }
            public int ClientCompanyNumber { get; set; }


            public string CompanyName { get; set; }
            public string CompanyStreet { get; set; }
            public string CompanyCity { get; set; }
            public string CompanyCountry { get; set; }
            public int CompanyZipCode { get; set; }
            public int CompanyNumber { get; set; }


            public DateTime InvoiceDate { get; set; }
            public string InvoiceItem { get; set; }
            public int InvoiceQuantity { get; set; }
            public float InvoicePrice { get; set; }
            public float InvoiceTax { get; set; }
            public float InvoiceAmount { get; set; }
        }
    }
}